using System;
using System.Collections.Generic;

namespace RegexUp
{
    /// <summary>
    /// Provides factory methods for creating positive lookahead assertions.
    /// </summary>
    public sealed class PositiveLookaheadAssertion : Group, IPositiveLookaheadAssertion
    {
        /// <summary>
        /// Creates a group that is not captured.
        /// </summary>
        /// <returns>The non-capture group.</returns>
        public static IPositiveLookaheadAssertion Of(params IExpression[] members)
        {
            return From(members);
        }

        /// <summary>
        /// Creates a group that is not captured.
        /// </summary>
        /// <returns>The non-capture group.</returns>
        public static IPositiveLookaheadAssertion From(IEnumerable<IExpression> members)
        {
            if (members == null)
            {
                throw new ArgumentNullException(nameof(members));
            }
            var group = new PositiveLookaheadAssertion();
            foreach (var expression in members)
            {
                group.Add(expression);
            }
            return group;
        }

        internal PositiveLookaheadAssertion()
        {
        }

        protected override string OnEncode()
        {
            var parts = new[] { "(?=", EncodeMembers(), ")" };
            var encoded = String.Join(String.Empty, parts);
            return encoded;
        }
    }
}
