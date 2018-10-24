using System;

namespace RegexUp
{
    internal sealed class QuantifiedExpression : IQuantifiedExpression
    {
        public QuantifiedExpression(IExpression expression, string quantifier)
        {
            Expression = expression ?? throw new ArgumentNullException(nameof(expression));
            Quantifier = quantifier;
        }

        public IExpression Expression { get; }

        public string Quantifier { get; }

        public int LowerBound { get; set; }

        public int? UpperBound { get; set; }

        public bool IsGreedy { get; set; }

        public bool NeedsGroupedToQuantify() => false;

        void IVisitableExpression.Accept(ExpressionVisitor visitor) => visitor.Visit(this);

        public override string ToString() => EncodingExpressionVisitor.ToString(this);
    }
}
