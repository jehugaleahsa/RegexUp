namespace RegexUp
{
    /// <summary>
    /// Matches a UTF-16 code unit whose value is nnnn hexadecimal.
    /// </summary>
    public interface IUnicodeEscape : ICharacterGroupMember, IExpression
    {
        /// <summary>
        /// Gets the numeric value for the unicode character code.
        /// </summary>
        int CharacterCode { get; }
    }
}
