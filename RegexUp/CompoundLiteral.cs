using System;
using System.Collections.Generic;
using System.Linq;

namespace RegexUp
{
    internal sealed class CompoundLiteral : ICompoundLiteral
    {
        private readonly List<ILiteral> literals = new List<ILiteral>();

        public CompoundLiteral()
        {
        }

        public IEnumerable<ILiteral> Literals => literals;

        public string Value => String.Join(String.Empty, literals.Select(l => l.Value));

        public void Add(ILiteral literal)
        {
            if (literal == null)
            {
                throw new ArgumentNullException(nameof(literal));
            }
            literals.Add(literal);
        }

        public bool NeedsGroupedToQuantify() => true;

        void IVisitableExpression.Accept(ExpressionVisitor visitor) => visitor.Visit(this);

        public override string ToString() => EncodingExpressionVisitor.ToString(this);
    }
}
