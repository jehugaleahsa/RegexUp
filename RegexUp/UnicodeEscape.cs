using System;

namespace RegexUp
{
    internal sealed class UnicodeEscape : IUnicodeEscape, IExpressionEncoder
    {
        public UnicodeEscape(int code)
        {
            CharacterCode = code;
        }

        public int CharacterCode { get; }

        public bool NeedsGroupedToQuantify() => false;

        public string Encode(ExpressionContext context, int position, int length)
        {
            var hexidecimalString = Convert.ToString(CharacterCode, 16).PadLeft(4, '0');
            return $@"\u{hexidecimalString}";
        }

        public override string ToString() => Encode(ExpressionContext.Group, 0, 1);
    }
}
