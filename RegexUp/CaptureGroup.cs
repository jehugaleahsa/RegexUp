using System;
using System.Collections.Generic;

namespace RegexUp
{
    /// <summary>
    /// Provides factory methods for creating capture groups.
    /// </summary>
    public sealed class CaptureGroup : Group, ICaptureGroup
    {
        /// <summary>
        /// Creates a new group that captures its subexpressions by number or name (optional).
        /// </summary>
        /// <param name="name">An optional name to associate with the capture group.</param>
        /// <param name="members">The sub-expressions appearing in the group.</param>
        /// <returns>The capture group.</returns>
        public static ICaptureGroup Of(params IExpression[] members)
        {
            return From(null, members);
        }

        /// <summary>
        /// Creates a new group that captures its subexpressions by number or name (optional).
        /// </summary>
        /// <param name="options">The capture group options to use.</param>
        /// <param name="members">The sub-expressions appearing in the group.</param>
        /// <returns>The capture group.</returns>
        public static ICaptureGroup Of(CaptureGroupOptions options, params IExpression[] members)
        {
            return From(options, members);
        }

        /// <summary>
        /// Creates a new group that captures its subexpressions by number or name (optional).
        /// </summary>
        /// <param name="members">The sub-expressions appearing in the group.</param>
        /// <returns>The capture group.</returns>
        public static ICaptureGroup From(IEnumerable<IExpression> members)
        {
            return From(null, members);
        }

        /// <summary>
        /// Creates a new group that captures its subexpressions by number or name (optional).
        /// </summary>
        /// <param name="options">The capture group options to use -or- null, if no options are provided.</param>
        /// <param name="members">The sub-expressions appearing in the group.</param>
        /// <returns>The capture group.</returns>
        public static ICaptureGroup From(CaptureGroupOptions options, IEnumerable<IExpression> members)
        {
            if (members == null)
            {
                throw new ArgumentNullException(nameof(members));
            }
            var group = new CaptureGroup();
            if (options != null)
            {
                string name = String.IsNullOrWhiteSpace(options.Name) ? null : options.Name;
                ValidateCaptureGroupName(nameof(options), name);
                group.Name = name;
                group.UseQuotes = options.UseQuotes;
            }
            foreach (var member in members)
            {
                group.Add(member);
            }
            return group;
        }

        internal CaptureGroup()
        {
        }

        public string Name { get; set; }

        public bool UseQuotes { get; set; }

        protected override string OnEncode()
        {
            var parts = new List<string>() { "(" };
            if (Name != null)
            {
                parts.Add("?");
                parts.Add(UseQuotes ? "'" : "<");
                parts.Add(Name);
                parts.Add(UseQuotes ? "'" : ">");
            }
            parts.Add(EncodeMembers());
            parts.Add(")");
            var encoded = String.Join(String.Empty, parts);
            return encoded;
        }
    }
}
