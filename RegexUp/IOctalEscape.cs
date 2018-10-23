namespace RegexUp
{
    /// <summary>
    /// Matches an ASCII character, where nnn consists of two or three digits that represent the octal character code.
    /// </summary>
    public interface IOctalEscape : ICharacterGroupMember, IExpression
    {
        /// <summary>
        /// Gets the numeric value for the octal character code.
        /// </summary>
        int CharacterCode { get; }
    }
}
