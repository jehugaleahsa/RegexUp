using System;
using System.Collections.Generic;

namespace RegexUp
{
    internal sealed class Quantifier : IQuantifiedExpression, IExpressionEncoder
    {
        private readonly IExpression expression;
        private readonly string quantifier;

        public Quantifier(IExpression expression, string quantifier)
        {
            this.expression = expression ?? throw new ArgumentNullException(nameof(expression));
            this.quantifier = quantifier;
        }

        public int LowerBound { get; set; }

        public int? UpperBound { get; set; }

        public bool IsGreedy { get; set; }

        public bool NeedsGroupedToQuantify() => false;

        public string Encode(ExpressionContext context, int position, int length)
        {
            string childEncoded = EncodeChildExpression(context);
            if (String.IsNullOrWhiteSpace(childEncoded))
            {
                return String.Empty;
            }
            var parts = new List<string>() { childEncoded, quantifier };
            if (!IsGreedy)
            {
                parts.Add("?");
            }
            var encoded = String.Join(string.Empty, parts);
            return encoded;
        }

        private string EncodeChildExpression(ExpressionContext context)
        {
            if (expression.NeedsGroupedToQuantify())
            {
                var group = NonCaptureGroup.Of(expression);
                return ((IExpressionEncoder)group).Encode(ExpressionContext.Group, 0, 1);
            }
            return ((IExpressionEncoder)expression).Encode(context, 0, 1);
        }

        public override string ToString() => Encode(ExpressionContext.Group, 0, 1);
    }
}
