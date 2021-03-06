﻿namespace RegexUp
{
    internal sealed class UnicodeEscape : IUnicodeEscape
    {
        public UnicodeEscape(int code)
        {
            CharacterCode = code;
        }

        public int CharacterCode { get; }

        public bool NeedsGroupedToQuantify() => false;

        void IVisitableExpression.Accept(ExpressionVisitor visitor) => visitor.Visit(this);

        public override string ToString() => EncodingExpressionVisitor.ToString(this);
    }
}
