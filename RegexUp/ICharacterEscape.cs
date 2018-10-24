namespace RegexUp
{
    /// <summary>
    /// Match a special character or an explicitly escaped character.
    /// </summary>
    public interface ICharacterEscape : ICharacterGroupMember, IExpression
    {
        /// <summary>
        /// Gets literal value for the escape.
        /// </summary>
        string Value { get; }
    }
}
