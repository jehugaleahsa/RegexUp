namespace RegexUp
{
    /// <summary>
    /// Represents one or more sub-expressions.
    /// </summary>
    public interface IExpression
    {
        /// <summary>
        /// Gets whether the expression needs to be wrapped in parentheses to be quantified.
        /// </summary>
        /// <returns>True, if the expression needs wrapped in paranetheses to be quantified; otherwise, false.</returns>
        bool NeedsGroupedToQuantify();
    }
}
