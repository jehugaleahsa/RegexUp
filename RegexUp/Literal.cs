using System.Text.RegularExpressions;

namespace RegexUp
{
    /// <summary>
    /// Provides factory methods for creating literals.
    /// </summary>
    public sealed class Literal : ILiteral, ICharacterCategory, ICharacterGroupMember, ICharacterEscape, IAnchor, IExpression
    {
        /// <summary>
        /// Creates a literal for the given value, escaping special characters, if necessary.
        /// </summary>
        /// <param name="value">The value to create a literal for.</param>
        /// <returns>The literal.</returns>
        public static ILiteral For(string value)
        {
            return new Literal(value);
        }

        internal Literal(string value, bool bypassEscape = false)
        {
            this.Value = value;
            this.BypassEscape = bypassEscape;
        }

        public string Value { get; }

        public bool BypassEscape { get; }

        string IExpression.Encode(ExpressionContext context)
        {
            if (BypassEscape)
            {
                return Value;
            }
            if (context == ExpressionContext.CharacterGroup)
            {
                var escaped = Value.Replace(@"\", @"\\");
                escaped = escaped.Replace(@"-", @"\-");
                escaped = escaped.Replace(@"^", @"\^");
                return escaped;
            }
            return Regex.Escape(Value);
        }

        public override string ToString() => Value;
    }
}
