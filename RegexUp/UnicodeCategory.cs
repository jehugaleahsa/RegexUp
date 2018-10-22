namespace RegexUp
{
    internal class UnicodeCategory : ICharacterClass, IExpressionEncoder
    {
        public UnicodeCategory(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public bool NeedsGroupedToQuantify() => false;

        public string Encode(ExpressionContext context) => Value;

        public override string ToString() => Value;
    }
}
