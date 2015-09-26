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
<mo>&#x2200;<!-- &forall; --></mo>
<mi>x</mi>
<mo>&#x2208;<!-- &isin; --></mo>
<mi>X</mi>
<mo>,</mo>
<mspace width=""2em""/><mo>&#x2203;<!-- &exist; --></mo>
<mi>y</mi>
<mo>&#8804; <!-- leq --> </mo> 
<mi>&#x3B5;<!-- &epsilon; --></mi><mo>&#8804; <!-- le --> </mo> 
<mo>&#8805; <!-- geq --> </mo> 
<mo>&#8805; <!-- ge --> </mo> 
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


		[Test]
		public void ConvertOperatorsWhitespacesReturnsMMLWithCorrectSpacing() 
		{
			String latexExpression = @"$A,\qquad B, \quad C, \; D, \: E, \, F, \! G $";
			String latexToConvert = begin + latexExpression + end;
			latex2MathMLConverter = new LatexMathToMathMLConverter();
			latex2MathMLConverter.BeforeXmlFormat += ConvertOperatorsWhitespacesReturnsMMLWithCorrectSpacingListener;
			latex2MathMLConverter.Convert(latexToConvert);

			String expected = @"<math xmlns=""http://www.w3.org/1998/Math/MathML"" alttext=""A,\qquad B, \quad C, \; D, \: E, \, F, \! G "" display=""inline"" class=""normalsize"">
<mstyle displaystyle=""true"" /><mrow>
<mi>A</mi>
<mo>,</mo>
<mspace width=""4em""/><mi>B</mi>
<mo>,</mo>
<mspace width=""2em""/><mi>C</mi>
<mo>,</mo>
<mspace width=""1.1em""/><mi>D</mi>
<mo>,</mo>
<mspace width=""0.9em""/><mi>E</mi>
<mo>,</mo>
<mspace width=""0.7em""/><mi>F</mi>
<mo>,</mo>
<mspace width=""0.3em""/><mi>G</mi>
</mrow>
</math>";
			Thread.Sleep(400);


			NUnit.Framework.Assert.That(result, Is.EqualTo(expected));

		}

		void ConvertOperatorsWhitespacesReturnsMMLWithCorrectSpacingListener(object sender, EventArgs e)
		{
			result = latex2MathMLConverter.Output;

		}

		/**
		 * Tests all relationships symbols mentioned on https://en.wikibooks.org/wiki/LaTeX/Mathematics
		 * **/
		[Test]
		public void ConvertOperatorsRelationShipOperatorsReturnsCorrectMML() 
		{
			String latexExpression = @"$< <= \leq \ll \subset \subseteq \nsubseteq \sqsubset \sqsubseteq \preceq > >=\geq \gg \supset \supseteq \nsupseteq \sqsupset \sqsupseteq \succeq = \doteq \equiv \approx \cong \simeq \sim \propto \neq \parallel \asymp \vdash \in \smile \models \perp \prec \sphericalangle$";
			String latexToConvert = begin + latexExpression + end;
			latex2MathMLConverter = new LatexMathToMathMLConverter();
			latex2MathMLConverter.BeforeXmlFormat += ConvertOperatorsRelationShipOperatorsReturnsCorrectMMLListener;
			latex2MathMLConverter.Convert(latexToConvert);

			String expected = @"<math xmlns=""http://www.w3.org/1998/Math/MathML"" alttext=""&amp;lt; &amp;lt;= \leq \ll \subset \subseteq \nsubseteq \sqsubset \sqsubseteq \preceq &amp;gt; &amp;gt;=\geq \gg \supset \supseteq \nsupseteq \sqsupset \sqsupseteq \succeq = \doteq \equiv \approx \cong \simeq \sim \propto \neq \parallel \asymp \vdash \in \smile \models \perp \prec \sphericalangle"" display=""inline"" class=""normalsize"">
<mstyle displaystyle=""true"" /><mrow>
<mo>&lt;</mo>
<mo>&le;</mo>
<mo>=</mo>
<mo>&#8804; <!-- leq --> </mo> 
<mo>&#x226A;<!-- &Lt; --></mo>
<mo>&#x2282;<!-- &sub; --></mo>
<mo>&#x2286;<!-- &sube; --></mo>
<mo>&#x2288;<!-- &nsube; --></mo>
<mo>&#x228F;<!-- &sqsub; --></mo>
<mo>&#x2291;<!-- &sqsube; --></mo>
<mo>&#x227C;<!-- &cupre; --></mo>
<mo>&gt;</mo>
<mo>&ge;</mo>
<mo>=</mo>
<mo>&#8805; <!-- geq --> </mo> 
<mo>&#x226B;<!-- &Gt; --></mo>
<mo>&#x2283;<!-- &sup; --></mo>
<mo>&#x2287;<!-- &supe; --></mo>
<mo>&#x2289;<!-- &nsupe; --></mo>
<mo>&#x2290;<!-- &sqsup; --></mo>
<mo>&#x2292;<!-- &sqsupe; --></mo>
<mo>&#x227D;<!-- &sccue; --></mo>
<mo>=</mo>
<mo>&#x2250;<!-- &esdot; --></mo>
<mo>&#x2261;<!-- equiv --></mo>
<mo>&#x2248;<!-- &asymp; --></mo>
<mo>&#x2245;<!-- &cong; --></mo>
<mo>&#x2243;<!-- &sime; --></mo>
<mo>&#x223C;<!-- &sim; --></mo>
<mo>&#x221D;<!-- &vprop; --></mo>
<mo>&#x2260;<!-- &ne; --></mo>
<mo>&#x20E6;</mo>
<mo>&#x224d;<!-- &asymp; --></mo>
<mo>&#x22A2;<!-- &vdash; --></mo>
<mo>&#x2208;<!-- &isin; --></mo>
<mo>&#x23DD;</mo>
<mo>&#x22A7;<!-- &models; --></mo>
<mo>&#x22A5;<!-- &bottom; --></mo>
<mo>&#x227A;<!-- &pr; --></mo>
<mo>&#x2222;<!-- &angsph; --></mo>
</mrow>
</math>";
			Thread.Sleep(400);


			NUnit.Framework.Assert.That(result, Is.EqualTo(expected));

		}

		void ConvertOperatorsRelationShipOperatorsReturnsCorrectMMLListener(object sender, EventArgs e)
		{
			result = latex2MathMLConverter.Output;

		}

		[Test]
		public void ConvertOperatorsDotsReturnsCorrectMML() 
		{
			String latexExpression = @"$\dots \dotsm \vdots \ddots$";
			String latexToConvert = begin + latexExpression + end;
			latex2MathMLConverter = new LatexMathToMathMLConverter();
			latex2MathMLConverter.BeforeXmlFormat += ConvertOperatorsDotsReturnsCorrectMMLListener;
			latex2MathMLConverter.Convert(latexToConvert);

			String expected = @"<math xmlns=""http://www.w3.org/1998/Math/MathML"" alttext=""\dots \dotsm \vdots \ddots"" display=""inline"" class=""normalsize"">
<mstyle displaystyle=""true"" /><mrow>
<mo>&hellip;</mo>
<mo>&ctdot;</mo>
<mo>&dtdot;</mo>
<mo>&vellip;</mo>
</mrow>
</math>";
			Thread.Sleep(400);


			NUnit.Framework.Assert.That(result, Is.EqualTo(expected));

		}

		void ConvertOperatorsDotsReturnsCorrectMMLListener(object sender, EventArgs e)
		{
			result = latex2MathMLConverter.Output;

		}

		/**
		 * Tests all the binary operators mentioned on https://en.wikibooks.org/wiki/LaTeX/Mathematics
		 * 
		 * **/
		[Test]
		public void ConvertOperatorsBinaryOperators() 
		{
			String latexExpression = @"$\pm \mp \times \div \ast \star \dagger \ddagger \cap \cup \uplus \sqcap \sqcup \vee \wedge \cdot \diamond \bigtriangleup \bigtriangledown \triangleleft \triangledown \bigcirc \bullet \wr \oplus \ominus \otimes \oslash \odot \circ \setminus \amalg$";
			String latexToConvert = begin + latexExpression + end;
			latex2MathMLConverter = new LatexMathToMathMLConverter();
			latex2MathMLConverter.BeforeXmlFormat += ConvertOperatorsBinaryOperatorsListener;
			latex2MathMLConverter.Convert(latexToConvert);

			String expected = @"<math xmlns=""http://www.w3.org/1998/Math/MathML"" alttext=""\dots \dotsm \vdots \ddots"" display=""inline"" class=""normalsize"">
<mstyle displaystyle=""true"" /><mrow>
<mo>&hellip;</mo>
<mo>&ctdot;</mo>
<mo>&dtdot;</mo>
<mo>&vellip;</mo>
</mrow>
</math>";
			Thread.Sleep(400);


			NUnit.Framework.Assert.That(result, Is.EqualTo(expected));

		}

		void ConvertOperatorsBinaryOperatorsListener(object sender, EventArgs e)
		{
			result = latex2MathMLConverter.Output;

		}
	
	}

}

