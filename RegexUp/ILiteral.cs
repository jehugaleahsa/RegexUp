﻿namespace RegexUp
{
    /// <summary>
    /// A character to be matched verbatim.
    /// </summary>
    public interface ILiteral : ICharacterGroupMember, ICompoundLiteral, IExpression
    {
        /// <summary>
        /// Gets the value of the literal.
        /// </summary>
        new char Value { get; }
    }
}
