using System;
using System.Collections.Generic;
using RegexUp.Properties;

namespace RegexUp
{
    /// <summary>
    /// Quantifier definitions.
    /// </summary>
    public static class Quantifiers
    {
        /// <summary>
        /// Adds a quantifier indicating the expression can occur zero or more times. (*)
        /// </summary>
        /// <param name="expression">The expression to quantified.</param>
        /// <returns>The quantified expression.</returns>
        public static IQuantifiedExpression ZeroOrMore(IQuantifiable expression, bool isGreedy = true)
        {
            return new Quantifier(expression, "*") { LowerBound = 0, IsGreedy = isGreedy };
        }

        /// <summary>
        /// Adds a quantifier indicating the expression can occur one or more times. (+)
        /// </summary>
        /// <param name="expression">The expression to quantified.</param>
        /// <returns>The quantified expression.</returns>
        public static IQuantifiedExpression OneOrMore(IQuantifiable expression, bool isGreedy = true)
        {
            return new Quantifier(expression, "+") { LowerBound = 1, IsGreedy = isGreedy };
        }

        /// <summary>
        /// Adds a quantifier indicating the expression can occur zero or once. (?)
        /// </summary>
        /// <param name="expression">The expression to quantified.</param>
        /// <returns>The quantified expression.</returns>
        public static IQuantifiedExpression ZeroOrOne(IQuantifiable expression, bool isGreedy = true)
        {
            return new Quantifier(expression, "?") { LowerBound = 0, UpperBound = 1, IsGreedy = isGreedy };
        }

        /// <summary>
        /// Adds a quantifier indicating the expression can occur an exact number of times. ({n})
        /// </summary>
        /// <param name="expression">The expression to quantified.</param>
        /// <param name="occurrences">The number of times the expression must occur.</param>
        /// <returns>The quantified expression.</returns>
        public static IQuantifiedExpression Exactly(IQuantifiable expression, int occurrences, bool isGreedy = true)
        {
            return new Quantifier(expression, $"{{{occurrences}}}") { LowerBound = occurrences, UpperBound = occurrences, IsGreedy = isGreedy };
        }

        /// <summary>
        /// Adds a quantifier indicating the expression can occur a minimum number of times. ({n,})
        /// </summary>
        /// <param name="expression">The expression to quantified.</param>
        /// <param name="occurrences">The minimum number of times the expression must occur.</param>
        /// <returns>The quantified expression.</returns>
        public static IQuantifiedExpression AtLeast(IQuantifiable expression, int occurrences, bool isGreedy = true)
        {
            return new Quantifier(expression, $"{{{occurrences},}}") { LowerBound = occurrences, IsGreedy = isGreedy };
        }

        /// <summary>
        /// Adds a quantifier indicating the expression can occur between  
        /// </summary>
        /// <param name="expression">The expression to quantified.</param>
        /// <param name="min">The minimum number of times the expression must occur.</param>
        /// <param name="max">The maximum number of times the expression must occur.</param>
        /// <returns>The quantified expression.</returns>
        public static IQuantifiedExpression Between(IQuantifiable expression, int min, int max, bool isGreedy = true)
        {
            if (max < min)
            {
                throw new ArgumentException(Resources.InvalidMinMax, nameof(max));
            }
            return new Quantifier(expression, $"{{{min},{max}}}") { LowerBound = min, UpperBound = max, IsGreedy = isGreedy };
        }
    }

    internal class Quantifier : IQuantifiedExpression, IExpression
    {
        private readonly IQuantifiable expression;
        private readonly string quantifier;

        public Quantifier(IQuantifiable expression, string quantifier)
        {
            this.expression = expression ?? throw new ArgumentNullException(nameof(expression));
            this.quantifier = quantifier;
        }

        public int LowerBound { get; set; }

        public int? UpperBound { get; set; }

        public bool IsGreedy { get; set; }

        public string Encode(ExpressionContext context)
        {
            string childEncoded = EncodeChildExpression(context);
            if (String.IsNullOrWhiteSpace(childEncoded))
            {
                return String.Empty;
            }
            var parts = new List<string>() { childEncoded, quantifier };
            if (!IsGreedy)
            {
                parts.Add("?");
            }
            var encoded = String.Join(string.Empty, parts);
            return encoded;
        }

        private string EncodeChildExpression(ExpressionContext context)
        {
            if (expression is Literal literal && !literal.BypassEscape && literal.Value.Length > 1)
            {
                var group = Groups.NonCapture();
                group.Add(literal);
                return ((IExpression)group).Encode(ExpressionContext.Group);
            }
            return ((IExpression)expression).Encode(context);
        }

        public override string ToString() => Encode(ExpressionContext.Group);
    }
}
