namespace RegexUp
{
    internal class UnicodeCategory : ICharacterClass, IExpression
    {
        public UnicodeCategory(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public string Encode(ExpressionContext context) => Value;

        public override string ToString() => Value;
    }
}
