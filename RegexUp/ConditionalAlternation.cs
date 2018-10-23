using System;
using System.Collections.Generic;

namespace RegexUp
{
    /// <summary>
    /// Provides factory methods for creating alternations.
    /// </summary>
    internal sealed class ConditionalAlternation : IExpressionBasedConditionalAlternation, IExpressionEncoder
    {
        public ConditionalAlternation()
        {
        }
        
        public IExpression Expression { get; set; }

        public IExpression YesOption { get; set; }

        public IExpression NoOption { get; set; }

        public bool NeedsGroupedToQuantify() => false;

        string IExpressionEncoder.Encode(ExpressionContext context)
        {
            var parts = new List<string>() { "(?(" };
            parts.Add(((IExpressionEncoder)Expression).Encode(ExpressionContext.Group));
            parts.Add( ")");
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
