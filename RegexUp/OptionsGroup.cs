using System;
using System.Collections.Generic;
using RegexUp.Properties;

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

        private static void ValidateRegexOptions(string parameterName, GroupRegexOptions options)
        {
            if ((options & GroupRegexOptions.Multiline) == GroupRegexOptions.Multiline && (options & GroupRegexOptions.Singleline) == GroupRegexOptions.Singleline)
            {
                throw new ArgumentException(Resources.SingleAndMultilineMode, parameterName);
            }
            var allOptions = GroupRegexOptions.IgnoreCase | GroupRegexOptions.Multiline | GroupRegexOptions.ExplicitCapture | GroupRegexOptions.Singleline | GroupRegexOptions.IgnorePatternWhitespace;
            options &= ~allOptions;
            if (options != 0)
            {
                throw new ArgumentException(Resources.InvalidGroupOptions, parameterName);
            }
        }

        internal OptionsGroup()
        {
        }

        public GroupRegexOptions EnabledOptions { get; set; }

        public GroupRegexOptions DisabledOptions { get; set; }

        protected override string OnEncode()
        {
            var parts = new List<string>() { "(?", EncodeOptions(EnabledOptions) };
            if (DisabledOptions != GroupRegexOptions.None)
            {
                parts.Add("-");
                parts.Add(EncodeOptions(DisabledOptions));
            }
            parts.Add(":");
            parts.Add(EncodeMembers());
            parts.Add(")");
            var encoded = String.Join(String.Empty, parts);
            return encoded;
        }

        private string EncodeOptions(GroupRegexOptions options)
        {
            var parts = new List<string>();
            if ((options & GroupRegexOptions.IgnoreCase) == GroupRegexOptions.IgnoreCase)
            {
                parts.Add("i");
            }
            if ((options & GroupRegexOptions.Multiline) == GroupRegexOptions.Multiline)
            {
                parts.Add("m");
            }
            if ((options & GroupRegexOptions.ExplicitCapture) == GroupRegexOptions.ExplicitCapture)
            {
                parts.Add("n");
            }
            if ((options & GroupRegexOptions.Singleline) == GroupRegexOptions.Singleline)
            {
                parts.Add("s");
            }
            if ((options & GroupRegexOptions.IgnorePatternWhitespace) == GroupRegexOptions.IgnorePatternWhitespace)
            {
                parts.Add("x");
            }
            var encoded = String.Join(String.Empty, parts);
            return encoded;
        }
    }
}
