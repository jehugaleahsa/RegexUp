using System;
using System.Collections.Generic;
using System.Linq;
using RegexUp.Properties;

namespace RegexUp
{
    /// <summary>
    /// Provides factory methods for creating different groups.
    /// </summary>
    public abstract class Group : IGroup, IContainer
    {
        internal static void ValidateCaptureGroupName(string parameterName, string name)
        {
            if (name != null && (Char.IsDigit(name[0]) || name.Any(Char.IsPunctuation)))
            {
                throw new ArgumentException(Resources.InvalidCaptureGroupName, parameterName);
            }
        }

        internal static void ValidateRegexOptions(string parameterName, GroupRegexOptions options)
        {
            if ((options & GroupRegexOptions.Multiline) == GroupRegexOptions.Multiline && (options & GroupRegexOptions.Singleline) == GroupRegexOptions.Singleline)
            {
                throw new ArgumentException(Resources.SingleAndMultilineMode, parameterName);
            }
            var allOptions = GroupRegexOptions.IgnoreCase | GroupRegexOptions.Multiline | GroupRegexOptions.ExplicitCapture | GroupRegexOptions.Singleline | GroupRegexOptions.IgnorePatternWhitespace;
            options &= ~allOptions;
            if (options != 0)
            {
                throw new ArgumentException(Resources.InvalidGroupOptions, parameterName);
            }
        }

        private readonly List<IExpression> members = new List<IExpression>();

        protected Group()
        {
        }

        public IEnumerable<IExpression> Members => members;

        bool IExpression.NeedsGroupedToQuantify() => false;

        public void Add(IExpression member)
        {
            if (member == null)
            {
                throw new ArgumentNullException(nameof(member));
            }
            members.Add(member);
        }

        void IVisitableExpression.Accept(ExpressionVisitor visitor)
        {
            OnAccept(visitor);
        }

        protected abstract void OnAccept(ExpressionVisitor visitor);
    }
}
