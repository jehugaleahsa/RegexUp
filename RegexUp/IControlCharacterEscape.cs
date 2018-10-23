namespace RegexUp
{
    /// <summary>
    /// Matches an ASCII control character, where X is the letter of the control character.
    /// </summary>
    public interface IControlCharacterEscape : ICharacterGroupMember, IExpression
    {
        /// <summary>
        /// Gets the control character.
        /// </summary>
        char ControlCharacter { get; }
    }
}
