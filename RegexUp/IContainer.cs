namespace RegexUp
{
    internal interface IContainer : IExpression
    {
        void Add(IExpression expression);
    }
}
