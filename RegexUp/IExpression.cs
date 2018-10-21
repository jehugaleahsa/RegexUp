namespace RegexUp
{
    /// <summary>
    /// A subexpression appearing within a regular expression.
    /// </summary>
    internal interface IExpression
    {
        string Encode(ExpressionContext context);
    }
}
