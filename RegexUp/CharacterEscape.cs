namespace RegexUp
{
    internal class CharacterEscape : ICharacterEscape, IExpression
    {
        public CharacterEscape(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public string Encode(ExpressionContext context) => Value;

        public override string ToString() => Value;
    }
}
