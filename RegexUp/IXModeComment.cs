namespace RegexUp
{
    /// <summary>
    /// Represents a comment that extends to the end of a line in a regular expression with x mode enabled.
    /// </summary>
    public interface IXModeComment : IExpression
    {
        /// <summary>
        /// Gets the contents of the comment.
        /// </summary>
        string Comment { get; }
    }
}
