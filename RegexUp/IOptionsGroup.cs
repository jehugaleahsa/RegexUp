namespace RegexUp
{
    /// <summary>
    /// A group that enables and/or disables regular expression options for the subexpressions.
    /// </summary>
    public interface IOptionsGroup : IGroup
    {
        /// <summary>
        /// Gets the options that will be enabled within this group.
        /// </summary>
        GroupRegexOptions EnabledOptions { get; }

        /// <summary>
        /// Gets the options that will be disabled within this group.
        /// </summary>
        GroupRegexOptions DisabledOptions { get; }
    }
}
