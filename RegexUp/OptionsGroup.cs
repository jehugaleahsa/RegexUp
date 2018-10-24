using System;
using System.Collections.Generic;

namespace RegexUp
{
    /// <summary>
    /// Provides factory methods for creating groups with different regex options.
    /// </summary>
    public sealed class OptionsGroup : Group, IOptionsGroup
    {
        /// <summary>
        /// Creates a group that can enable or disable options for its subexpressions.
        /// </summary>
        /// <param name="enabled">The options to enable.</param>
        /// <param name="disabled">The options to disable.</param>
        /// <returns>The options group.</returns>
        public static IOptionsGroup Of(GroupRegexOptions enabled, GroupRegexOptions disabled, params IExpression[] members)
        {
            return From(enabled, disabled, members);
        }

        /// <summary>
        /// Creates a group that can enable or disable options for its subexpressions.
        /// </summary>
        /// <param name="enabled">The options to enable.</param>
        /// <param name="disabled">The options to disable.</param>
        /// <returns>The options group.</returns>
        public static IOptionsGroup From(GroupRegexOptions enabled, GroupRegexOptions disabled, IEnumerable<IExpression> members)
        {
            if (members == null)
            {
                throw new ArgumentNullException(nameof(members));
            }
            ValidateRegexOptions(nameof(enabled), enabled);
            ValidateRegexOptions(nameof(disabled), disabled);
            var group = new OptionsGroup() { EnabledOptions = enabled, DisabledOptions = disabled };
            foreach (var member in members)
            {
                group.Add(member);
            }
            return group;
        }

        internal OptionsGroup()
        {
        }

        public GroupRegexOptions EnabledOptions { get; set; }

        public GroupRegexOptions DisabledOptions { get; set; }

        protected override void OnAccept(ExpressionVisitor visitor) => visitor.Visit(this);

        public override string ToString() => EncodingExpressionVisitor.ToString(this);
    }
}
