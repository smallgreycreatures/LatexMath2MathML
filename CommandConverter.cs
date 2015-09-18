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

namespace LatexMath2MathML
{
    /// <summary>
    /// The proxy class between a command and the corresponding converter.
    /// </summary>
    internal class CommandConverter : NamedConverter
    {
        /// <summary>
        /// The hash table of command converters.
        /// </summary>
        public static readonly IDictionary<int, CommandConverter> CommandConverters = new Dictionary<int, CommandConverter>
        {
            #region Initialization
            {(new FracCommandConverter()).GetHashCode(), new FracCommandConverter()},
			{(new DfracCommandConverter()).GetHashCode(), new DfracCommandConverter()},
			{(new TfracCommandConverter()).GetHashCode(), new TfracCommandConverter()},
            {(new SqrtCommandConverter()).GetHashCode(), new SqrtCommandConverter()},            
            //{(new ItemCommandConverter()).GetHashCode(), new ItemCommandConverter()},

			//TODO WIll we need these in an equation editor? Probably not
            {(new OverlineAccentConverter()).GetHashCode(), new OverlineAccentConverter()},
            {(new HatAccentConverter()).GetHashCode(), new HatAccentConverter()},
            {(new WidehatAccentConverter()).GetHashCode(), new WidehatAccentConverter()},
            {(new CheckAccentConverter()).GetHashCode(), new CheckAccentConverter()},
            {(new TildeAccentConverter()).GetHashCode(), new TildeAccentConverter()},
            {(new WidetildeAccentConverter()).GetHashCode(), new WidetildeAccentConverter()},
            {(new AcuteAccentConverter()).GetHashCode(), new AcuteAccentConverter()},
            {(new GraveAccentConverter()).GetHashCode(), new GraveAccentConverter()},
            {(new DotAccentConverter()).GetHashCode(), new DotAccentConverter()},
            {(new DdotAccentConverter()).GetHashCode(), new DdotAccentConverter()},
            {(new BreveAccentConverter()).GetHashCode(), new BreveAccentConverter()},
            {(new BarAccentConverter()).GetHashCode(), new BarAccentConverter()},
            {(new VecAccentConverter()).GetHashCode(), new VecAccentConverter()},
            {(new HatTextAccentConverter()).GetHashCode(), new HatTextAccentConverter()},
            {(new CheckTextAccentConverter()).GetHashCode(), new CheckTextAccentConverter()},
            {(new TildeTextAccentConverter()).GetHashCode(), new TildeTextAccentConverter()},
            {(new AcuteTextAccentConverter()).GetHashCode(), new AcuteTextAccentConverter()},
            {(new GraveTextAccentConverter()).GetHashCode(), new GraveTextAccentConverter()},
            {(new DotTextAccentConverter()).GetHashCode(), new DotTextAccentConverter()},
            {(new DdotTextAccentConverter()).GetHashCode(), new DdotTextAccentConverter()},
            {(new BreveTextAccentConverter()).GetHashCode(), new BreveTextAccentConverter()},
            {(new BarTextAccentConverter()).GetHashCode(), new BarTextAccentConverter()},

            {(new StateCommandConverter()).GetHashCode(), new StateCommandConverter()},
            {(new StatexCommandConverter()).GetHashCode(), new StatexCommandConverter()},
            {(new ProcedureCommandConverter()).GetHashCode(), new ProcedureCommandConverter()},
            {(new EndProcedureCommandConverter()).GetHashCode(), new EndProcedureCommandConverter()},
            {(new MathcalCommandConverter()).GetHashCode(), new MathcalCommandConverter()},
            
			#endregion
        };


        public static readonly Dictionary<string, string> CommandConstants = new Dictionary<string, string>
        {
            #region Initialization
			{"\\", "\n</mrow>\n<mrow>\n"},
			{"footnoterule", "\n</mrow>\n<mrow>\n"},
			{"Alpha", "&#x391;<!-- &Alpha; -->"},
            {"alpha", "&#x3B1;<!-- &alpha; -->"},
			{"Beta", "&#x392;<!-- &Beta; -->"},
            {"beta", "&#x3B2;<!-- &beta; -->"},
			{"Gamma", "&#x393;<!-- &Gamma; -->"},
            {"gamma", "&#x3B3;<!-- &gamma; -->"},
			{"Delta", "&#x394;<!-- &Delta; -->"},
            {"delta", "&#x3B4;<!-- &delta; -->"},
			{"Epsilon", "&#x395;<!--&Epsilon; -->"},
            {"epsilon", "&#x3B5;<!-- &epsilon; -->"},
            {"varepsilon", "&#x3B5;<!-- &epsilon; -->"},
			{"Zeta", "&#x396;<!-- &Zeta; -->"},
            {"zeta", "&#x3B6;<!-- &zeta; -->"},
			{"Eta", "&#x397;<!-- &Eta; -->"},
            {"eta", "&#x3B7;<!-- &eta; -->"},
			{"Theta", "&#x398;<!-- &Theta; -->"},
            {"theta", "&#x3B8;<!-- &theta; -->"},
			{"vartheta", "&#x03D1;<!-- &theta -->"},
			{"Iota", "&#x399;<!-- &Iota; -->"},
            {"iota", "&#x3B9;<!-- &iota; -->"},
			{"Kappa", "&#x39a;<!-- &Kappa; -->"},
            {"kappa", "&#x3BA;<!-- &kappa; -->"},
			{"varkappa", "&#x03F0;<!-- &kappav; -->"},
			{"Lambda", "&#x39b;<!-- &Lambda; -->"},
            {"lambda", "&#x3BB;<!-- &lambda; -->"},
			{"Mu", "&#x39c;<!-- &Mu; -->"},
            {"mu", "&#x3BC;<!-- &mu; -->"},
            {"nu", "&#x3BD;<!-- &nu; -->"},
			{"Xi", "&#x39e;<!-- &Xi; -->"},
            {"xi", "&#x3BE;<!-- &xi; -->"},
            {"omicron", "&#x3BF;<!-- &omicron; -->"},
			{"Pi", "&#x3a0;<!-- &Pi; -->"},
            {"pi", "&#x3C0;<!-- &pi; -->"},
			{"varpi", "&#x03D6;<!-- &piv; -->"},
            {"rho", "&#x3C1;<!-- &rho; -->"},
			{"varrho", "&#x03F1;<!--&rhov -->"},
			{"Sigma", "&#x3a3;<!--&Sigma -->"},
            {"sigma", "&#x3C3;<!-- &sigma; -->"},
			{"varsigma", "&#x03C2;<!--&sigmav; -->"},
            {"tau", "&#x3C4;<!-- &tau; -->"},
			{"Upsilon", "&#x3a5;<!-- &Upsilon; -->"},
            {"upsilon", "&#x3C5;<!-- &upsilon; -->"},
			{"Phi", "&#x3a6;<!-- &Phi; -->"},
			{"phi", "&#x3c6;<!-- &phi; -->"},
			{"varphi", "&#x03D5;<!-- &phiv; -->"},
            {"chi", "&#x3C7;<!-- &chi; -->"},
			{"Psi", "&#x3a8;<!-- &Psi; -->"},
            {"psi", "&#x3C8;<!-- &psi; -->"},
			{"Omega", "&#x3a9;<!-- &Omega; -->"},
            {"omega", "&#x3C9;<!-- &omega; -->"},
            {"dag", "&#x2020;<!-- &dagger; -->"},
            {"ddag", "&#x2021;<!-- &Dagger; -->"},
            {"pounds", "&#x00A3;<!-- &pound; -->"},
            {"textsterling", "&#x00A3;<!-- &pound; -->"},
            {"euro", "&#x20AC;<!-- &euro; -->"},
            {"EUR", "&#x20AC;<!-- &euro; -->"},
            {"S", "&#x00A7;<!-- &sect; -->"},
            {"hline", "<hr />"},
            {"vert", "|"},
            {"~", "~"},
            #endregion
        };

        public static readonly Dictionary<string, string> MathFunctionsCommandConstants = new Dictionary<string, string>
        {          
            #region Initialization
            {"sin", "<mi>sin</mi>\n"},
            {"cos", "<mi>cos</mi>\n"},
            {"tan", "<mi>tan</mi>\n"},
            {"sec", "<mi>sec</mi>\n"},
            {"csc", "<mi>csc</mi>\n"},
            {"cot", "<mi>cot</mi>\n"},
            {"sinh", "<mi>sinh</mi>\n"},
            {"cosh", "<mi>cosh</mi>\n"},
            {"tanh", "<mi>tanh</mi>\n"},
            {"sech", "<mi>sech</mi>\n"},
            {"csch", "<mi>csch</mi>\n"},
            {"coth", "<mi>coth</mi>\n"},
            {"arcsin", "<mi>arcsin</mi>\n"},
            {"arccos", "<mi>arccos</mi>\n"},
            {"arctan", "<mi>arctan</mi>\n"},
            {"arccosh", "<mi>arccosh</mi>\n"},
            {"arccot", "<mi>arccot</mi>\n"},
            {"arccoth", "<mi>arccoth</mi>\n"},
            {"arccsc", "<mi>arccsc</mi>\n"},
            {"arccsch", "<mi>arccsch</mi>\n"},
            {"arcsec", "<mi>arcsec</mi>\n"},
            {"arcsech", "<mi>arcsech</mi>\n"},
            {"arcsinh", "<mi>arcsinh</mi>\n"},
            {"arctanh", "<mi>arctanh</mi>\n"},             
            #endregion
        };

        public static readonly Dictionary<string, string> MathFunctionsScriptCommandConstants = new Dictionary<string, string>
        {
            #region Initialization
            {"int", "<mo>&#x222B;<!-- &int; --></mo>\n"},                                  
            {"iint", "<mo>&#x222C;<!-- iint --></mo>\n"},
            {"iiint", "<mo>&#x222D;<!-- iiint --></mo>\n"},
            {"oint", "<mo>&#x222E;<!-- oint --></mo>\n"}, 
            {"oiint", "<mo>&#x222F;<!-- oiint --></mo>\n"},
            {"oiiint", "<mo>&#x2230;<!-- oiiint --></mo>\n"},
            {"ointclockwise", "<mo>&#x2232;<!-- ointclockwise --></mo>\n"},
            {"ointctrclockwise", "<mo>&#x2233;<!-- ointctrclockwise --></mo>\n"},   

            {"lim", "<mo>lim</mo>\n"},
            {"sup", "<mo>sup</mo>\n"},
            {"inf", "<mo>inf</mo>\n"},
            {"min", "<mo>min</mo>\n"},
            {"max", "<mo>max</mo>\n"},
            {"ker", "<mo>ker</mo>\n"},
            {"sum", "<mo>&#x2211;<!-- &sum; --></mo>\n"}
            #endregion
        };

        public static readonly Dictionary<string, string> MathCommandConstants = new Dictionary<string, string>
        {
            #region Initialization
            {"displaystyle", ""},
                      
            {"neq", "<mo>&#x2260;<!-- &ne; --></mo>\n"},
            {"equiv", "<mo>&#x2261;<!-- equiv --></mo>\n"},
			{"leq", "<mo>&#8804; <!-- leq --> </mo> \n"},
			{"le", "<mo>&#8804; <!-- le --> </mo> \n"},
			{"geq", "<mo>&#8805; <!-- geq --> </mo> \n"},
			{"ge", "<mo>&#8805; <!-- ge --> </mo> \n"},
            {"sim", "<mo>&#x223C;<!-- &sim; --></mo>\n"},
            {"approx", "<mo>&#x2248;<!-- &asymp; --></mo>\n"},
            {"cap", "<mo>&#x2229;<!-- &cap; --></mo>\n"},
            {"cup", "<mo>&#x2230;<!-- &cup; --></mo>\n"},                      
            {"in", "<mo>&#x2208;<!-- &isin; --></mo>\n"},
            {"notin", "<mo>&#x2209;<!-- &notin; --></mo>\n"},
            {"ni", "<mo>&#x220B;<!-- &ni; --></mo>\n"},
            {"forall", "<mo>&#x2200;<!-- &forall; --></mo>\n"},
            {"infty", "<mo>&#x221E;<!-- &infty; --></mo>\n"},
            {"exists", "<mo>&#x2203;<!-- &exist; --></mo>\n"},
            {"nexists", "<mo>&#x2204;<!-- &nexists --></mo>\n"},
            {"to", "<mo>&#x2192;<!-- &rarr; --></mo>\n"},
			{"ll", "<mo>&#x226A;<!-- &Lt; --></mo>\n"},
			{"gg", "<mo>&#x226B;<!-- &Gt; --></mo>\n"},
			{"subset", "<mo>&#x2282;<!-- &sub; --></mo>\n"},
			{"subseteq", "<mo>&#x2286;<!-- &sube; --></mo>\n"},
			{"nsubseteq", "<mo>&#x2288;<!-- &nsube; --></mo>\n"},
			{"supset", "<mo>&#x2283;<!-- &sup; --></mo>\n"},
			{"supseteq", "<mo>&#x2287;<!-- &supe; --></mo>\n"},
			{"nsupseteq", "<mo>&#x2289;<!-- &nsupe; --></mo>\n"},
			{"sqsubset", "<mo>&#x228F;<!-- &sqsub; --></mo>\n"},
			{"sqsubseteq", "<mo>&#x2291;<!-- &sqsube; --></mo>\n"},
			{"sqsupset", "<mo>&#x2290;<!-- &sqsup; --></mo>\n"},
			{"sqsupseteq", "<mo>&#x2292;<!-- &sqsupe; --></mo>\n"},
			{"preceq", "<mo>&#x227C;<!-- &cupre; --></mo>\n"},
			{"succeq", "<mo>&#x227D;<!-- &sccue; --></mo>\n"},
			{"doteq", "<mo>&#x2250;<!-- &esdot; --></mo>\n"},
			{"cong", "<mo>&#x2245;<!-- &cong; --></mo>\n"},
			{"simeq", "<mo>&#x2243;<!-- &sime; --></mo>\n"},
			{"propto", "<mo>&#x221D;<!-- &vprop; --></mo>\n"},
			{"parallel", "<mo>&#x20E6;</mo>\n"},
			{"asymp", "<mo>&#x224d;<!-- &asymp; --></mo>\n"},
			{"vdash", "<mo>&#x22A2;<!-- &vdash; --></mo>\n"},
			{"smile", "<mo>&#x23DD;</mo>\n"},
			{"models", "<mo>&#x22A7;<!-- &models; --></mo>\n"},
			{"perp", "<mo>&#x22A5;<!-- &bottom; --></mo>\n"},
			{"prec", "<mo>&#x227A;<!-- &pr; --></mo>\n"},
			{"sphericalangle", "<mo>&#x2222;<!-- &angsph; --></mo>\n"},
			{"pm", "<mo>&#xB1;<!-- pm --></mo>\n"},
			{"mp", "<mo>&#x2213;<!-- mp --></mo>\n"},
			{"times","<mo></mo>\n"},
			{"div", "<mo></mo>\n"},
			{"ast", "<mo></mo>\n"},
			{"star", "<mo></mo>\n"},
			{"dagger", "<mo></mo>\n"},
			{"ddagger", "<mo></mo>\n"},


			{"uplus", "<mo></mo>\n"},
			{"sqcap", "<mo></mo>\n"},
			{"sqcup", "<mo></mo>\n"},
			{"vee", "<mo></mo>\n"},
			{"wedge", "<mo></mo>\n"},
			{"cdot", "<mo></mo>\n"},
			{"diamond", "<mo>&#x22CF <!-- &diam; --> </mo>\n"},
			{"bigtriangleup", "<mo></mo>\n"},
			{"bigtriangledown", "<mo></mo>\n"},
			{"triangleleft", "<mo></mo>\n"},
			{"triangleright", "<mo></mo>\n"},
			{"bigcirc", "<mo></mo>\n"},
			{"bullet", "<mo></mo>\n"},
			{"wr", "<mo></mo>\n"},
			{"oplus", "<mo>&#x2295<!-- &oplus --></mo>\n"},
			{"ominus", "<mo>&#x2296<!-- &ominus --></mo>\n"},
			{"otimes", "<mo>&#x2297<!-- &otimes --></mo>\n"},
			{"oslash", "<mo>&#x2298<!-- &oslash --></mo>\n"},
			{"odot", "<mo>&#x2299<!-- &odot --></mo>\n"},
			{"circ", "<mo></mo>\n"},
			{"setminus", "<mo></mo>\n"},
			{"amalg", "<mo></mo>\n"},

            {"quad", "<mspace width=\"2em\"/>"},
            {"qquad", "<mspace width=\"4em\"/>"},
            {";", "<mspace width=\"1.1em\"/>"},
            {":", "<mspace width=\"0.9em\"/>"},
			{",", "<mspace width=\"0.7em\"/>"},
			{"!", "<mspace width=\"0.3em\"/>"}, //Should be negative space... but is not
            {"Leftrightarrow", "<mo>&#x21D4;<!-- &hArr; --></mo>\n"},
            {"Updownarrow", "<mo>&#x21D5;<!-- \\Updownarrow --></mo>\n"},
            {"Downarrow", "<mo>&#x21D3;<!-- &dArr; --></mo>\n"},
            {"Rightarrow", "<mo>&#x21D2;<!-- &rArr; --></mo>\n"},
            {"Uparrow", "<mo>&#x21D1;<!-- &uArr; --></mo>\n"},
            {"Leftarrow", "<mo>&#x21D0;<!-- &lArr; --></mo>\n"},
            {"nRightarrow", "<mo>&#x21CF;<!-- \\nRightarrow --></mo>\n"},
            {"nLeftrightarrow", "<mo>&#x21CE;<!-- \\nLeftrightarrow --></mo>\n"},
            {"nLeftarrow", "<mo>&#x21CD;<!-- \\nLeftarrow --></mo>\n"},
            {"rightleftharpoons", "<mo>&#x21CC;<!-- \\rightleftharpoons --></mo>\n"},
            {"leftrightharpoons", "<mo>&#x21CB;<!-- \\leftrightharpoons --></mo>\n"},
            {"downdownarrows", "<mo>&#x21CA;<!-- \\downdownarrows --></mo>\n"},
            {"rightrightarrows", "<mo>&#x21C9;<!-- \\rightrightarrows --></mo>\n"},
            {"upuparrows", "<mo>&#x21C8;<!-- \\upuparrows --></mo>\n"},
            {"leftleftarrows", "<mo>&#x21C7;<!-- \\leftleftarrows --></mo>\n"},
            {"leftrightarrows", "<mo>&#x21C6;<!-- \\leftrightarrows --></mo>\n"},
            {"updownarrows", "<mo>&#x21C5;<!-- \\updownarrows --></mo>\n"},
            {"rightleftarrows", "<mo>&#x21C4;<!-- \\rightleftarrows --></mo>\n"},
            {"downharpoonleft", "<mo>&#x21C3;<!-- \\downharpoonleft --></mo>\n"},
            {"downharpoonright", "<mo>&#x21C2;<!-- \\downharpoonright --></mo>\n"},
            {"rightharpoondown", "<mo>&#x21C1;<!-- \\rightharpoondown --></mo>\n"},
            {"rightharpoonup", "<mo>&#x21C0;<!-- \\rightharpoonup --></mo>\n"},
            {"upharpoonleft", "<mo>&#x21BF;<!-- \\upharpoonleft --></mo>\n"},
            {"upharpoonright", "<mo>&#x21BE;<!-- \\upharpoonright --></mo>\n"},
            {"leftharpoondown", "<mo>&#x21BD;<!-- \\leftharpoondown --></mo>\n"},
            {"leftharpoonup", "<mo>&#x21BC;<!-- \\leftharpoonup --></mo>\n"},
            {"circlearrowright", "<mo>&#x21BB;<!-- \\circlearrowright --></mo>\n"},
            {"circlearrowleft", "<mo>&#x21BA;<!-- \\circlearrowleft --></mo>\n"},
            {"curvearrowright", "<mo>&#x21B7;<!-- \\curvearrowright --></mo>\n"},
            {"curvearrowleft", "<mo>&#x21B6;<!-- \\curvearrowleft --></mo>\n"},
            {"Rsh", "<mo>&#x21B1;<!-- \\Rsh --></mo>\n"},
            {"Lsh", "<mo>&#x21B0;<!-- \\Lsh --></mo>\n"},
            {"looparrowright", "<mo>&#x21AC;<!-- \\looparrowright --></mo>\n"},
            {"looparrowleft", "<mo>&#x21AB;<!-- \\looparrowleft --></mo>\n"},
            {"rightarrowtail", "<mo>&#x21A3;<!-- \\rightarrowtail --></mo>\n"},
            {"leftarrowtail", "<mo>&#x21A2;<!-- \\leftarrowtail --></mo>\n"},
            {"twoheaddownarrow", "<mo>&#x21A1;<!-- \\twoheaddownarrow --></mo>\n"},
            {"twoheadrightarrow", "<mo>&#x21A0;<!-- \\twoheadrightarrow --></mo>\n"},
            {"twoheaduparrow", "<mo>&#x219F;<!-- \\twoheaduparrow --></mo>\n"},
            {"twoheadleftarrow", "<mo>&#x219E;<!-- \\twoheadleftarrow --></mo>\n"},
            {"swarrow", "<mo>&#x2199;<!-- \\swarrow --></mo>\n"},
            {"searrow", "<mo>&#x2198;<!-- \\searrow --></mo>\n"},
            {"nearrow", "<mo>&#x2197;<!-- \\nearrow --></mo>\n"},
            {"nwarrow", "<mo>&#x2196;<!-- \\nwarrow --></mo>\n"},
            {"updownarrow", "<mo>&#x2195;<!-- \\updownarrow --></mo>\n"},
            {"leftrightarrow", "<mo>&#x2194;<!-- \\leftrightarrow --></mo>\n"},
            {"downarrow", "<mo>&#x2193;<!-- &darr; --></mo>\n"},
            {"rightarrow", "<mo>&#x2192;<!-- &rarr; --></mo>\n"},
            {"uparrow", "<mo>&#x2191;<!-- &uarr; --></mo>\n"},
            {"leftarrow", "<mo>&#x2190;<!-- &larr; --></mo>\n"},

            {"varnothing", "<mo>&#x2205;<!-- &empty; --></mo>\n"},
            {"triangle", "<mo>&#x2206;<!-- \\triangle --></mo>\n"},
            {"nabla", "<mo>&#x2207;<!-- &nabla; --></mo>\n"},
            {"triangledown", "<mo>&#x2207;<!-- &nabla; --></mo>\n"},
            {"blacksquare", "<mo>&#x220E;<!-- \\blacksquare --></mo>\n"},
            {"partial", "<mo>&#x2202;<!-- &part; --></mo>\n"},
            {"dashrightarrow", "<mo>&#x21E2;<!-- \\dashrightarrow --></mo>\n"},
            {"dashleftarrow", "<mo>&#x21E0;<!-- \\dashleftarrow --></mo>\n"},
            
            {"left", ""},
            {"right", ""},
            {"[", "<mo>[</mo>\n"},
            {"]", "<mo>]</mo>\n"},
            {"{", "<mo>{</mo>\n"},
            {"}", "<mo>}</mo>\n"},
            {"|", "<mo>|</mo>\n"},

			//{"dots", "<mi>&#x2026;<!-- &hellip; --></mi>\n"},
			{"dots", "<mo>&hellip;</mo>\n"},
			{"dotsm", "<mo>&ctdot;</mo>\n"},
			{"vdots", "<mo>&dtdot;</mo>\n"},
			{"ddots", "<mo>&vellip;</mo>\n"},

            #endregion
        };

        /// <summary>
        /// Gets an empty string. This property must be overriden by all the inheritors.
        /// </summary>
        public override string Name
        {
            get { return ""; }
        }

        /// <summary>
        /// Returns true if the block cancels a math environment; otherwise, false.
        /// </summary>
        /// <returns></returns>
        public virtual bool CancelsMathMode()
        {
            return false;
        }

        public static bool CancelsMathMode(string name)
        {
            foreach (var commandConverter in CommandConverters)
            {
                if (commandConverter.Value.Name == name)
                {
                    return commandConverter.Value.CancelsMathMode();
                }
            }
            return false;
        }

        /// <summary>
        /// Gets the expected count of child subtrees.
        /// </summary>
        public virtual int ExpectedBranchesCount
        {
            get { return 0; }
        }

        /// <summary>
        /// Gets the type of the corresponding expression (that is, ExpressionType.Command).
        /// </summary>
        public override ExpressionType ExprType
        {
            get
            {
                return ExpressionType.Command;
            }
        }

        /// <summary>
        /// Gets the Expressions[0][0] string or an empty string if no child expressions exist.
        /// This property can be overriden by an inheritor.
        /// </summary>
        public virtual string FirstValue
        {
            get
            {
                return "";
            }
        }

        /// <summary>
        /// Gets the hash code of this instance.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ ExprType.GetHashCode() ^ FirstValue.GetHashCode();
        }

        /// <summary>
        /// Converts the value of this instance to a System.String.
        /// </summary>
        /// <returns>The System.String instance.</returns>
        public override string ToString()
        {
            return string.Format("[{0}] {1} ({2})", ExprType, Name, FirstValue);
        }

        /// <summary>
        /// Determines whether the specified System.Object is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to check.</param>
        /// <returns>True if the specified System.Object is equal to this instance; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            var conv = obj as CommandConverter;
            if (conv == null)
            {
                return false;
            }
            return Name == conv.Name && ExprType == conv.ExprType && FirstValue == conv.FirstValue;
        }

        /// <summary>
        /// Searches in a predefined conversion table and returns the converted result or null.
        /// </summary>
        /// <param name="table">The conversion table to search in.</param>
        /// <param name="expr">The expression to convert.</param>
        /// <returns>The converted result or null.</returns>
        private static string SearchInTables(IDictionary<string, string> table, LatexExpression expr)
        {
            string constant;
            if (table.TryGetValue(expr.Name, out constant))
            {
                string children = "";
                if (expr.Expressions != null && expr.Options != null && expr.Options.AsExpressions != null)
                {
                    for (int i = 0; i < expr.Options.AsExpressions.Count; i++)
                    {
                        children += expr.Options.AsExpressions[i].Convert();
                    }
                }
                if (expr.Expressions != null)
                {
                    for (int i = 0; i < expr.Expressions.Count; i++)
                    {
                        for (int j = 0; j < expr.Expressions[i].Count; j++)
                        {
                            children += expr.Expressions[i][j].Convert();
                        }
                    }
                }
                return constant + children;
            }
            return null;
        }

        /// <summary>
        /// Performs the conversion procedure.
        /// </summary>
        /// <param name="expr">The expression to convert.</param>
        /// <returns>The conversion result.</returns>
        public override string Convert(LatexExpression expr)
        {
            CommandConverter converter;
            if (CommandConverters.TryGetValue(expr.GetCommandConverterHashCode(), out converter))
            {
                if (converter.ExpectedBranchesCount > 0 && (expr.Expressions == null || expr.Expressions.Count < converter.ExpectedBranchesCount))
                {

                    throw new FormatException(@"Unexpected format in command \\" + converter.Name);
                    //return "<!-- Unexpected format in command \\" + converter.Name + " -->";
                }
                var result = converter.Convert(expr);
                // Make sure that {} blocks which were attached to the command by mistake will be converted, too.
                // Goddamn ancient Latex
                if (expr.Expressions != null && expr.Expressions.Count > converter.ExpectedBranchesCount)
                {
                    for (int i = converter.ExpectedBranchesCount; i < expr.Expressions.Count; i++)
                    {
                        result += SequenceConverter.ConvertOutline(expr.Expressions[i], expr.Customization);
                    }
                }
                return result;
            }
            string constant;
            if (CommandConstants.TryGetValue(expr.Name, out constant))
            {
                if (expr.MathMode)
                {
                    return "<mi>" + constant + "</mi>";
                }
                return constant;
            }
            if ((constant = SearchInTables(MathCommandConstants, expr)) != null)
            {
                return constant;
            }
            if ((constant = SearchInTables(MathFunctionsCommandConstants, expr)) != null)
            {
                return constant;
            }
            if ((constant = SearchInTables(MathFunctionsScriptCommandConstants, expr)) != null)
            {
                return constant;
            }            
            return "<!-- \\" + LatexStringToXmlString(expr.Name) + " -->\n";
        }
    }
}
