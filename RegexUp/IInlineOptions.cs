namespace RegexUp
{
    /// <summary>
    /// Sets or disables options such as case insensitivity in the middle of a pattern.
    /// </summary>
    public interface IInlineOptions : IExpression
    {
        /// <summary>
        /// Gets the options that will be enabled from this point in the expression onward.
        /// </summary>
        GroupRegexOptions EnabledOptions { get; }

        /// <summary>
        /// Gets the options that will be disabled from this point in the expression onward.
        /// </summary>
        GroupRegexOptions DisabledOptions { get; }
    }
}
