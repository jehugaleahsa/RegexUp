namespace RegexUp
{
    internal sealed class CharacterEscape : ICharacterEscape
    {
        public CharacterEscape(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public bool NeedsGroupedToQuantify() => false;

        void IVisitableExpression.Accept(ExpressionVisitor visitor) => visitor.Visit(this);

        public override string ToString() => EncodingExpressionVisitor.ToString(this);
    }
}
