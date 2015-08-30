using NUnit.Framework;
using System;
using System.Threading;

namespace LatexMath2MathML
{
	[TestFixture]
	public class Tests
	{
		String begin = @"\begin{document} ";
		String end = @" \end{document}";
		String result;
		LatexMathToMathMLConverter latex2MathMLConverter;
		[Test]
		public void ConvertXSquaredReturnXSquaredInMML()
		{
			String latexExpression = "$x^2$";
			String latexToConvert = begin + latexExpression + end;
			latex2MathMLConverter= new LatexMathToMathMLConverter();
			latex2MathMLConverter.BeforeXmlFormat += ConvertXSquaredReturnXSquaredInMMLEventListener;
			latex2MathMLConverter.Convert(latexToConvert);

			String expected = @"<math xmlns=""http://www.w3.org/1998/Math/MathML"" alttext=""x^2"" display=""inline"" class=""normalsize"">
<mstyle displaystyle=""true"" /><mrow>
<msup>
<mrow>
<mi>x</mi>
</mrow>
<mrow>
<mn>2</mn>
</mrow>
</msup>
</mrow>
</math>";
			Thread.Sleep(400);
			Console.WriteLine(result);
			Console.WriteLine(expected);
			NUnit.Framework.Assert.That(result, Is.EqualTo(expected));
		}
		void ConvertXSquaredReturnXSquaredInMMLEventListener(object sender, EventArgs e)
		{
			result = latex2MathMLConverter.Output;


		}

		[Test]
		public void ConvertFracXYPlus2ReturnsFracXYPlus2InMML()
		{
			String latexExpression = @"$\frac{x}{y}+2$";
			String latexToConvert = begin + latexExpression + end;
			latex2MathMLConverter = new LatexMathToMathMLConverter();
			latex2MathMLConverter.BeforeXmlFormat += ConvertFracXYPlus2ReturnsFracXYPlus2InMMLEventListener;
			latex2MathMLConverter.Convert(latexToConvert);
			String expected = @"<math xmlns=""http://www.w3.org/1998/Math/MathML"" alttext=""\frac{x}{y}+2"" display=""inline"" class=""normalsize"">
<mstyle displaystyle=""true"" /><mrow>
<mfrac>
<mrow>
<mi>x</mi>
</mrow>
<mrow>
<mi>y</mi>
</mrow>
</mfrac>
<mo>+</mo>
<mn>2</mn>
</mrow>
</math>";
			Thread.Sleep(400);

			NUnit.Framework.Assert.That(result, Is.EqualTo(expected));

		}

		void ConvertFracXYPlus2ReturnsFracXYPlus2InMMLEventListener(object sender, EventArgs e)
		{

			result = latex2MathMLConverter.Output;
			Console.WriteLine(result);

			// NUnit.Framework.Assert.That(result, Is.EqualTo(expected));


		}


		[Test]
		public void ConvertOperatorsSinCosReturnsSinCos() 
		{
			String latexExpression = @"$\cos (2\theta) = \cos^2 \theta - \sin^2 \theta$";
			String latexToConvert = begin + latexExpression + end;
			latex2MathMLConverter = new LatexMathToMathMLConverter();
			latex2MathMLConverter.BeforeXmlFormat += ConvertFracXYPlus2ReturnsFracXYPlus2InMMLEventListener;
			latex2MathMLConverter.Convert(latexToConvert);

			String expected = @"<math xmlns=""http://www.w3.org/1998/Math/MathML"" alttext=""\cos (2\theta) = \cos^2 \theta - \sin^2 \theta"" display=""inline"" class=""normalsize"">
<mstyle displaystyle=""true"" /><mrow>
<mi>cos</mi>
<mfenced>
<mrow>
<mn>2</mn>
<mi>&#x3B8;<!-- &theta; --></mi></mrow>
</mfenced>
<mo>=</mo>
<msup>
<mrow>
<mi>cos</mi>
</mrow>
<mrow>
<mn>2</mn>
</mrow>
</msup>
<mi>&#x3B8;<!-- &theta; --></mi><mo>-</mo>
<msup>
<mrow>
<mi>sin</mi>
</mrow>
<mrow>
<mn>2</mn>
</mrow>
</msup>
<mi>&#x3B8;<!-- &theta; --></mi></mrow>
</math>";
			Thread.Sleep(400);


			NUnit.Framework.Assert.That(result, Is.EqualTo(expected));

		}

		void ConvertOperatorsSinCosReturnsSinCosListener(object sender, EventArgs e)
		{
			result = latex2MathMLConverter.Output;

		}
		[Test]
		public void ConvertSymbolsForAllInQuadExistsLeqEpsilonLeGeqGeReturnsCorrectMML() 
		{

			String latexExpression = @"$\forall x \in X, \quad \exists y \leq \epsilon \le \geq \ge$";
			String latexToConvert = begin + latexExpression + end;
			latex2MathMLConverter = new LatexMathToMathMLConverter();
			latex2MathMLConverter.BeforeXmlFormat += ConvertSymbolsForAllInQuadExistsLeqEpsilonReturnsCorrectMMLListener;
			latex2MathMLConverter.Convert(latexToConvert);

			String expected = @"<math xmlns=""http://www.w3.org/1998/Math/MathML"" alttext=""\forall x \in X, \quad \exists y \leq \epsilon \le \geq \ge"" display=""inline"" class=""normalsize"">
<mstyle displaystyle=""true"" /><mrow>
<mi>&#x2200;<!-- &forall; --></mi>
<mi>x</mi>
<mi>&#x2208;<!-- &isin; --></mi>
<mi>X</mi>
<mo>,</mo>
<mspace width=""2em""/><mi>&#x2203;<!-- &exist; --></mi>
<mi>y</mi>
<mi>&#8804; <!-- leq --> </mi> 
<mi>&#x3B5;<!-- &epsilon; --></mi><mi>&#8804; <!-- le --> </mi> 
<mi>&#8805; <!-- geq --> </mi> 
<mi>&#8805; <!-- ge --> </mi> 
</mrow>
</math>";
			Thread.Sleep(400);


			NUnit.Framework.Assert.That(result, Is.EqualTo(expected));

		}

		void ConvertSymbolsForAllInQuadExistsLeqEpsilonReturnsCorrectMMLListener(object sender, EventArgs e)
		{
			result = latex2MathMLConverter.Output;

		}






	}

}

