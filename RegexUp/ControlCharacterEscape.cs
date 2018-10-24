namespace RegexUp
{
    internal sealed class ControlCharacterEscape : IControlCharacterEscape
    {
        public ControlCharacterEscape(char controlCharacter)
        {
            ControlCharacter = controlCharacter;
        }

        public char ControlCharacter { get; }

        public bool NeedsGroupedToQuantify() => false;

        void IVisitableExpression.Accept(ExpressionVisitor visitor) => visitor.Visit(this);

        public override string ToString() => EncodingExpressionVisitor.ToString(this);
    }
}
