namespace RegexUp
{
    /// <summary>
    /// Character escape definitions.
    /// </summary>
    public static class CharacterEscapes
    {
        /// <summary>
        /// Matches a bell (alarm) character, \u0007.
        /// </summary>
        public static ICharacterEscape Bell { get; } = new Literal(@"\a", true);

        /// <summary>
        /// In a character class, matches a backspace, \u0008. Outside a character class, \b is an anchor that matches a word boundary.
        /// </summary>
        public static ICharacterEscape Backspace { get; } = new Literal(@"\b", true);

        /// <summary>
        /// Matches a tab, \u0009.
        /// </summary>
        public static ICharacterEscape Tab { get; } = new Literal(@"\t", true);

        /// <summary>
        /// Matches a carriage return, \u000D. Note that \r is not equivalent to the newline character, \n.
        /// </summary>
        public static ICharacterEscape CarriageReturn { get; } = new Literal(@"\r", true);

        /// <summary>
        /// Matches a vertical tab, \u000B.
        /// </summary>
        public static ICharacterEscape VerticalTab { get; } = new Literal(@"\v", true);

        /// <summary>
        /// Matches a form feed, \u000C.
        /// </summary>
        public static ICharacterEscape FormFeed { get; } = new Literal(@"\f", true);

        /// <summary>
        /// Matches a new line, \u000A.
        /// </summary>
        public static ICharacterEscape NewLine { get; } = new Literal(@"\n", true);

        /// <summary>
        /// Matches an escape, \u001B.
        /// </summary>
        public static ICharacterEscape Escape { get; } = new Literal(@"\e", true);

        /// <summary>
        /// Matches a period (.).
        /// </summary>
        public static ICharacterEscape Period { get; } = new Literal(@"\.", true);

        /// <summary>
        /// Matches a dollar ($).
        /// </summary>
        public static ICharacterEscape Dollar { get; } = new Literal(@"\$", true);

        /// <summary>
        /// Matches a carot (^).
        /// </summary>
        public static ICharacterEscape Carot { get; } = new Literal(@"\^", true);

        /// <summary>
        /// Matches a left curly brace ({).
        /// </summary>
        public static ICharacterEscape LeftCurlyBrace { get; } = new Literal(@"\{", true);

        /// <summary>
        /// Matches a left square bracket ([).
        /// </summary>
        public static ICharacterEscape LeftSquareBracket { get; } = new Literal(@"\[", true);

        /// <summary>
        /// Matches a left parenthesis (().
        /// </summary>
        public static ICharacterEscape LeftParenthesis { get; } = new Literal(@"\(", true);

        /// <summary>
        /// Matches a pipe (|).
        /// </summary>
        public static ICharacterEscape Pipe { get; } = new Literal(@"\|", true);

        /// <summary>
        /// Matches a right parenthesis ()).
        /// </summary>
        public static ICharacterEscape RightParenthesis { get; } = new Literal(@"\)", true);

        /// <summary>
        /// Matches an asterisk (*).
        /// </summary>
        public static ICharacterEscape Asterisk { get; } = new Literal(@"\*", true);

        /// <summary>
        /// Matches a plus (+).
        /// </summary>
        public static ICharacterEscape Plus { get; } = new Literal(@"\+", true);

        /// <summary>
        /// Matches a question mark (?).
        /// </summary>
        public static ICharacterEscape QuestionMark { get; } = new Literal(@"\?", true);

        /// <summary>
        /// Matches a backslash (\).
        /// </summary>
        public static ICharacterEscape Backslash { get; } = new Literal(@"\\", true);

        /// <summary>
        /// Escapes the given character, even if it does not normally need escaped.
        /// </summary>
        /// <param name="value">The value to escape.</param>
        /// <returns>The escaped literal.</returns>
        internal static ICharacterEscape For(char value) => new Literal($@"\{value}", true);
    }
}
