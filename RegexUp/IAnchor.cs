namespace RegexUp
{
    /// <summary>
    /// Anchors specify a position in the string where a match must occur.
    /// </summary>
    public interface IAnchor : IExpression
    {
        /// <summary>
        /// Gets the literal value for the anchor.
        /// </summary>
        string Value { get; }
    }
}
