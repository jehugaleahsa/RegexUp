using System;
using System.Collections.Concurrent;
using RegexUp.Properties;

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

        /// <summary>
        /// Creates an octal character escape.
        /// </summary>
        /// <param name="code">The textual representation of the octal code.</param>
        /// <returns>The octal escape.</returns>
        public static IOctalEscape Octal(string code)
        {
            if (code.Length == 0 || code.Length > 3)
            {
                throw new ArgumentOutOfRangeException(nameof(code), code, Resources.InvalidOctalCode);
            }
            int value = Convert.ToInt32(code, 8);
            return new OctalEscape(value, code.Length);
        }

        /// <summary>
        /// Creates an hexidecimal character escape.
        /// </summary>
        /// <param name="code">The textual representation of the hexidecimal code.</param>
        /// <returns>The hexidecimal escape.</returns>
        public static IHexidecimalEscape Hexidecimal(string code)
        {
            if (code.Length != 2)
            {
                throw new ArgumentOutOfRangeException(nameof(code), code, Resources.InvalidHexidecimalCode);
            }
            int value = Convert.ToInt32(code, 16);
            return new HexidecimalEscape(value);
        }

        /// <summary>
        /// Creates a control character escape.
        /// </summary>
        /// <param name="controlCharacter">The control character (between A and Z).</param>
        /// <returns>The control character escape.</returns>
        public static IControlCharacterEscape ControlCharacter(char controlCharacter)
        {
            controlCharacter = Char.ToUpperInvariant(controlCharacter);
            if (controlCharacter < 'A' || controlCharacter > 'Z')
            {
                throw new ArgumentOutOfRangeException(nameof(controlCharacter), controlCharacter, Resources.InvalidControlCharacter);
            }
            return new ControlCharacterEscape(controlCharacter);
        }

        /// <summary>
        /// Creates a unicode character escape.
        /// </summary>
        /// <param name="code">The textual representation of the unicode code.</param>
        /// <returns>The unicode character escape.</returns>
        public static IUnicodeEscape Unicode(string code)
        {
            if (code.Length != 4)
            {
                throw new ArgumentOutOfRangeException(nameof(code), code, Resources.InvalidUnicodeCode);
            }
            int value = Convert.ToInt32(code, 16);
            return new UnicodeEscape(value);
        }
    }
}
