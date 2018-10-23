namespace RegexUp
{
    internal interface IExpressionEncoder
    {
        string Encode(ExpressionContext context, int position, int length);
    }
}
