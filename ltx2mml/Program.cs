/*  
    This file is part of Latex2MathML.

    Latex2MathML is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    Latex2MathML is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with Latex2MathML.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Text;
using LatexMath2MathML;

namespace ltx2mml
{
    class Program
    {
        static void Main(string[] args)
        {

			Program program = new Program ();
			program.Convert ();
        }



		LatexMathToMathMLConverter lmm;

		public void Convert() {
			String latexExpression = @"\begin{document} $ \sum_{i=1}^{10} t_i $ \end{document}";
			lmm = new LatexMathToMathMLConverter(
				latexExpression);
			lmm.ValidateResult = true;
			lmm.BeforeXmlFormat += MyEventListener;
			lmm.Convert(); 

		}
		private void MyEventListener(object sender, EventArgs e) 
		{
			//Console.WriteLine("called .");
			String output = lmm.output;
			Console.WriteLine (output);
		}

    }
}
