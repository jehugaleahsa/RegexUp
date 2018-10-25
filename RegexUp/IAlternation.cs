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
    }
}
