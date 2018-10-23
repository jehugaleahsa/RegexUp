namespace RegexUp
{
    /// <summary>
    /// Matches an ASCII character, where nn is a two-digit hexadecimal character code.
    /// </summary>
    public interface IHexidecimalEscape : ICharacterGroupMember, IExpression
    {
        /// <summary>
        /// Gets the numeric value for the hex character code.
        /// </summary>
        int CharacterCode { get; }
    }
}
