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


		[Test]
		public void ConvertSymbolsGreekLettersReturnsCorrectMML() 
		{

			String latexExpression = @"$A, \alpha, B, \beta, \Gamma, \gamma, \Delta, \delta, E, \epsilon, \varepsilon, Z, \zeta, H, \eta, \Theta, \theta, \vartheta, I, \iota, K, \kappa, \varkappa, \Lambda, \lambda, M, \mu, N, \nu, \Xi, \xi, O, o, \Pi, \pi, \varpi, P, \rho, \varrho, \Sigma, \sigma, \varsigma, T, \tau, \Upsilon, \upsilon, \Phi, \phi, \varphi, X, \chi, \Psi, \psi, \Omega, \omega$";
			String latexToConvert = begin + latexExpression + end;
			latex2MathMLConverter = new LatexMathToMathMLConverter();
			latex2MathMLConverter.BeforeXmlFormat += ConvertSymbolsGreekLettersReturnsCorrectMMLListener;
			latex2MathMLConverter.Convert(latexToConvert);

			String expected = @"<math xmlns=""http://www.w3.org/1998/Math/MathML"" alttext=""A, \alpha, B, \beta, \Gamma, \gamma, \Delta, \delta, E, \epsilon, \varepsilon, Z, \zeta, H, \eta, \Theta, \theta, \vartheta, I, \iota, K, \kappa, \varkappa, \Lambda, \lambda, M, \mu, N, \nu, \Xi, \xi, O, o, \Pi, \pi, \varpi, P, \rho, \varrho, \Sigma, \sigma, \varsigma, T, \tau, \Upsilon, \upsilon, \Phi, \phi, \varphi, X, \chi, \Psi, \psi, \Omega, \omega"" display=""inline"" class=""normalsize"">
<mstyle displaystyle=""true"" /><mrow>
<mi>A</mi>
<mo>,</mo>
<mi>&#x3B1;<!-- &alpha; --></mi><mo>,</mo>
<mi>B</mi>
<mo>,</mo>
<mi>&#x3B2;<!-- &beta; --></mi><mo>,</mo>
<mi>&#x393;<!-- &Gamma; --></mi><mo>,</mo>
<mi>&#x3B3;<!-- &gamma; --></mi><mo>,</mo>
<mi>&#x394;<!-- &Delta; --></mi><mo>,</mo>
<mi>&#x3B4;<!-- &delta; --></mi><mo>,</mo>
<mi>E</mi>
<mo>,</mo>
<mi>&#x3B5;<!-- &epsilon; --></mi><mo>,</mo>
<mi>&#x3B5;<!-- &epsilon; --></mi><mo>,</mo>
<mi>Z</mi>
<mo>,</mo>
<mi>&#x3B6;<!-- &zeta; --></mi><mo>,</mo>
<mi>H</mi>
<mo>,</mo>
<mi>&#x3B7;<!-- &eta; --></mi><mo>,</mo>
<mi>&#x398;<!-- &Theta; --></mi><mo>,</mo>
<mi>&#x3B8;<!-- &theta; --></mi><mo>,</mo>
<mi>&#x03D1;<!-- &theta --></mi><mo>,</mo>
<mi>I</mi>
<mo>,</mo>
<mi>&#x3B9;<!-- &iota; --></mi><mo>,</mo>
<mi>K</mi>
<mo>,</mo>
<mi>&#x3BA;<!-- &kappa; --></mi><mo>,</mo>
<mi>&#x03F0;<!-- &kappav; --></mi><mo>,</mo>
<mi>&#x39b;<!-- &Lambda; --></mi><mo>,</mo>
<mi>&#x3BB;<!-- &lambda; --></mi><mo>,</mo>
<mi>M</mi>
<mo>,</mo>
<mi>&#x3BC;<!-- &mu; --></mi><mo>,</mo>
<mi>N</mi>
<mo>,</mo>
<mi>&#x3BD;<!-- &nu; --></mi><mo>,</mo>
<mi>&#x39e;<!-- &Xi; --></mi><mo>,</mo>
<mi>&#x3BE;<!-- &xi; --></mi><mo>,</mo>
<mi>O</mi>
<mo>,</mo>
<mi>o</mi>
<mo>,</mo>
<mi>&#x3a0;<!-- &Pi; --></mi><mo>,</mo>
<mi>&#x3C0;<!-- &pi; --></mi><mo>,</mo>
<mi>&#x03D6;<!-- &piv; --></mi><mo>,</mo>
<mi>P</mi>
<mo>,</mo>
<mi>&#x3C1;<!-- &rho; --></mi><mo>,</mo>
<mi>&#x03F1;<!--&rhov --></mi><mo>,</mo>
<mi>&#x3a3;<!--&Sigma --></mi><mo>,</mo>
<mi>&#x3C3;<!-- &sigma; --></mi><mo>,</mo>
<mi>&#x03C2;<!--&sigmav; --></mi><mo>,</mo>
<mi>T</mi>
<mo>,</mo>
<mi>&#x3C4;<!-- &tau; --></mi><mo>,</mo>
<mi>&#x3a5;<!-- &Upsilon; --></mi><mo>,</mo>
<mi>&#x3C5;<!-- &upsilon; --></mi><mo>,</mo>
<mi>&#x3a6;<!-- &Phi; --></mi><mo>,</mo>
<mi>&#x3c6;<!-- &phi; --></mi><mo>,</mo>
<mi>&#x03D5;<!-- &phiv; --></mi><mo>,</mo>
<mi>X</mi>
<mo>,</mo>
<mi>&#x3C7;<!-- &chi; --></mi><mo>,</mo>
<mi>&#x3a8;<!-- &Psi; --></mi><mo>,</mo>
<mi>&#x3C8;<!-- &psi; --></mi><mo>,</mo>
<mi>&#x3a9;<!-- &Omega; --></mi><mo>,</mo>
<mi>&#x3C9;<!-- &omega; --></mi></mrow>
</math>";
			Thread.Sleep(400);


			NUnit.Framework.Assert.That(result, Is.EqualTo(expected));

		}

		void ConvertSymbolsGreekLettersReturnsCorrectMMLListener(object sender, EventArgs e)
		{
			result = latex2MathMLConverter.Output;

		}



	}

}

