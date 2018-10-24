namespace RegexUp
{
    internal sealed class Anchor : IAnchor
    {
        public Anchor(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public bool NeedsGroupedToQuantify() => false;

        public void Accept(ExpressionVisitor visitor) => visitor.Visit(this);

        public override string ToString() => EncodingExpressionVisitor.ToString(this);
    }
}
