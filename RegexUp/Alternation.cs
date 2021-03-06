﻿using System;
using System.Collections.Generic;

namespace RegexUp
{
    /// <summary>
    /// Provides factory methods for creating alternations.
    /// </summary>
    public sealed class Alternation : IAlternation, IExpression, IContainer
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

        internal void Add(IExpression alterivative)
        {
            if (alterivative == null)
            {
                throw new ArgumentNullException(nameof(alterivative));
            }
            if (alterivative is IAlternation other)
            {
                alternatives.AddRange(other.Alternatives);
            }
            else
            {
                alternatives.Add(alterivative);
            }
        }

        void IContainer.Add(IExpression expression) => Add(expression);

        bool IExpression.NeedsGroupedToQuantify()
        {
            return alternatives.Count > 1 || alternatives[0].NeedsGroupedToQuantify();
        }

        void IVisitableExpression.Accept(ExpressionVisitor visitor) => visitor.Visit(this);

        public override string ToString() => EncodingExpressionVisitor.ToString(this);
    }
}
