/*  
    This file is part of LatexMath2MathML.

    LatexMath2MathML is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    LatexMath2MathML is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with LatexMath2MathML.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Linq;

namespace LatexMath2MathML
{
	internal enum TextSize
	{
		tiny, scriptsize, footnotesize, small, normalsize, large, Large, LARGE, huge, Huge	
	}
	
	[Flags]
	internal enum TextStyle
	{
		Normal = 0,
        Bold = 1,
        Italic = 2,
        Underlined = 4,
        Strikethrough = 8
	}
	
    internal struct LabeledReference
    {
        public string Kind;
        public int Number;
    }

    /// <summary>
    /// The converter from Latex to XHTML + MathML.
    /// </summary>
    public sealed class LatexMathToMathMLConverter
    {

		public string output { get; private set; }

        readonly string _sourceText;
        internal Dictionary<string, LabeledReference> References;


		//TODO How shall we present the text?
		/// <summary>
		///The dictionary to get the corresponding CSS style from a text size definition. 
		/// </summary>
		/*internal static readonly Dictionary<TextSize, string> CSSTextSizeDictionary = new Dictionary<TextSize, string>
		{
			{TextSize.tiny, "tiny"},
			{TextSize.scriptsize, "scriptsize"},
			{TextSize.footnotesize, "footnotesize"},
			{TextSize.small, "small"},
			{TextSize.normalsize, "normalsize"},
			{TextSize.large, "large"},
			{TextSize.Large, "xlarge"},
			{TextSize.LARGE, "xxlarge"},
			{TextSize.huge, "huge"},
			{TextSize.Huge, "xhuge"},
		};        */
		
        private static readonly log4net.ILog Log =
            log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


		
        private void CommonInitialize()
        {            
            WaitForConversionFinish = true;
            Localization = System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
			CurrentTextSize = TextSize.normalsize;
			CurrentTextStyle = TextStyle.Normal;
            Counters = new Dictionary<string, int>();
        }
        
        /// <summary>
        /// Initializes a new instance of the LatexToMathConverter class.
        /// </summary>
        /// <param name="sourcePath">The path to the source LaTeX document to convert.</param>
        /// <param name="sourceEncoding">The character encoding of the specified LaTeX document.</param>
        /// <param name="outputPath">The path to the conversion result.</param>
        public LatexMathToMathMLConverter(string latexExpression)
        {            
            _sourceText = latexExpression;

            CommonInitialize();
        }

        #region Properties

        /// <summary>
        ///Gets the Bibtex records attached to this document. 
        /// </summary>

        /// <summary>
        /// Gets the counters for various needs.
        /// </summary>
        internal Dictionary<string, int> Counters { get; private set; }

		/// <summary>
		/// Gets or sets the current text size.
		/// </summary>
		internal TextSize CurrentTextSize { get; set; }
		
		/// <summary>
		///Gets or sets the current text style. 
		/// </summary>
		internal TextStyle CurrentTextStyle { get; set; }

        /// <summary>
        /// Gets or sets the localized title of the table of contents.
        /// Default: "Contents".
        /// </summary>
        public string TableOfContentsTitle { get; set; }

        /// <summary>
        /// Gets or sets the localized title of the bibliography.
        /// Default: "Bibliography".
        /// </summary>
        public string BibliographyTitle { get; set; }

        /// <summary>
        /// Gets or sets Gets the ISO 639-1 two-letter code for the desired language.
        /// Default: System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName.
        /// </summary>
        public string Localization { get; set; }

        /// <summary>
        /// Gets or sets the value specifying whether to perform the DTD validation
        /// of the conversion result. If the validation is successful, W3C validity
        /// logos are inserted.
        /// </summary>
        public bool ValidateResult { get; set; }

        /// <summary>
        /// Gets or sets the value indicating whether to wait for the worker thread to terminate
        /// instead of using OnConversionFinish event.
        /// </summary>
        public bool WaitForConversionFinish { get; set; }

        /// <summary>
        /// Gets or sets the section format string to use with String.Format.
        /// </summary>
        public string SectionFormat { get; set; }

        /// <summary>
        /// Gets or sets the subsection format string to use with String.Format.
        /// </summary>
        public string SubsectionFormat { get; set; }

        /// <summary>
        /// Gets or sets the section format in the table of contents to use with String.Format.
        /// </summary>
        public string TocSectionFormat { get; set; }

        /// <summary>
        /// Gets or sets the subsection format in the table of contents to use with String.Format.
        /// </summary>
        public string TocSubsectionFormat { get; set; }

        /// <summary>
        /// Gets or sets the cite format (BibTeX).
        /// </summary>
        public string CiteFormat { get; set; }

        /// <summary>
        /// Gets or sets the bibliography record format (BibTeX).
        /// Parameters:
        /// 0 - reference (internal)
        /// 1 - Number
        /// 2 - Author
        /// 3 - title
        /// 4 - Everything else, each will be comma separated.
        /// </summary>
        public string BibliographyRecordFormat { get; set; }
        
        #endregion

        #region Events

        /// <summary>
        /// Occurs when the LaTeX document was parsed and the object tree was built.
        /// </summary>
        public event EventHandler AfterDocumentWasParsed;

        /// <summary>
        /// Occurs when the converter has finished the main work and is about to properly indent
        /// the result XML.
        /// </summary>
        public event EventHandler BeforeXmlFormat;

        /// <summary>
        /// Occurs when the converter is about to perform DTD validation.
        /// </summary>
        public event EventHandler BeforeValidate;

        /// <summary>
        /// Occurs when the conversion has been finished.
        /// </summary>
        public event EventHandler OnConversionFinish;
        #endregion      


		
        private void ThreadConvert(object obj)
        {
            var parser = new LatexParser(_sourceText, this);
            LatexExpression root;
            //try
            //{
                root = parser.Root;
            /*}
// ReSharper disable RedundantCatchClause
#pragma warning disable 168
            catch (Exception e)
#pragma warning restore 168
            {
#if DEBUG
                throw;
#else
                Log.Error("Failed to parse the document", e);
                return;
#endif
            }*/
// ReSharper restore RedundantCatchClause
            CallEventHandler(AfterDocumentWasParsed);
			//TODO Add eventual DTD validation in a nicer way
			String str = "<!DOCTYPE math SYSTEM 'http://www.w3.org/Math/DTD/mathml1/mathml.dtd'>";
            output = root.Convert();
            CallEventHandler(BeforeXmlFormat);
            XDocument xml;
			str += output;
			xml = XDocument.Parse(str);

            if (ValidateResult)
            {
				#region Validate
                CallEventHandler(BeforeValidate);               
                var settings = new XmlReaderSettings
                {
                    ProhibitDtd = false,
                    ValidationType = ValidationType.DTD
                };
// ReSharper disable ConvertToConstant.Local
                bool validationSuccessful = true;
// ReSharper restore ConvertToConstant.Local
                settings.ValidationEventHandler += (s, e) =>
                {
#if DEBUG
                    throw e.Exception;
#else
                    Log.Debug("DTD Validator - " + e.Message, e.Exception);
                    validationSuccessful = false;
#endif
                };
                var reader = xml.CreateReader();
                while (reader.Read()) { }
// ReSharper disable ConditionIsAlwaysTrueOrFalse
                if (validationSuccessful)
// ReSharper restore ConditionIsAlwaysTrueOrFalse
                {
                    #region Insert W3C logos
                    // ReSharper disable PossibleNullReferenceException
					Console.WriteLine("success");/*
                    var body = xml.Element(XName.Get("html", "http://www.w3.org/1999/xhtml")).
                        Element(XName.Get("body", "http://www.w3.org/1999/xhtml"));
                    var img = new XElement(XName.Get("img", "http://www.w3.org/1999/xhtml"));
                    img.SetAttributeValue("src", "http://www.w3.org/Icons/valid-xhtml11-blue.png");
                    img.SetAttributeValue("alt", "Valid XHTML 1.1");
                    body.Add(img);
                    img = new XElement(XName.Get("img", "http://www.w3.org/1999/xhtml"));
                    img.SetAttributeValue("src", "http://www.w3.org/Icons/valid-css-blue.png");
                    img.SetAttributeValue("alt", "Valid CSS");
                    body.Add(img);
                    img = new XElement(XName.Get("img", "http://www.w3.org/1999/xhtml"));
                    img.SetAttributeValue("src", "http://www.w3.org/Icons/valid-mathml20-blue.png");
                    img.SetAttributeValue("alt", "Valid MATHML 2.0");
                    body.Add(img);                 */   
                    // ReSharper restore PossibleNullReferenceException                    
                    #endregion
                }
                //xml.Save(OutputPath);
                #endregion
            }
        }

        /// <summary>
        /// Starts the conversion process.
        /// </summary>
        public void Convert()
        {
            if (WaitForConversionFinish)
            {
                var thread = new Thread(ThreadConvert);
                thread.Start(null);
                var checkPoint = DateTime.Now;
                thread.Join(120000);
                if ((DateTime.Now - checkPoint).Seconds > 119)
                {
                    Log.Error("It took more than 2 minutes to convert the document");
                }
            }
            else
            {
                var thread = new BackgroundWorker();
                thread.DoWork += (s, e) => ThreadConvert(null);
                thread.RunWorkerCompleted += (s, e) => CallEventHandler(OnConversionFinish, e);
                thread.RunWorkerAsync();                    
            }
        }

        /// <summary>
        /// Invokes an event handler if it is not null in a diagnostic environment.
        /// </summary>
        /// <param name="handler">The EventHandler instance to invoke.</param>
        private void CallEventHandler(EventHandler handler)
        {
            if (handler != null)
            {
                try
                {
                    handler(this, EventArgs.Empty);
                }
// ReSharper disable RedundantCatchClause
#pragma warning disable 168
                catch (Exception e)
#pragma warning restore 168
                {
#if DEBUG
                    throw;
#else
                    Log.Debug("A user exception occured when " + 
                        handler.Method.Name + " was raised with a message \"" + e.Message + "\"");
#endif
                }
// ReSharper restore RedundantCatchClause
            }
        }

        /// <summary>
        /// Invokes an event handler if it is not null in a diagnostic environment.
        /// </summary>
        /// <param name="handler">The EventHandler instance to invoke.</param>
        /// <param name="args">The event arguments to send.</param>
        private void CallEventHandler(EventHandler handler, EventArgs args)
        {
            if (handler != null)
            {
                try
                {
                    handler(this, args);
                }
// ReSharper disable RedundantCatchClause
#pragma warning disable 168
                catch (Exception e)
#pragma warning restore 168
                {
#if DEBUG
                    throw;
#else
                    Log.Debug("A user exception occured when " + 
                        handler.Method.Name + " was raised with a message \"" + e.Message + "\"");
#endif
                }
// ReSharper restore RedundantCatchClause
            }
        }             
    }
}
