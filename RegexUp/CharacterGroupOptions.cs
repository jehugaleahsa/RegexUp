namespace RegexUp
{
    /// <summary>
    /// Holds the options for creating a character group.
    /// </summary>
    public sealed class CharacterGroupOptions
    {
        /// <summary>
        /// Gets or sets whether the character group is negated.
        /// </summary>
        public bool IsNegated { get; set; }

        /// <summary>
        /// Gets or sets the character group to exclude from the current character group.
        /// </summary>
        public ICharacterGroup Exclusions { get; set; }
    }
}
