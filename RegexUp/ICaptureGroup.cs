namespace RegexUp
{
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
}
