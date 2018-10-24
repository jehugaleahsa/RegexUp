namespace RegexUp
{
    /// <summary>
    /// An expression that can be visited by a visitor.
    /// </summary>
    public interface IVisitableExpression
    {
        /// <summary>
        /// Takes a visitor and calls the corresponding the handler.
        /// </summary>
        /// <param name="visitor">The visitor to call the handler for.</param>
        void Accept(ExpressionVisitor visitor);
    }
}
