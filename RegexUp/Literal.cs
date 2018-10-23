using System;
using System.Collections.Concurrent;
using System.Linq;

namespace RegexUp
{
    /// <summary>
    /// Provides factory methods for creating literals.
    /// </summary>
    public sealed class Literal : ILiteral, IExpressionEncoder
    {
        private static readonly ConcurrentDictionary<char, ILiteral> literals = new ConcurrentDictionary<char, ILiteral>();

        /// <summary>
        /// Creates a literal for the given value, escaping special characters, if necessary.
        /// </summary>
        /// <param name="value">The value to create a literal for.</param>
        /// <returns>The literal.</returns>
        public static ILiteral For(char value)
        {
            return literals.GetOrAdd(value, (c) => new Literal(c));
        }

        /// <summary>
        /// Creates a literal for the given value, escaping special characters, if necessary.
        /// </summary>
        /// <param name="value">The value to create a literal for.</param>
        /// <returns>The literal.</returns>
        public static ICompoundLiteral For(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            if (value.Length == 1)
            {
                return For(value[0]);
            }
            var literals = value.Select(c => For(c));
            var compound = new CompoundLiteral();
            foreach (var character in value)
            {
                compound.Add(For(character));
            }
            return compound;
        }

        internal Literal(char value)
        {
            this.Value = value;
        }
        
        public char Value { get; }

        string ICompoundLiteral.Value => Value.ToString();

        bool IExpression.NeedsGroupedToQuantify() => false;

        string IExpressionEncoder.Encode(ExpressionContext context)
        {
            if (Char.IsWhiteSpace(Value))
            {
                return $@"\{Value}";
            }
            if (context == ExpressionContext.CharacterGroup)
            {
                switch (Value)
                {
                    case '\\': return @"\\";
                    case '-': return @"\-";
                    case '^': return @"\^";
                    default: return Value.ToString();
                }
            }
            else
            {
                switch (Value)
                {
                    case '\\': return @"\\";
                    case '*': return @"\*";
                    case '+': return @"\+";
                    case '?': return @"\?";
                    case '|': return @"\|";
                    case '{': return @"\{";
                    case '[': return @"\[";
                    case '(': return @"\(";
                    case ')': return @"\)";
                    case '^': return @"\^";
                    case '$': return @"\$";
                    case '.': return @"\";
                    case '#': return @"\#";
                    default: return Value.ToString();
                }
            }
        }

        /// <summary>
        /// Gets the literal value.
        /// </summary>
        /// <returns>The literal value.</returns>
        public override string ToString() => Value.ToString();
    }
}
