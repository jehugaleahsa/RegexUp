using System;
using System.Collections.Generic;

namespace RegexUp
{
    /// <summary>
    /// Provides factory methods for creating negative lookbehind assertions.
    /// </summary>
    public sealed class NegativeLookbehindAssertion : Group, INegativeLookbehindAssertion
    {
        /// <summary>
        /// Creates a group that is not captured.
        /// </summary>
        /// <returns>The non-capture group.</returns>
        public static INegativeLookbehindAssertion Of(params IExpression[] members)
        {
            return From(members);
        }

        /// <summary>
        /// Creates a group that is not captured.
        /// </summary>
        /// <returns>The non-capture group.</returns>
        public static INegativeLookbehindAssertion From(IEnumerable<IExpression> members)
        {
            if (members == null)
            {
                throw new ArgumentNullException(nameof(members));
            }
            var group = new NegativeLookbehindAssertion();
            foreach (var expression in members)
            {
                group.Add(expression);
            }
            return group;
        }

        internal NegativeLookbehindAssertion()
        {
        }

        protected override string OnEncode()
        {
            var parts = new[] { "(?<!", EncodeMembers(), ")" };
            var encoded = String.Join(String.Empty, parts);
            return encoded;
        }
    }
}
