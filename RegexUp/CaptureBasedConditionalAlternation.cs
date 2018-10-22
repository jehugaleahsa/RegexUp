using System;
using System.Collections.Generic;

namespace RegexUp
{
    /// <summary>
    /// Provides factory methods for creating alternations.
    /// </summary>
    internal sealed class CaptureBasedConditionalAlternation : ICaptureBasedConditionalAlternation, IExpressionEncoder
    {
        public CaptureBasedConditionalAlternation()
        {
        }

        public string Name { get; set; }

        public bool IsNamed { get; set; }

        public IExpression YesOption { get; set; }

        public IExpression NoOption { get; set; }

        public bool NeedsGroupedToQuantify() => false;

        public string Encode(ExpressionContext context)
        {
            var parts = new List<string>() { "(?(", Name, ")" };
            parts.Add(((IExpressionEncoder)YesOption).Encode(ExpressionContext.Alternation));
            if (NoOption != null)
            {
                parts.Add("|");
                parts.Add(((IExpressionEncoder)NoOption).Encode(ExpressionContext.Alternation));
            }
            parts.Add(")");
            var encoded = String.Join(String.Empty, parts);
            return encoded;
        }
    }
}
