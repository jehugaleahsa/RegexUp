using System.Collections.Generic;

namespace RegexUp
{
    /// <summary>
    /// Alternation constructs modify a regular expression to enable either/or matching.
    /// </summary>
    public interface IAlternation : IExpression
    {
        /// <summary>
        /// Gets the alternative options.
        /// </summary>
        IEnumerable<IExpression> Alternatives { get; }

        /// <summary>
        /// Adds the expression as an alternative.
        /// </summary>
        void Add(IExpression alternative);
    }

    /// <summary>
    /// An alternation construct that attempts to match the first alternative if the preceding expression matches.
    /// </summary>
    public interface IExpressionBasedConditionalAlternation : IExpression
    {
        /// <summary>
        /// Gets the expression that must match in order for the 'yes' option to be matched.
        /// </summary>
        IExpression Expression { get; }

        /// <summary>
        /// Gets the option to match if the expression matches.
        /// </summary>
        IExpression YesOption { get; }

        /// <summary>
        /// Gets the options to match if the expression does not match.
        /// </summary>
        IExpression NoOption { get; }
    }
}
