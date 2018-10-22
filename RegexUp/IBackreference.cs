namespace RegexUp
{
    /// <summary>
    /// A backreference allows a previously matched subexpression to be identified subsequently in the same regular expression.
    /// </summary>
    public interface IBackreference : IExpression
    {
        /// <summary>
        /// Gets the number of name of the backreference.
        /// </summary>
        string Reference { get; }

        /// <summary>
        /// Gets whether the backreference is named.
        /// </summary>
        bool IsNamed { get; }

        /// <summary>
        /// Gets whether quotes should be used instead of angle brackets.
        /// </summary>
        bool UseQuotes { get; }
    }
}
