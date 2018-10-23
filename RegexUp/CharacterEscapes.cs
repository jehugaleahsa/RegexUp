using System.Collections.Concurrent;

namespace RegexUp
{
    /// <summary>
    /// Character escape definitions.
    /// </summary>
    public static class CharacterEscapes
    {
        private static readonly ConcurrentDictionary<string, ICharacterEscape> escapes = new ConcurrentDictionary<string, ICharacterEscape>();

        /// <summary>
        /// Matches a bell (alarm) character, \u0007.
        /// </summary>
        public static ICharacterEscape Bell => For('a');

        /// <summary>
        /// In a character class, matches a backspace, \u0008. Outside a character class, \b is an anchor that matches a word boundary.
        /// </summary>
        public static ICharacterEscape Backspace => For('b');

        /// <summary>
        /// Matches a tab, \u0009.
        /// </summary>
        public static ICharacterEscape Tab => For('t');

        /// <summary>
        /// Matches a carriage return, \u000D. Note that \r is not equivalent to the newline character, \n.
        /// </summary>
        public static ICharacterEscape CarriageReturn => For('r');

        /// <summary>
        /// Matches a vertical tab, \u000B.
        /// </summary>
        public static ICharacterEscape VerticalTab => For('v');

        /// <summary>
        /// Matches a form feed, \u000C.
        /// </summary>
        public static ICharacterEscape FormFeed => For('f');

        /// <summary>
        /// Matches a new line, \u000A.
        /// </summary>
        public static ICharacterEscape NewLine => For('n');

        /// <summary>
        /// Matches an escape, \u001B.
        /// </summary>
        public static ICharacterEscape Escape => For('e');

        /// <summary>
        /// Matches a period (.).
        /// </summary>
        public static ICharacterEscape Period => For('.');

        /// <summary>
        /// Matches a dollar ($).
        /// </summary>
        public static ICharacterEscape Dollar => For('$');

        /// <summary>
        /// Matches a carot (^).
        /// </summary>
        public static ICharacterEscape Carot => For('^');

        /// <summary>
        /// Matches a left curly brace ({).
        /// </summary>
        public static ICharacterEscape LeftCurlyBrace => For('{');

        /// <summary>
        /// Matches a left square bracket ([).
        /// </summary>
        public static ICharacterEscape LeftSquareBracket => For('[');

        /// <summary>
        /// Matches a left parenthesis (().
        /// </summary>
        public static ICharacterEscape LeftParenthesis => For('(');

        /// <summary>
        /// Matches a pipe (|).
        /// </summary>
        public static ICharacterEscape Pipe => For('|');

        /// <summary>
        /// Matches a right parenthesis ()).
        /// </summary>
        public static ICharacterEscape RightParenthesis => For(')');

        /// <summary>
        /// Matches an asterisk (*).
        /// </summary>
        public static ICharacterEscape Asterisk => For('*');

        /// <summary>
        /// Matches a plus (+).
        /// </summary>
        public static ICharacterEscape Plus => For('+');

        /// <summary>
        /// Matches a question mark (?).
        /// </summary>
        public static ICharacterEscape QuestionMark => For('?');

        /// <summary>
        /// Matches a backslash (\).
        /// </summary>
        public static ICharacterEscape Backslash => For('\\');

        /// <summary>
        /// Escapes the given character.
        /// </summary>
        /// <param name="value">The character to escape.</param>
        /// <returns>The escaped character.</returns>
        public static ICharacterEscape For(char value)
        {
            return escapes.GetOrAdd($@"\{value}", (key) => new CharacterEscape(key)); 
        }
    }
}
