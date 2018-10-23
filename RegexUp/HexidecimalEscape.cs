using System;

namespace RegexUp
{
    internal sealed class HexidecimalEscape : IHexidecimalEscape, IExpressionEncoder
    {
        public HexidecimalEscape(int code)
        {
            this.CharacterCode = code;
        }

        public int CharacterCode { get; }

        public bool NeedsGroupedToQuantify() => false;

        public string Encode(ExpressionContext context, int position, int length)
        {
            var hexString = Convert.ToString(CharacterCode, 16).PadLeft(2, '0');
            return $@"\x{hexString}";
        }

        public override string ToString() => Encode(ExpressionContext.Group, 0, 1);
    }
}
