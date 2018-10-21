namespace RegexUp
{
    /// <summary>
    /// Character class definitions.
    /// </summary>
    public static class CharacterClasses
    {
        /// <summary>
        /// Wildcard: Matches any single character except \n.
        /// </summary>
        public static ICharacterCategory Wildcard { get; } = new Literal(@".", true);

        /// <summary>
        /// Matches any word character.
        /// </summary>
        public static ICharacterCategory Word { get; } = new Literal(@"\w", true);

        /// <summary>
        /// Matches any non-word character.
        /// </summary>
        public static ICharacterCategory NonWord { get; } = new Literal(@"\W", true);

        /// <summary>
        /// Matches any white-space character.
        /// </summary>
        public static ICharacterCategory Space { get; } = new Literal(@"\s", true);

        /// <summary>
        /// Matches any non-white-space character.
        /// </summary>
        public static ICharacterCategory NonSpace { get; } = new Literal(@"\S", true);

        /// <summary>
        /// Matches any digit character.
        /// </summary>
        public static ICharacterCategory Digit { get; } = new Literal(@"\d", true);

        /// <summary>
        /// Matches any non-digit character.
        /// </summary>
        public static ICharacterCategory NonDigit { get; } = new Literal(@"\D", true);
    }
}
