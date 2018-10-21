namespace RegexUp
{
    internal class Anchor : IAnchor, IExpression
    {
        public Anchor(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public string Encode(ExpressionContext context) => Value;

        public override string ToString() => Value;
    }
}
