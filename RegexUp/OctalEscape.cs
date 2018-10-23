using System;

namespace RegexUp
{
    internal sealed class OctalEscape : IOctalEscape, IExpressionEncoder
    {
        private readonly int width;

        public OctalEscape(int code, int width)
        {
            this.CharacterCode = code;
            this.width = width;
        }

        public int CharacterCode { get; }

        public bool NeedsGroupedToQuantify() => false;

        public string Encode(ExpressionContext context, int position, int length)
        {
            var octalString = Convert.ToString(CharacterCode, 8).PadLeft(width, '0');
            return $@"\{octalString}";
        }

        public override string ToString() => Encode(ExpressionContext.Group, 0, 1);
    }
}
