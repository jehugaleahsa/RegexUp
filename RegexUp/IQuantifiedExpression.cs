﻿namespace RegexUp
{
    /// <summary>
    /// A sub-expression appearing within a regular expression with an indicator specifying how many times it can occur.
    /// </summary>
    public interface IQuantifiedExpression : IExpression
    {
        /// <summary>
        /// Gets the expression being quantified.
        /// </summary>
        IExpression Expression { get; }

        /// <summary>
        /// Gets the quantifier used.
        /// </summary>
        string Quantifier { get; }

        /// <summary>
        /// Gets the minimum number of times the expression may appear.
        /// </summary>
        int LowerBound { get; }

        /// <summary>
        /// Gets the maximum number of times the expression may appear; however, if null, there is no upper bound.
        /// </summary>
        int? UpperBound { get; }

        /// <summary>
        /// Gets whether the quantifier matches as many characters as possible.
        /// </summary>
        bool IsGreedy { get; }
    }
}
