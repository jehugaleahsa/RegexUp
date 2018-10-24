using System;
using RegexUp.Properties;

namespace RegexUp
{
    /// <summary>
    /// Porvides factory methods for creating character ranges.
    /// </summary>
    public sealed class Range : IRange
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

        void IVisitableExpression.Accept(ExpressionVisitor visitor) => visitor.Visit(this);

        public override string ToString() => EncodingExpressionVisitor.ToString(this);
    }
}
