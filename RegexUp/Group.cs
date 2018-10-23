using System;
using System.Collections.Generic;
using System.Linq;
using RegexUp.Properties;

namespace RegexUp
{
    /// <summary>
    /// Provides factory methods for creating different groups.
    /// </summary>
    public abstract class Group : IGroup, IContainer, IExpressionEncoder
    {
        internal static void ValidateCaptureGroupName(string parameterName, string name)
        {
            if (name != null && (Char.IsDigit(name[0]) || name.Any(Char.IsPunctuation)))
            {
                throw new ArgumentException(Resources.InvalidCaptureGroupName, parameterName);
            }
        }

        private readonly List<IExpression> members = new List<IExpression>();

        protected Group()
        {
        }

        public IEnumerable<IExpression> Members => members;

        bool IExpression.NeedsGroupedToQuantify() => false;

        string IExpressionEncoder.Encode(ExpressionContext context)
        {
            return OnEncode();
        }

        protected abstract string OnEncode();

        protected virtual string EncodeMembers()
        {
            var encoded = String.Join(String.Empty, members.Cast<IExpressionEncoder>().Select(m => m.Encode(ExpressionContext.Group)));
            return encoded;
        }

        public void Add(IExpression member)
        {
            if (member == null)
            {
                throw new ArgumentNullException(nameof(member));
            }
            members.Add(member);
        }

        public override string ToString() => ((IExpressionEncoder)this).Encode(ExpressionContext.Group);
    }
}
