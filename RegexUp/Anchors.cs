namespace RegexUp
{
    /// <summary>
    /// Anchor definitions.
    /// </summary>
    public static class Anchors
    {
        /// <summary>
        /// By default, the match must start at the beginning of the string; in multiline mode, it must start at the beginning of the line.
        /// </summary>
        public static IAnchor Carot { get; } = new Literal(@"^", true);

        /// <summary>
        /// By default, the match must occur at the end of the string or before \n at the end of the string; in multiline mode, it must occur before the end of the line or before \n at the end of the line.
        /// </summary>
        public static IAnchor Dollar { get; } = new Literal(@"$", true);

        /// <summary>
        /// The match must occur at the start of the string.
        /// </summary>
        public static IAnchor A { get; } = new Literal(@"/A", true);

        /// <summary>
        /// The match must occur at the end of the string or before \n at the end of the string.
        /// </summary>
        public static IAnchor Z { get; } = new Literal(@"/Z", true);

        /// <summary>
        /// The match must occur at the end of the string.
        /// </summary>
        public static IAnchor z { get; } = new Literal(@"/z", true);

        /// <summary>
        /// The match must occur at the point where the previous match ended.
        /// </summary>
        public static IAnchor G { get; } = new Literal(@"/G", true);

        /// <summary>
        /// The match must occur on a boundary between a \w (alphanumeric) and a \W (nonalphanumeric) character.
        /// </summary>
        public static IAnchor b { get; } = new Literal(@"/b", true);

        /// <summary>
        /// The match must not occur on a \b boundary.
        /// </summary>
        public static IAnchor B { get; } = new Literal(@"/B", true);
    }
}
