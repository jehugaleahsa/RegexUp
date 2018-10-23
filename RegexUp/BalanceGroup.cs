using System;
using System.Collections.Generic;
using RegexUp.Properties;

namespace RegexUp
{
    /// <summary>
    /// Provides factory methods for creating balanced groups.
    /// </summary>
    public sealed class BalancedGroup : Group, IBalancedGroup
    {
        /// <summary>
        /// Creates a balanced group.
        /// </summary>
        /// <param name="current">The name to store the result in.</param>
        /// <param name="previous">The starting group name to replace.</param>
        /// <param name="members">The sub-expressions appearing in the group.</param>
        /// <returns>The balance group.</returns>
        public static IBalancedGroup Of(string current, string previous, params IExpression[] members)
        {
            return From(current, previous, null, members);
        }

        /// <summary>
        /// Creates a balanced group.
        /// </summary>
        /// <param name="current">The name to store the result in.</param>
        /// <param name="previous">The starting group name to replace.</param>
        /// <param name="members">The sub-expressions appearing in the group.</param>
        /// <param name="options">The balance group options to use -or- null, if no options are provided.</param>
        /// <returns>The balance group.</returns>
        public static IBalancedGroup Of(string current, string previous, BalanceGroupOptions options, params IExpression[] members)
        {
            return From(current, previous, options, members);
        }

        /// <summary>
        /// Creates a balanced group.
        /// </summary>
        /// <param name="current">The name to store the result in.</param>
        /// <param name="previous">The starting group name to replace.</param>
        /// <param name="members">The sub-expressions appearing in the group.</param>
        /// <returns>The balance group.</returns>
        public static IBalancedGroup From(string current, string previous, IEnumerable<IExpression> members)
        {
            return From(current, previous, null, members);
        }

        /// <summary>
        /// Creates a balanced group.
        /// </summary>
        /// <param name="current">The name to store the result in.</param>
        /// <param name="previous">The starting group name to replace.</param>
        /// <param name="options">The sub-expressions appearing in the group.</param>
        /// <param name="options">The balance group options to use -or- null, if no options are provided.</param>
        /// <returns>The balance group.</returns>
        public static IBalancedGroup From(string current, string previous, BalanceGroupOptions options, IEnumerable<IExpression> members)
        {
            if (String.IsNullOrWhiteSpace(current))
            {
                current = null;
            }
            ValidateCaptureGroupName(nameof(current), current);
            if (String.IsNullOrWhiteSpace(previous))
            {
                throw new ArgumentException(Resources.PreviousGroupNameBlank, nameof(previous));
            }
            ValidateCaptureGroupName(nameof(previous), previous);
            var group = new BalancedGroup()
            {
                Current = current,
                Previous = previous,
                UseQuotes = options?.UseQuotes ?? false
            };
            foreach (var member in members)
            {
                group.Add(member);
            }
            return group;
        }

        internal BalancedGroup()
        {
        }

        public string Current { get; set; }

        public string Previous { get; set; }

        public bool UseQuotes { get; set; }

        protected override string OnEncode()
        {
            var parts = new[] 
            {
                "(?",
                UseQuotes ? "'" : "<",
                Current,
                "-",
                Previous,
                UseQuotes ? "'" : ">",
                EncodeMembers(),
                ")"
            };
            var encoded = String.Join(String.Empty, parts);
            return encoded;
        }
    }
}
