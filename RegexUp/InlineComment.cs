using System;
using RegexUp.Properties;

namespace RegexUp
{
    public sealed class InlineComment : IInlineComment, IExpressionEncoder
    {
        public static IInlineComment For(string comment)
        {
            if (comment == null)
            {
                comment = String.Empty;
            }
            else if (comment.Contains(")"))
            {
                throw new ArgumentException(Resources.InvalidComment, nameof(comment));
            }
            return new InlineComment(comment);
        }

        private InlineComment(string comment)
        {
            Comment = comment;
        }

        public string Comment { get; }

        string IExpressionEncoder.Encode(ExpressionContext context, int position, int length)
        {
            return $"(?#{Comment})";
        }

        bool IExpression.NeedsGroupedToQuantify() => false;

        public override string ToString() => ((IExpressionEncoder)this).Encode(ExpressionContext.Group, 0, 1);
    }
}
