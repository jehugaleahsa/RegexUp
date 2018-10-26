using System;
using RegexUp.Properties;

namespace RegexUp
{
    /// <summary>
    /// Provide factory methods for creatig x-mode comments.
    /// </summary>
    public sealed class XModeComment : IXModeComment
    {
        public static IXModeComment For(string comment)
        {
            if (comment == null)
            {
                throw new ArgumentNullException(nameof(comment));
            }
            if (comment.IndexOf('\n') != -1)
            {
                throw new ArgumentException(Resources.InvalidXModeComment, nameof(comment));
            }
            return new XModeComment(comment);
        }

        private XModeComment(string comment)
        {
            Comment = comment;
        }

        public string Comment { get; }

        public void Accept(ExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString() => EncodingExpressionVisitor.ToString(this);

        public bool NeedsGroupedToQuantify() => false;
    }
}
