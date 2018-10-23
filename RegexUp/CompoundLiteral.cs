using System;
using System.Collections.Generic;
using System.Linq;

namespace RegexUp
{
    internal sealed class CompoundLiteral : ICompoundLiteral, IExpressionEncoder
    {
        private readonly List<ILiteral> literals = new List<ILiteral>();

        public CompoundLiteral()
        {
        }

        public string Value => String.Join(String.Empty, literals.Select(l => l.Value));

        public void Add(ILiteral literal)
        {
            literals.Add(literal);
        }

        public string Encode(ExpressionContext context)
        {
            var encoded = String.Join(String.Empty, literals.Cast<IExpressionEncoder>().Select(e => e.Encode(context)));
            return encoded;
        }

        public bool NeedsGroupedToQuantify() => true;
    }
}
