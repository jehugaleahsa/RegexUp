namespace RegexUp
{
    /// <summary>
    /// A balancing group definition deletes the definition of a previously defined group and stores, in the current group, the interval between the previously defined group and the current group.
    /// </summary>
    public interface IBalancedGroup : IGroup
    {
        /// <summary>
        /// Gets whether the names should be wrapped in quotes instead of angle brackets.
        /// </summary>
        bool UseQuotes { get; }

        /// <summary>
        /// Gets the current group name, that the previous group name will be replaced with,
        /// </summary>
        string Current { get; }

        /// <summary>
        /// Gets the group name that will be folded into the current group.
        /// </summary>
        string Previous { get; }
    }
}
