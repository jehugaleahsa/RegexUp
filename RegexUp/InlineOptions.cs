using System;
using System.Collections.Generic;

namespace RegexUp
{
    /// <summary>
    /// Provides factory methods for creating inline options.
    /// </summary>
    public sealed class InlineOptions : IInlineOptions, IExpressionEncoder
    {
        /// <summary>
        /// Creates inline options that can enable or disable options for the remainder of the current expression.
        /// </summary>
        /// <param name="enabled">The options to enable.</param>
        /// <param name="disabled">The options to disable.</param>
        /// <returns>The options group.</returns>
        public static IInlineOptions For(GroupRegexOptions enabled, GroupRegexOptions disabled)
        {
            Group.ValidateRegexOptions(nameof(enabled), enabled);
            Group.ValidateRegexOptions(nameof(disabled), disabled);
            var options = new InlineOptions() { EnabledOptions = enabled, DisabledOptions = disabled };
            return options;
        }

        public GroupRegexOptions EnabledOptions { get; set; }

        public GroupRegexOptions DisabledOptions { get; set; }

        string IExpressionEncoder.Encode(ExpressionContext context, int position, int length)
        {
            var parts = new List<string>() { "(?", OptionsGroup.EncodeOptions(EnabledOptions) };
            if (DisabledOptions != GroupRegexOptions.None)
            {
                parts.Add("-");
                parts.Add(OptionsGroup.EncodeOptions(DisabledOptions));
            }
            parts.Add(")");
            var encoded = String.Join(String.Empty, parts);
            return encoded;
        }

        public override string ToString() => ((IExpressionEncoder)this).Encode(ExpressionContext.Group, 0, 1);

        bool IExpression.NeedsGroupedToQuantify() => false;
    }
}
