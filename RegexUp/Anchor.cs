namespace RegexUp
{
    internal sealed class Anchor : IAnchor, IExpressionEncoder
    {
        public Anchor(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public bool NeedsGroupedToQuantify() => false;

        public string Encode(ExpressionContext context) => Value;

        public override string ToString() => Value;
    }
}
