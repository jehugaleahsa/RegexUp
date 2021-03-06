﻿using System;
using System.Collections.Generic;

namespace RegexUp
{
    /// <summary>
    /// Provides factory methods for creating a series of expressions.
    /// </summary>
    public sealed class CompoundExpression : ICompoundExpression, IContainer
    {
        /// <summary>
        /// Creates an expression consisting of multiple sub-expressions.
        /// </summary>
        /// <param name="members">The expressions comprising the expression.</param>
        /// <returns>The expression.</returns>
        public static ICompoundExpression Of(params IExpression[] members)
        {
            return From(members);
        }

        /// <summary>
        /// Creates an expression consisting of multiple sub-expressions.
        /// </summary>
        /// <param name="members">The expressions comprising the expression.</param>
        /// <returns>The expression.</returns>
        public static ICompoundExpression From(IEnumerable<IExpression> members)
        {
            if (members == null)
            {
                throw new ArgumentNullException(nameof(members));
            }
            var expression = new CompoundExpression();
            foreach (var member in members)
            {
                expression.Add(member);
            }
            return expression;
        }

        private readonly List<IExpression> members = new List<IExpression>();

        internal CompoundExpression()
        {
        }

        public IEnumerable<IExpression> Members => members;

        internal void Add(IExpression member)
        {
            if (member == null)
            {
                throw new ArgumentNullException(nameof(member));
            }
            if (member is ICompoundExpression compoundExpression)
            {
                members.AddRange(compoundExpression.Members);
            }
            else
            {
                members.Add(member);
            }
        }

        internal void Remove()
        {
            members.RemoveAt(members.Count - 1);
        }

        void IContainer.Add(IExpression expression) => Add(expression);

        public IExpression Normalize() => members.Count == 1 ? members[0] : this;

        bool IExpression.NeedsGroupedToQuantify() => members.Count > 1 || members[0].NeedsGroupedToQuantify();

        void IVisitableExpression.Accept(ExpressionVisitor visitor) => visitor.Visit(this);

        public override string ToString() => EncodingExpressionVisitor.ToString(this);
    }
}
