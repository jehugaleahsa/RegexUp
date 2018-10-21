using System.Text.RegularExpressions;

namespace RegexUp
{
    /// <summary>
    /// Provides factory methods for creating literals.
    /// </summary>
    public sealed class Literal : ILiteral, IExpression
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

        internal Literal(string value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets the literal value.
        /// </summary>
        public string Value { get; }

        string IExpression.Encode(ExpressionContext context)
        {
            if (context == ExpressionContext.CharacterGroup)
            {
                var escaped = Value.Replace(@"\", @"\\");
                escaped = escaped.Replace(@"-", @"\-");
                escaped = escaped.Replace(@"^", @"\^");
                return escaped;
            }
            return Regex.Escape(Value);
        }

        /// <summary>
        /// Gets the literal value.
        /// </summary>
        /// <returns>The literal value.</returns>
        public override string ToString() => Value;
    }
}
