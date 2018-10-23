using System;
using System.Collections.Generic;

namespace RegexUp
{
    /// <summary>
    /// Provides factory methods for creating non-backtracking sub-expressions.
    /// </summary>
    public sealed class NonbacktrackingAssertion : Group, INonbacktrackingAssertion
    {
        /// <summary>
        /// Creates a group that cannot be backtracked.
        /// </summary>
        /// <returns>The non-backtracked expression group.</returns>
        public static INonbacktrackingAssertion Of(params IExpression[] members)
        {
            return From(members);
        }

        /// <summary>
        /// Creates a group that cannot be backtracked.
        /// </summary>
        /// <returns>The non-backtracked expression group.</returns>
        public static INonbacktrackingAssertion From(IEnumerable<IExpression> members)
        {
            if (members == null)
            {
                throw new ArgumentNullException(nameof(members));
            }
            var group = new NonbacktrackingAssertion();
            foreach (var expression in members)
            {
                group.Add(expression);
            }
            return group;
        }

        internal NonbacktrackingAssertion()
        {
        }

        protected override string OnEncode()
        {
            var parts = new[] { "(?>", EncodeMembers(), ")" };
            var encoded = String.Join(String.Empty, parts);
            return encoded;
        }
    }
}
