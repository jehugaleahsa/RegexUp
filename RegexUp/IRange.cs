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
        char First { get; }

        /// <summary>
        /// Gets the last character in the range.
        /// </summary>
        char Last { get; }

        /// <summary>
        /// Gets a subset of the range that should be excluded.
        /// </summary>
        ICharacterGroup ExcludedGroup { get; }
    }
}
