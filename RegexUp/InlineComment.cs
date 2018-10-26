using System;
using RegexUp.Properties;

namespace RegexUp
{
    public sealed class InlineComment : IInlineComment
    {
        public static IInlineComment For(string comment)
        {
            if (comment == null)
            {
                throw new ArgumentNullException(nameof(comment));
            }
            if (comment.IndexOfAny(new[] { ')', '\n' }) != -1)
            {
                throw new ArgumentException(Resources.InvalidInlineComment, nameof(comment));
            }
            return new InlineComment(comment);
        }

        private InlineComment(string comment)
        {
            Comment = comment;
        }

        public string Comment { get; }

        bool IExpression.NeedsGroupedToQuantify() => false;

        void IVisitableExpression.Accept(ExpressionVisitor visitor) => visitor.Visit(this);

        public override string ToString() => EncodingExpressionVisitor.ToString(this);
    }
}
