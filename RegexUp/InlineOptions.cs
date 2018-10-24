namespace RegexUp
{
    /// <summary>
    /// Provides factory methods for creating inline options.
    /// </summary>
    public sealed class InlineOptions : IInlineOptions
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

        bool IExpression.NeedsGroupedToQuantify() => false;

        void IVisitableExpression.Accept(ExpressionVisitor visitor) => visitor.Visit(this);

        public override string ToString() => EncodingExpressionVisitor.ToString(this);
    }
}
