using System;
using System.Collections.Generic;
using RegexUp.Properties;

namespace RegexUp
{
    /// <summary>
    /// Porvides factory methods for creating character ranges.
    /// </summary>
    public sealed class Range : IRange, IExpressionEncoder
    {
        /// <summary>
        /// Creates a range of characters.
        /// </summary>
        /// <param name="first">The first character in the range.</param>
        /// <param name="last">The last character in the range.</param>
        /// <returns>The range.</returns>
        public static IRange For(char first, char last)
        {
            if (last < first)
            {
                throw new ArgumentException(Resources.InvalidRange, nameof(last));
            }
            return new Range(first, last);
        }

        private Range(char first, char last)
        {
            First = first;
            Last = last;
        }

        public char First { get; }

        public char Last { get; }

        string IExpressionEncoder.Encode(ExpressionContext context)
        {
            var parts = new List<string>() { First.ToString(), "-", Last.ToString() };
            var encoded = String.Join(String.Empty, parts);
            return encoded;
        }

        public override string ToString() => ((IExpressionEncoder)this).Encode(ExpressionContext.CharacterGroup);
    }
}
