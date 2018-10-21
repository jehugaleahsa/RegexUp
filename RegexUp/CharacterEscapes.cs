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
        public static ICharacterEscape Bell { get; } = new CharacterEscape(@"\a");

        /// <summary>
        /// In a character class, matches a backspace, \u0008. Outside a character class, \b is an anchor that matches a word boundary.
        /// </summary>
        public static ICharacterEscape Backspace { get; } = new CharacterEscape(@"\b");

        /// <summary>
        /// Matches a tab, \u0009.
        /// </summary>
        public static ICharacterEscape Tab { get; } = new CharacterEscape(@"\t");

        /// <summary>
        /// Matches a carriage return, \u000D. Note that \r is not equivalent to the newline character, \n.
        /// </summary>
        public static ICharacterEscape CarriageReturn { get; } = new CharacterEscape(@"\r");

        /// <summary>
        /// Matches a vertical tab, \u000B.
        /// </summary>
        public static ICharacterEscape VerticalTab { get; } = new CharacterEscape(@"\v");

        /// <summary>
        /// Matches a form feed, \u000C.
        /// </summary>
        public static ICharacterEscape FormFeed { get; } = new CharacterEscape(@"\f");

        /// <summary>
        /// Matches a new line, \u000A.
        /// </summary>
        public static ICharacterEscape NewLine { get; } = new CharacterEscape(@"\n");

        /// <summary>
        /// Matches an escape, \u001B.
        /// </summary>
        public static ICharacterEscape Escape { get; } = new CharacterEscape(@"\e");

        /// <summary>
        /// Matches a period (.).
        /// </summary>
        public static ICharacterEscape Period { get; } = new CharacterEscape(@"\.");

        /// <summary>
        /// Matches a dollar ($).
        /// </summary>
        public static ICharacterEscape Dollar { get; } = new CharacterEscape(@"\$");

        /// <summary>
        /// Matches a carot (^).
        /// </summary>
        public static ICharacterEscape Carot { get; } = new CharacterEscape(@"\^");

        /// <summary>
        /// Matches a left curly brace ({).
        /// </summary>
        public static ICharacterEscape LeftCurlyBrace { get; } = new CharacterEscape(@"\{");

        /// <summary>
        /// Matches a left square bracket ([).
        /// </summary>
        public static ICharacterEscape LeftSquareBracket { get; } = new CharacterEscape(@"\[");

        /// <summary>
        /// Matches a left parenthesis (().
        /// </summary>
        public static ICharacterEscape LeftParenthesis { get; } = new CharacterEscape(@"\(");

        /// <summary>
        /// Matches a pipe (|).
        /// </summary>
        public static ICharacterEscape Pipe { get; } = new CharacterEscape(@"\|");

        /// <summary>
        /// Matches a right parenthesis ()).
        /// </summary>
        public static ICharacterEscape RightParenthesis { get; } = new CharacterEscape(@"\)");

        /// <summary>
        /// Matches an asterisk (*).
        /// </summary>
        public static ICharacterEscape Asterisk { get; } = new CharacterEscape(@"\*");

        /// <summary>
        /// Matches a plus (+).
        /// </summary>
        public static ICharacterEscape Plus { get; } = new CharacterEscape(@"\+");

        /// <summary>
        /// Matches a question mark (?).
        /// </summary>
        public static ICharacterEscape QuestionMark { get; } = new CharacterEscape(@"\?");

        /// <summary>
        /// Matches a backslash (\).
        /// </summary>
        public static ICharacterEscape Backslash { get; } = new CharacterEscape(@"\\");
    }
}
