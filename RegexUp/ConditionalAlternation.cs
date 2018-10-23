using System;
using System.Collections.Generic;

namespace RegexUp
{
    /// <summary>
    /// Provides factory methods for creating alternations.
    /// </summary>
    public sealed class ConditionalAlternation : IConditionalAlternation, IExpressionEncoder
    {
        /// <summary>
        /// Creates an alternation that tries to match the 'yes' option if the expression is matched.
        /// </summary>
        /// <param name="expression">The expression that must be matched for the 'yes' option to be matched.</param>
        /// <param name="yes">The alternative to match if the capture group is matched.</param>
        /// <param name="no">The alternative to match if the capture group is not matched.</param>
        /// <returns>The alternation.</returns>
        public static IConditionalAlternation For(IExpression expression, IExpression yes, IExpression no)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }
            if (yes == null)
            {
                throw new ArgumentNullException(nameof(yes));
            }
            var alternation = new ConditionalAlternation()
            {
                Expression = expression,
                YesOption = yes,
                NoOption = no
            };
            return alternation;
        }

        internal ConditionalAlternation()
        {
        }
        
        public IExpression Expression { get; set; }

        public IExpression YesOption { get; set; }

        public IExpression NoOption { get; set; }

        public bool NeedsGroupedToQuantify() => false;

        string IExpressionEncoder.Encode(ExpressionContext context, int position, int length)
        {
            var parts = new List<string>() { "(?(" };
            parts.Add(((IExpressionEncoder)Expression).Encode(ExpressionContext.Group, 0, 1));
            parts.Add( ")");
            parts.Add(((IExpressionEncoder)YesOption).Encode(ExpressionContext.Alternation, 0, 2));
            if (NoOption != null)
            {
                parts.Add("|");
                parts.Add(((IExpressionEncoder)NoOption).Encode(ExpressionContext.Alternation, 1, 2));
            }
            parts.Add(")");
            var encoded = String.Join(String.Empty, parts);
            return encoded;
        }

        public override string ToString() => ((IExpressionEncoder)this).Encode(ExpressionContext.Group, 0, 1);
    }
}
