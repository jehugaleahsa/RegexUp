namespace RegexUp
{
    /// <summary>
    /// An inline comment.
    /// </summary>
    public interface IInlineComment : IExpression
    {
        /// <summary>
        /// Gets the contents of the comment.
        /// </summary>
        string Comment { get; }
    }
}
