using System.Collections.Generic;

namespace RegexUp
{
    /// <summary>
    /// Defines an expression composed one or more expressions.
    /// </summary>
    public interface ICompoundExpression : IExpression
    {
        /// <summary>
        /// Gets the sub-expressions making up this expression.
        /// </summary>
        IEnumerable<IExpression> Members { get; }
    }
}
