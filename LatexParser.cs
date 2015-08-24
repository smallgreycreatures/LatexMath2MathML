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
using System.IO;

namespace LatexMath2MathML
{
    /// <summary>
    /// The parser of the LaTeX document object tree.
    /// </summary>
    internal sealed class LatexParser
    {
        private static readonly log4net.ILog Log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The source string to parse.
        /// </summary>
        private string _source;

        /// <summary>
        /// The LatexMathToMathMLConverter class instance to customize the conversion result.
        /// </summary>
        private readonly LatexMathToMathMLConverter _customization;

        /// <summary>
        /// The root of the document object tree.
        /// </summary>
        private LatexExpression _root;

        /// <summary>
        /// The hash list of custom commands.
        /// </summary>
        private Dictionary<string, LatexExpression> _customCommands;		

        private static readonly List<string> ArrayLikeBlockNames = new List<string>
        {
            "array", "eqnarray"
        };
		//TODO kill this__
      public static readonly List<string> InfoNames = new List<string>
        { 
            "author", "title", "date" 
        };

        /// <summary>
        /// Gets the root of the object tree.
        /// </summary>
        public LatexExpression Root 
        {
            get
            {
                if (_root == null)
                {
                    Parse(_customization);
                }
                return _root;
            }
        }		

        /// <summary>
        /// The event to report the progress of the parse process.
        /// </summary>
        public EventHandler<ProgressEventArgs> ProgressEvent;

        private void OnProgressEvent(byte index, byte count)
        {
            if (ProgressEvent != null)
            {
                ProgressEvent(this, new ProgressEventArgs(index, count));
            }
        }

        /// <summary>
        /// Initializes a new instance of the LatexParser class.
        /// </summary>
        /// <param name="source">The source string to build the tree from.</param>
        /// <param name="customization">The LatexMathToMathMLConverter class instance to customize the conversion result.</param>
        public LatexParser(string source, LatexMathToMathMLConverter customization)
        {
            if (String.IsNullOrEmpty(source))
            {
                throw new ArgumentException("The parameter can not be null or empty.", "source");
            }
            _source = source;
            _customization = customization;
        }

        /// <summary>
        /// Loggs a message if not in DEBUG.
        /// </summary>
        /// <param name="message">The message to debug.</param>
        private static void LogInfo(object message)
        {
#if !DEBUF
            Log.Info(message);
#endif
        }

        /// <summary>
        /// Builds the document object tree.
        /// </summary>
        /// <param name="customization">The LatexMathToMathMLConverter class instance to customize the conversion result.</param>
        /// <remarks>The parsing procedure consists of stand-alone passes, so that it resembles a compiler pipeline.</remarks>
        public void Parse(LatexMathToMathMLConverter customization)
        {
            foreach (var rule in PreformatRules)
            {
                _source = _source.Replace(rule[0], rule[1]);
            }
            var rdr = new StringReader(_source);
            const byte PASS_COUNT = 14;
            byte step = 1;

            // Build the tree
            LogInfo("CreateRoot");
            _root = LatexExpression.CreateRoot(rdr, customization);
            OnProgressEvent(step++, PASS_COUNT);

			//TODO Go through the usage of _customCommands
            // Rebuild tree with custom commands
            _customCommands = new Dictionary<string, LatexExpression>();
            LogInfo("RecursiveParseCustomCommands");

            // Incapsulate fragments between \begin and \end
            LogInfo("IncapsulateCommands");
            IncapsulateCommands(Root.Expressions[0]);
            OnProgressEvent(step++, PASS_COUNT);

			// Post-parse arrays
			LogInfo("PostParseArrays");
			PostParseArrays(Root.Expressions[0], customization);
			OnProgressEvent(step++, PASS_COUNT);

			// Build super- and subscripts
			LogInfo("BuildScripts");
			BuildScripts(Root.Expressions[0], customization);
			OnProgressEvent(step++, PASS_COUNT);  

            // Simplify math blocks that begin with baseless scripts
            LogInfo("SimplifyScripts");
            SimplifyBaselessScripts(Root.Expressions[0]);
            OnProgressEvent(step++, PASS_COUNT);

            // Numerate blocks (needed for the next step)
            LogInfo("NumerateBlocks");
            NumerateBlocks(Root.FindDocument().Expressions[0]);
            Root.Customization.Counters.Add("document", 0);
            OnProgressEvent(step++, PASS_COUNT);

            // Simplify math blocks that begin with baseless scripts
            LogInfo("PreProcessLabels");
            PreprocessLabels(Root.FindDocument().Expressions[0]);
            Root.Customization.Counters.Clear();
            OnProgressEvent(step++, PASS_COUNT);

            // Deal with algorithmic blocks
            LogInfo("PreprocessAlgorithms");
            PreprocessAlgorithms(Root.FindDocument().Expressions[0]);
            OnProgressEvent(step++, PASS_COUNT);
			
            LogInfo("Finished");
        }


		
        /// <summary>
        /// Recursively wrap algorithmic block expressions into blocks
        /// </summary>
        /// <param name="outline">The outline of the root LatexExpression instance.</param>
        private static void PreprocessAlgorithms(List<LatexExpression> outline)
        {
            for (int i = 0; i < outline.Count; i++)
            {
                if (outline[i].Expressions != null &&
                    (outline[i].ExprType == ExpressionType.Command || outline[i].ExprType == ExpressionType.Block))
                {
                    foreach (var subTree in outline[i].Expressions)
                    {
                        PreprocessAlgorithms(subTree);
                    }
                }

                if (outline[i].ExprType == ExpressionType.Block &&
                    (outline[i].Name == "algorithmic" || outline[i].Name == "algorithmicx"))
                {
                    int indentation = 0;
                    int counter = 1;
                    foreach (var part in outline[i].Expressions[0])
                    {
                        if (part.ExprType == ExpressionType.Command)
                        {
                            if (part.Name == "Procedure" || part.Name == "Function" || part.Name == "Begin")
                            {
                                counter++;
                                indentation += 2;
                                continue;
                            }
                            if (part.Name == "EndProcedure" || part.Name == "EndFunction" || part.Name == "End")
                            {
                                counter++;
                                indentation -= 2;
                                continue;
                            }
                            if (part.ExprType == ExpressionType.Command && part.Name == "State")
                            {
                                counter++;
                                part.Tag = new[] {counter, indentation};
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Recursively numerate blocks by adding a new expression to the end.
        /// </summary>
        /// <param name="outline">The outline of the root LatexExpression instance.</param>
        private static void NumerateBlocks(List<LatexExpression> outline)
        {
            for (int i = 0; i < outline.Count; i++)
            {
                if (outline[i].Expressions != null &&
                        (outline[i].ExprType == ExpressionType.Command || outline[i].ExprType == ExpressionType.Block))
                {
                    foreach (var subTree in outline[i].Expressions)
                    {
                        NumerateBlocks(subTree);
                    }
                }

                if (outline[i].ExprType == ExpressionType.Block && outline[i].Name != "")
                {
                    int index;
                    if (outline[i].Customization.Counters.ContainsKey(outline[i].Name))
                    {
                        index = ++outline[i].Customization.Counters[outline[i].Name];
                    }
                    else
                    {
                        outline[i].Customization.Counters.Add(outline[i].Name, 1);
                        index = 1;
                    }
                    outline[i].Tag = index;
                }                
            }
        }
		/// <summary>
		/// Recursively walk the tree and handle labels.
		/// </summary>
		/// <param name="outline">The outline of the root LatexExpression instance.</param>
		private static void PreprocessLabels(List<LatexExpression> outline)
		{
			for (int i = 0; i < outline.Count; i++)
			{
				if (!(outline[i].ExprType == ExpressionType.Command && outline[i].Name == "label"))
				{
					if (outline[i].Expressions != null &&
						(outline[i].ExprType == ExpressionType.Command || outline[i].ExprType == ExpressionType.Block))
					{
						foreach (var subTree in outline[i].Expressions)
						{
							PreprocessLabels(subTree);
						}
					}
				}
				else
				{
					var parentBlock = outline[i].Parent;
					while (parentBlock.ExprType != ExpressionType.Block || parentBlock.Name == "" || parentBlock.Name == "paragraph")
					{
						parentBlock = parentBlock.Parent;
					}
					int index;
					if (parentBlock.Name == "document")
					{
						index = ++parentBlock.Customization.Counters["document"];
					}
					else
					{
						index = (int)parentBlock.Tag;
					}
					var reference = "";

					// Flatten the reference
					outline[i].EnumerateChildren(e =>
						{
							if (e.ExprType == ExpressionType.PlainText)
							{
								reference += e.Name;
							}
							return false;
						});
					outline[i].Tag = reference;
					if (!parentBlock.Customization.References.ContainsKey(reference))
					{
						parentBlock.Customization.References.Add(reference,
							new LabeledReference
							{Kind = parentBlock.Name, Number = index});
					}
				}
			}
		}

        /// <summary>
        /// Recursively split math blocks that begin with baseless scripts.
        /// </summary>
        /// <param name="outline">The outline of the root LatexExpression instance.</param>
        private static void SimplifyBaselessScripts(List<LatexExpression> outline)
        {
            for (int i = 0; i < outline.Count; i++)
            {
                if (outline[i].ExprType == ExpressionType.InlineMath)
                {
                    var firstExpr = outline[i].Expressions[0][0];
                    if (firstExpr.ExprType == ExpressionType.Block &&
                        (firstExpr.Name == "^" || firstExpr.Name == "_"))
                    {
                        firstExpr.IndexInParentChild = i;
                        firstExpr.ParentChildNumber = outline[i].ParentChildNumber;
                        firstExpr.Parent = outline[i].Parent;
                        firstExpr.EnumerateChildren(e => e.MathMode = false);

                        for (int j = i; j < outline.Count; j++)
                        {
                            outline[j].IndexInParentChild = j + 1;
                        }

                        for (int j = 1; j < outline[i].Expressions[0].Count; j++)
                        {
                            outline[i].Expressions[0][j].IndexInParentChild--;
                        }
                        outline[i].Expressions[0].RemoveAt(0);
                        outline.Insert(i, firstExpr);
                        i++;
                    }
                }
                else
                {
                    if (outline[i].Expressions != null && 
                        (outline[i].ExprType == ExpressionType.Command || outline[i].ExprType == ExpressionType.Block))
                    {
                        foreach (var subTree in outline[i].Expressions)
                        {
                            SimplifyBaselessScripts(subTree);
                        }
                    }   
                }                
            }
        }


		/// <summary>
		/// Recursively build scripts (for msup, munder, etc.)
		/// </summary>
		/// <param name="list">The outline of a LatexExpression instance.</param>
		/// <param name="customization">The LatexMathToMathMLConverter class instance to customize the conversion result.</param>
		private static void BuildScripts(List<LatexExpression> list, LatexMathToMathMLConverter customization)
		{
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].Expressions != null)
				{
					foreach (var subTree in list[i].Expressions)
					{
						BuildScripts(subTree, customization);
					}
				}
				if (list[i].ExprType == ExpressionType.Block &&
					(list[i].Name == "^" || list[i].Name == "_") && i > 0)
				{
					if (list[i - 1].ExprType == ExpressionType.Command && list[i - 1].Name == "limits")
					{
						list[i - 1].EraseFromParent();
						i--;
					}
					#region Place the previous expression to the script block                    
					if (i < list.Count - 1 && list[i + 1].ExprType == ExpressionType.Block &&
						(list[i + 1].Name == "^" || list[i + 1].Name == "_"))
					{                        
						var block = new LatexExpression("script" + list[i].Name + list[i + 1].Name, list[i].Parent,
							list[i].ParentChildNumber, i - 1, customization);                        
						block.MathMode = list[i].MathMode;                                                
						block.Expressions[0].Add(new LatexExpression(list[i - 1]));
						block.Expressions[0].Add(new LatexExpression(list[i]));
						block.Expressions[0].Add(new LatexExpression(list[i + 1]));
						block.Expressions[0][0].Parent = block;
						block.Expressions[0][0].ParentChildNumber = 0;
						block.Expressions[0][0].IndexInParentChild = 0;
						block.Expressions[0][0].MathMode = block.MathMode;
						block.Expressions[0][1].Parent = block;
						block.Expressions[0][1].ParentChildNumber = 0;
						block.Expressions[0][1].IndexInParentChild = 1;
						block.Expressions[0][1].MathMode = block.MathMode;
						block.Expressions[0][2].Parent = block;
						block.Expressions[0][2].ParentChildNumber = 0;
						block.Expressions[0][2].IndexInParentChild = 2;
						block.Expressions[0][2].MathMode = block.MathMode;
						list[i - 1] = block;
						list[i].EraseFromParent();
						list[i].EraseFromParent();
					}
					else
					{
						var block = new LatexExpression("script" + list[i].Name, list[i].Parent, list[i].ParentChildNumber, i - 1, customization);
						block.MathMode = list[i].MathMode;
						block.Expressions[0].Add(new LatexExpression(list[i - 1]));
						block.Expressions[0].Add(new LatexExpression(list[i]));
						block.Expressions[0][0].Parent = block;
						block.Expressions[0][0].ParentChildNumber = 0;
						block.Expressions[0][0].IndexInParentChild = 0;
						block.Expressions[0][0].MathMode = block.MathMode;
						block.Expressions[0][1].Parent = block;
						block.Expressions[0][1].ParentChildNumber = 0;
						block.Expressions[0][1].IndexInParentChild = 1;
						block.Expressions[0][1].MathMode = block.MathMode;                        
						list[i - 1] = block;
						list[i].EraseFromParent();
					}
					i--;
					#endregion                                               
				}
			}
		}


        /// <summary>
        /// Recursively finds the last command for the RecursiveParseCustomCommands.
        /// </summary>
        /// <param name="list">The list of expressions to search.</param>
        /// <returns>The last command or null.</returns>
        private static LatexExpression RecursiveFindCommandHandler(IList<LatexExpression> list)
        {
            for (int k = list.Count - 1; k > -1; k--)
            {
                if (list[k].ExprType == ExpressionType.Command)
                {
                    return list[k];
                }
                if (list[k].ExprType == ExpressionType.Block)
                {                    
                    for (int i = list[k].Expressions.Count - 1; i > -1; i--)
                    {
                        var res = RecursiveFindCommandHandler(list[k].Expressions[i]);
                        if (res != null)
                        {
                            return res;
                        }
                    }
                }
            }
            return null;
        }


        /// <summary>
        /// Recursively incapsulates tree fragments between \begin and \end.
        /// </summary>
        /// <param name="outline">The outline of a LatexExpression instance.</param>
        private static void IncapsulateCommands(List<LatexExpression> outline)
        {
            for (int i = 0; i < outline.Count; i++)
            {
                if (outline[i].ExprType == ExpressionType.Command &&
                    outline[i].Name == "begin")
                {
                    var cmdValue = outline[i].Expressions[0][0].Name;
                    int j;
                    for (j = i;
                        outline[j].Name != "end" || outline[j].Expressions == null || outline[j].Expressions[0][0].Name != cmdValue;
                        j++) { }
                    int length = j - i - 1;
                    var subOutline = new LatexExpression[length];
                    // Cut the right chunk
                    outline.CopyTo(i + 1, subOutline, 0, length);
                    outline.RemoveRange(i + 1, length + 1);
                    // Update outline[i]
                    outline[i].Name = cmdValue;
                    outline[i].ExprType = ExpressionType.Block;
                    outline[i].Expressions.RemoveAt(0);
                    for (int k = 0; k < outline[i].Expressions.Count; k++)
                    {
                        foreach (var expr in outline[i].Expressions[k])
                        {
                            expr.ParentChildNumber--;
                        }
                    }
                    // Update outline
                    for (int k = i + 1; k < outline.Count; k++)
                    {
                        outline[k].IndexInParentChild -= length + 1;
                    }                    
                    // Update subOutline
                    int parentChildNumber = outline[i].Expressions.Count;
                    for (int k = 0; k < subOutline.Length; k++)
                    {
                        subOutline[k].Parent = outline[i];
                        subOutline[k].ParentChildNumber = parentChildNumber;
                        subOutline[k].IndexInParentChild = k;
                    }                                       
                    // Link subOutline
                    outline[i].Expressions.Add(new List<LatexExpression>(subOutline));
                    IncapsulateCommands(outline[i].Expressions[parentChildNumber]);
                }
            }
            for (int i = 0; i < outline.Count; i++)
            {
                if (outline[i].Expressions != null)
                {
                    foreach (var subTree in outline[i].Expressions)
                    {
                        IncapsulateCommands(subTree);
                    }
                }
            }
        }

        /// <summary>
        /// Post-parses arrays.
        /// </summary>
        /// <param name="outline">The outline of a LatexExpression instance.</param>
        /// <param name="customization">The LatexMathToMathMLConverter class instance to customize the conversion result.</param>
        private static void PostParseArrays(IList<LatexExpression> outline, LatexMathToMathMLConverter customization)
        {
            for (int i = 0; i < outline.Count; i++)
            {
                if (outline[i].ExprType == ExpressionType.Block &&
                    (ArrayLikeBlockNames.Contains(outline[i].Name)))
                {
                    int parentChildNumber = outline[i].Expressions.Count == 1? 0 : 1;
                    var main = outline[i].Expressions[parentChildNumber];
                    var latex_array = new List<List<List<LatexExpression>>>(3) {new List<List<LatexExpression>>(3)};                    
                    #region Build array
                    int rowIndex = 0;
                    for (int j = 0; ; j++)
                    {
                        var cell = new List<LatexExpression>(2);
                        for (; j < main.Count &&
                            !(main[j].Name == "\\" && main[j].ExprType == ExpressionType.Command) &&
                            !(main[j].Name == "&" && main[j].ExprType == ExpressionType.PlainText)
                            ; j++)
                        {
                            // \hline won't do the same in XHTML
                            if (main[j].ExprType == ExpressionType.Command && main[j].Name == "hline") continue;
                            if (main[j].ExprType == ExpressionType.Comment) continue;
                            cell.Add(main[j]);
                        }
                        if (cell.Count > 0)
                        {
							latex_array[rowIndex].Add(cell);
                        }
                        if (j == main.Count)
                        {
                            break;
                        }
                        if (main[j].Name == "\\")
                        {
                            rowIndex++;
							latex_array.Add(new List<List<LatexExpression>>(3));
                        }
                    }
					if (latex_array[rowIndex].Count == 0)
                    {
						latex_array.RemoveAt(rowIndex);
                    }
                    #endregion
                    #region Link array
					main = new List<LatexExpression>(latex_array.Count);
					for (int j = 0; j < latex_array.Count; j++)
                    {
                        // Add row
                        main.Add(new LatexExpression("", outline[i], parentChildNumber, j, customization));
						for (int k = 0; k < latex_array[j].Count; k++)
                        {
                            // Add column cell
                            main[j].Expressions[0].Add(new LatexExpression("", main[j], 0, k, customization));
							for (int m = 0; m < latex_array[j][k].Count; m++)
                            {
								latex_array[j][k][m].Parent = main[j].Expressions[0][k];
								latex_array[j][k][m].ParentChildNumber = 0;
								latex_array[j][k][m].IndexInParentChild = m;
                                // Add cell atom
								main[j].Expressions[0][k].Expressions[0].Add(latex_array[j][k][m]);
                            }
                        }
                    }
                    outline[i].Expressions[parentChildNumber] = main;
                    #endregion
                }
            }
            for (int i = 0; i < outline.Count; i++)
            {
                if (outline[i].Expressions != null)
                {
                    foreach (var subTree in outline[i].Expressions)
                    {
                        PostParseArrays(subTree, customization);
                    }
                }
            }
        } 

        /// <summary>
        /// Important substitution rules which guarantee the failsafe work of the parser.
        /// </summary>
        public static readonly List<string[]> PreformatRules = new List<string[]>
        {
            new[] {"]\n", "] \n"},
            new[] {"}\n", "} \n"},
            new[] {"$\n", "$ \n"},
            new[] {@"\\", @"\\ "}
        };
    }

    /// <summary>
    /// The event arguments class to report the progress of the parse process.
    /// </summary>
    internal class ProgressEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the index of the current pass.
        /// </summary>
        public byte Index { get; private set; }

        /// <summary>
        /// Gets the count of the passes.
        /// </summary>
        public byte Count { get; private set; }

        /// <summary>
        /// Initializes a new instance of the ProgressEventArgs class.
        /// </summary>
        /// <param name="index">The index of the current pass.</param>
        /// <param name="count">The count of the passes.</param>
        public ProgressEventArgs(byte index, byte count)
        {
            Index = index;
            Count = count;
        }
    }
}
