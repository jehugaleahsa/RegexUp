using System;
using System.Collections.Generic;
using RegexUp.Properties;

namespace RegexUp
{
    /// <summary>
    /// Porvides factory methods for creating character ranges.
    /// </summary>
    public sealed class Range : IRange, IExpression
    {
        /// <summary>
        /// Creates a range of characters.
        /// </summary>
        /// <param name="first">The first character in the range.</param>
        /// <param name="last">The last character in the range.</param>
        /// <param name="excludedGroup">The subset of characters to exclude.</param>
        /// <returns>The range.</returns>
        public static IRange For(char first, char last, ICharacterGroup excludedGroup = null)
        {
            if (last < first)
            {
                throw new ArgumentException(Resources.InvalidRange, nameof(last));
            }
            return new Range(first, last, excludedGroup);
        }

        private Range(char first, char last, ICharacterGroup excludedGroup)
        {
            First = first;
            Last = last;
            ExcludedGroup = excludedGroup;
        }

        public char First { get; }

        public char Last { get; }

        public ICharacterGroup ExcludedGroup { get; }

        string IExpression.Encode(ExpressionContext context)
        {
            var parts = new List<string>() { First.ToString(), "-", Last.ToString() };
            if (ExcludedGroup != null)
            {
                parts.Add(((IExpression)ExcludedGroup).Encode(context));
            }
            var encoded = String.Join(String.Empty, parts);
            return encoded;
        }

        public override string ToString() => ((IExpression)this).Encode(ExpressionContext.CharacterGroup);
    }
}
