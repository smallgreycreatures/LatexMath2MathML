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
using System.Text;

namespace LatexMath2MathML
{
    /// <summary>
    /// The converter class for the document object tree root.
    /// </summary>
    internal sealed class RootConverter : BaseConverter
    {
#pragma warning disable 169
        private static readonly log4net.ILog Log =
#pragma warning restore 169
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Gets the type of the corresponding expression (ExpressionType.Root).
        /// </summary>
        public override ExpressionType ExprType
        {
            get { return ExpressionType.Root; }
        }
			
        /// <summary>
        /// Performs the conversion procedure.
        /// </summary>
        /// <param name="expr">The expression to convert.</param>
        /// <returns>The conversion result.</returns>
        public override string Convert(LatexExpression expr)
        {
			var bodyBuilder = new StringBuilder();

            // Convert the {document} block
            LatexExpression documentExpression = expr.FindDocument();
            if (documentExpression != null)
            {
                try
                {
                    bodyBuilder.Append(documentExpression.Convert());
                }
                // ReSharper disable RedundantCatchClause
#pragma warning disable 168
                catch (Exception e)
#pragma warning restore 168
                {
#if DEBUG
                    throw;
#else
                Log.Error("Failed to convert the document block", e);
#endif
                }
                // ReSharper restore RedundantCatchClause
                // ReSharper restore RedundantCatchClause
            }

			return bodyBuilder.ToString();
        }
			
    }
}
