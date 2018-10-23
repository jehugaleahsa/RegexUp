namespace RegexUp
{
    internal sealed class ControlCharacterEscape : IControlCharacterEscape, IExpressionEncoder
    {
        public ControlCharacterEscape(char controlCharacter)
        {
            ControlCharacter = controlCharacter;
        }

        public char ControlCharacter { get; }

        public bool NeedsGroupedToQuantify() => false;

        public string Encode(ExpressionContext context, int position, int length)
        {
            return $@"\c{ControlCharacter}";
        }

        public override string ToString() => Encode(ExpressionContext.Group, 0, 1);
    }
}
