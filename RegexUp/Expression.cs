using System;
using System.Collections.Generic;
using System.Linq;

namespace RegexUp
{
    /// <summary>
    /// Defines an expression composed one or more expressions.
    /// </summary>
    public class Expression : IExpression, IExpressionEncoder
    {
        /// <summary>
        /// Creates an expression consisting of multiple sub-expressions.
        /// </summary>
        /// <param name="members">The expressions comprising the expression.</param>
        /// <returns>The expression.</returns>
        public static IExpression Of(params IExpression[] members)
        {
            return From(members);
        }

        /// <summary>
        /// Creates an expression consisting of multiple sub-expressions.
        /// </summary>
        /// <param name="members">The expressions comprising the expression.</param>
        /// <returns>The expression.</returns>
        public static IExpression From(IEnumerable<IExpression> members)
        {
            if (members == null)
            {
                throw new ArgumentNullException(nameof(members));
            }
            var expression = new Expression();
            foreach (var member in members)
            {
                expression.Add(member);
            }
            return expression;
        }

        private readonly List<IExpression> members = new List<IExpression>();

        private Expression()
        {
        }

        public void Add(IExpression member)
        {
            members.Add(member);
        }

        bool IExpression.NeedsGroupedToQuantify() => members.Count > 1 || members[0].NeedsGroupedToQuantify();

        string IExpressionEncoder.Encode(ExpressionContext context)
        {
            string encoded = String.Join(String.Empty, members.Cast<IExpressionEncoder>().Select(e => e.Encode(context)));
            return encoded;
        }
    }
}
