namespace RegexUp
{
    /// <summary>
    /// A sequence of characters to be matched verbatim.
    /// </summary>
    public interface ILiteral : ICharacterGroupMember, IExpression
    {
        /// <summary>
        /// Gets the value of the literal.
        /// </summary>
        string Value { get; }
    }
}
