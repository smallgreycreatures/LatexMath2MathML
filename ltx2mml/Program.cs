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
			String latexExpression = @"\begin{document} $\frac{\mathrm d}{\mathrm d x} \big( k g(x \big)$ \end{document}";
			lmm = new LatexMathToMathMLConverter();
			lmm.ValidateResult = true;
			lmm.BeforeXmlFormat += MyEventListener;
			lmm.ExceptionEvent += ExceptionListener;
			lmm.Convert(latexExpression); 

		}

		private void ExceptionListener(object sender, ExceptionEventArgs e) {
			Console.WriteLine ("Exception handler called");

			String message = e.Message;
			Console.WriteLine (message);
		}

		private void MyEventListener(object sender, EventArgs e) 
		{
			//Console.WriteLine("called .");
			String output = lmm.Output;
			Console.WriteLine (output);
		}

    }
}
