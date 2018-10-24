using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace RegexUp
{
    /// <summary>
    /// Provides factory methods for creating literals.
    /// </summary>
    public sealed class Literal : ILiteral
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

        private Literal(char value)
        {
            Value = value;
        }
        
        public char Value { get; }

        IEnumerable<ILiteral> ICompoundLiteral.Literals => new[] { this };

        string ICompoundLiteral.Value => Value.ToString();

        bool IExpression.NeedsGroupedToQuantify() => false;

        void IVisitableExpression.Accept(ExpressionVisitor visitor) => visitor.Visit(this);

        public override string ToString() => EncodingExpressionVisitor.ToString(this);
    }
}
