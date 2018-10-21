namespace RegexUp
{
    /// <summary>
    /// A subexpression appearing within a regular expression.
    /// </summary>
    public interface IExpression
    {
        string Encode(ExpressionContext context);
    }
}
