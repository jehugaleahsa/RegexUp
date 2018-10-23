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
                throw new ArgumentOutOfRangeException(nameof(last), last, Resources.InvalidMinMax);
            }
            return new Range(Literal.For(first), Literal.For(last));
        }

        /// <summary>
        /// Creates a range of characters.
        /// </summary>
        /// <param name="first">The first character in the range.</param>
        /// <param name="last">The last character in the range.</param>
        /// <returns>The range.</returns>
        public static IRange For(ICharacterGroupMember first, ICharacterGroupMember last)
        {
            return new Range(first, last);
        }

        private Range(ICharacterGroupMember first, ICharacterGroupMember last)
        {
            First = first;
            Last = last;
        }

        public ICharacterGroupMember First { get; }

        public ICharacterGroupMember Last { get; }

        string IExpressionEncoder.Encode(ExpressionContext context, int position, int length)
        {
            var first = ((IExpressionEncoder)First).Encode(ExpressionContext.CharacterGroup, 0, 2);
            var last = ((IExpressionEncoder)Last).Encode(ExpressionContext.CharacterGroup, 1, 2);
            var parts = new List<string>() { first, "-", last };
            var encoded = String.Join(String.Empty, parts);
            return encoded;
        }

        public override string ToString() => ((IExpressionEncoder)this).Encode(ExpressionContext.CharacterGroup, 0, 1);
    }
}
