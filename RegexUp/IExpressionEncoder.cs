namespace RegexUp
{
    /// <summary>
    /// A subexpression appearing within a regular expression.
    /// </summary>
    internal interface IExpressionEncoder
    {
        string Encode(ExpressionContext context);
    }
}
