namespace RegexUp
{
    /// <summary>
    /// A group delineates subexpressions of a regular expression and can capture the substrings of an input string.
    /// </summary>
    public interface IGroup : IGroupMember, IQuantifiable
    {
        /// <summary>
        /// Adds the given sub-expression to the group.
        /// </summary>
        /// <param name="expression">The expression to add.</param>
        void Add(IGroupMember expression);
    }

    /// <summary>
    /// A grouping construct that captures the matched subexpression and lets you access it by number or name (optional).
    /// </summary>
    public interface ICaptureGroup : IGroup
    {
        /// <summary>
        /// Gets the name of the group -or- null, if the group is unnamed.
        /// </summary>
        string Name { get; }
    }

    /// <summary>
    /// A balancing group definition deletes the definition of a previously defined group and stores, in the current group, the interval between the previously defined group and the current group.
    /// </summary>
    public interface IBalancedGroup : IGroup
    {
        /// <summary>
        /// Gets the current group name, that the previous group name will be replaced with,
        /// </summary>
        string Current { get; }

        /// <summary>
        /// Gets the group name that will be folded into the current group.
        /// </summary>
        string Previous { get; }
    }

    /// <summary>
    /// Groups the subexpressions but does not create a capture.
    /// </summary>
    public interface INonCaptureGroup : IGroup
    {
    }

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

    /// <summary>
    /// A group that asserts the sub-expressions appears prior to the subsequent expression.
    /// </summary>
    public interface IPositiveLookaheadAssertionGroup : IGroup
    {
    }

    /// <summary>
    /// A group that asserts the sub-expressions do not appear prior to the subsequent expression.
    /// </summary>
    public interface INegativeLookaheadAssertionGroup : IGroup
    {
    }

    /// <summary>
    /// A group that asserts the sub-expressions do not appear after the previous expression.
    /// </summary>
    public interface IPositiveLookbehindAssertionGroup : IGroup
    {
    }

    /// <summary>
    /// A group that asserts the sub-expressions do not appear after the previous expression.
    /// </summary>
    public interface INegativeLookbehindAssertionGroup : IGroup
    {
    }

    /// <summary>
    /// A group that prevents the regular expression engine from backtracking.
    /// </summary>
    public interface INonbacktrackingGroup : IGroup
    {
    }
}
