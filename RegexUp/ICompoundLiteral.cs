using System.Collections.Generic;

namespace RegexUp
{
    /// <summary>
    /// A sequence of characters to be matched verbatim.
    /// </summary>
    public interface ICompoundLiteral : ICharacterGroupMember, IExpression
    {
        /// <summary>
        /// Gets the literals making up the compound literal.
        /// </summary>
        IEnumerable<ILiteral> Literals { get; }

        /// <summary>
        /// Gets the value of the literal.
        /// </summary>
        string Value { get; }
    }
}
