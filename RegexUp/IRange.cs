namespace RegexUp
{
    /// <summary>
    /// Defines a range of characters.
    /// </summary>
    public interface IRange : ICharacterGroupMember
    {
        /// <summary>
        /// Gets the first character in the range.
        /// </summary>
        ICharacterGroupMember First { get; }

        /// <summary>
        /// Gets the last character in the range.
        /// </summary>
        ICharacterGroupMember Last { get; }
    }
}
