using System;
using System.Collections.Generic;
using System.Linq;

namespace RegexUp
{
    /// <summary>
    /// Provides factory methods for creating alternations.
    /// </summary>
    public sealed class Alternation : IAlternation, IExpression, IContainer, IExpressionEncoder
    {
        /// <summary>
        /// Creates an alternation consisting of the given alternatives.
        /// </summary>
        /// <param name="alternatives">The alternatives comprising the alternation.</param>
        /// <returns>The alternation.</returns>
        public static IAlternation Of(params IExpression[] alternatives)
        {
            return From(alternatives);
        }

        /// <summary>
        /// Creates an alternation consisting of the given alternatives.
        /// </summary>
        /// <param name="alternatives">The alternatives comprising the alternation.</param>
        /// <returns>The alternation.</returns>
        public static IAlternation From(IEnumerable<IExpression> alternatives)
        {
            if (alternatives == null)
            {
                throw new ArgumentNullException(nameof(alternatives));
            }
            var alternation = new Alternation();
            foreach (var alternative in alternatives)
            {
                alternation.Add(alternative);
            }
            return alternation;
        }

        private readonly List<IExpression> alternatives = new List<IExpression>();

        internal Alternation()
        {
        }

        public IEnumerable<IExpression> Alternatives => alternatives;

        public void Add(IExpression alterivative)
        {
            if (alterivative == null)
            {
                throw new ArgumentNullException(nameof(alterivative));
            }
            alternatives.Add(alterivative);
        }

        bool IExpression.NeedsGroupedToQuantify()
        {
            return alternatives.Count > 1 || alternatives[0].NeedsGroupedToQuantify();
        }

        string IExpressionEncoder.Encode(ExpressionContext context, int position, int length)
        {
            string encoded = String.Join("|", alternatives
                .Cast<IExpressionEncoder>()
                .Select((e, i) => e.Encode(ExpressionContext.Alternation, i, alternatives.Count))
            );
            return encoded;
        }

        public override string ToString() => ((IExpressionEncoder)this).Encode(ExpressionContext.Group, 0, 1);
    }
}
