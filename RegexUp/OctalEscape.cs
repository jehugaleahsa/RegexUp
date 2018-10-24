namespace RegexUp
{
    internal sealed class OctalEscape : IOctalEscape
    {
        public OctalEscape(int code, int width)
        {
            CharacterCode = code;
            Width = width;
        }

        public int CharacterCode { get; }

        public int Width { get; }

        public bool NeedsGroupedToQuantify() => false;

        void IVisitableExpression.Accept(ExpressionVisitor visitor) => visitor.Visit(this);

        public override string ToString() => EncodingExpressionVisitor.ToString(this);
    }
}
