﻿using System;
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
        public static IQuantifiedExpression ZeroOrMore(IExpression expression, bool isGreedy = true)
        {
            return new QuantifiedExpression(expression, "*") { LowerBound = 0, IsGreedy = isGreedy };
        }

        /// <summary>
        /// Adds a quantifier indicating the expression can occur one or more times. (+)
        /// </summary>
        /// <param name="expression">The expression to quantified.</param>
        /// <returns>The quantified expression.</returns>
        public static IQuantifiedExpression OneOrMore(IExpression expression, bool isGreedy = true)
        {
            return new QuantifiedExpression(expression, "+") { LowerBound = 1, IsGreedy = isGreedy };
        }

        /// <summary>
        /// Adds a quantifier indicating the expression can occur zero or once. (?)
        /// </summary>
        /// <param name="expression">The expression to quantified.</param>
        /// <returns>The quantified expression.</returns>
        public static IQuantifiedExpression ZeroOrOne(IExpression expression, bool isGreedy = true)
        {
            return new QuantifiedExpression(expression, "?") { LowerBound = 0, UpperBound = 1, IsGreedy = isGreedy };
        }

        /// <summary>
        /// Adds a quantifier indicating the expression can occur an exact number of times. ({n})
        /// </summary>
        /// <param name="expression">The expression to quantified.</param>
        /// <param name="occurrences">The number of times the expression must occur.</param>
        /// <returns>The quantified expression.</returns>
        public static IQuantifiedExpression Exactly(IExpression expression, int occurrences, bool isGreedy = true)
        {
            return new QuantifiedExpression(expression, $"{{{occurrences}}}") { LowerBound = occurrences, UpperBound = occurrences, IsGreedy = isGreedy };
        }

        /// <summary>
        /// Adds a quantifier indicating the expression can occur a minimum number of times. ({n,})
        /// </summary>
        /// <param name="expression">The expression to quantified.</param>
        /// <param name="occurrences">The minimum number of times the expression must occur.</param>
        /// <returns>The quantified expression.</returns>
        public static IQuantifiedExpression AtLeast(IExpression expression, int occurrences, bool isGreedy = true)
        {
            return new QuantifiedExpression(expression, $"{{{occurrences},}}") { LowerBound = occurrences, IsGreedy = isGreedy };
        }

        /// <summary>
        /// Adds a quantifier indicating the expression can occur between  
        /// </summary>
        /// <param name="expression">The expression to quantified.</param>
        /// <param name="min">The minimum number of times the expression must occur.</param>
        /// <param name="max">The maximum number of times the expression must occur.</param>
        /// <returns>The quantified expression.</returns>
        public static IQuantifiedExpression Between(IExpression expression, int min, int max, bool isGreedy = true)
        {
            if (max < min)
            {
                throw new ArgumentException(Resources.InvalidMinMax, nameof(max));
            }
            return new QuantifiedExpression(expression, $"{{{min},{max}}}") { LowerBound = min, UpperBound = max, IsGreedy = isGreedy };
        }
    }
}
