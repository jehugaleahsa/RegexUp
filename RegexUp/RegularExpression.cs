using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RegexUp
{
    /// <summary>
    /// Provides factory methods for creating regular expressions.
    /// </summary>
    public sealed class RegularExpression : IRegularExpression
    {
        /// <summary>
        /// Creates a regular expression composed of the given subexpressions.
        /// </summary>
        /// <param name="expressions">The subexpressions making the regular expression.</param>
        /// <returns>The regular expression.</returns>
        public static IRegularExpression Of(params IExpression[] expressions)
        {
            return From(expressions);
        }

        /// <summary>
        /// Creates a regular expression composed of the given subexpressions.
        /// </summary>
        /// <param name="expressions">The subexpressions making the regular expression.</param>
        /// <returns>The regular expression.</returns>
        public static IRegularExpression From(IEnumerable<IExpression> expressions)
        {
            if (expressions == null)
            {
                throw new ArgumentNullException(nameof(expressions));
            }
            var regularExpression = new RegularExpression();
            foreach (var expression in expressions)
            {
                regularExpression.Add(expression);
            }
            return regularExpression;
        }

        /// <summary>
        /// Creates a regular expression from the given regular expression.
        /// </summary>
        /// <param name="regex">The regular expression to parse.</param>
        /// <returns>The regular expression.</returns>
        public static IRegularExpression From(Regex regex)
        {
            if (regex == null)
            {
                throw new ArgumentNullException(nameof(regex));
            }
            var parser = new RegularExpressionParser();
            var regularExpression = new RegularExpression();
            parser.Parse(regularExpression, regex.ToString());
            return regularExpression;
        }

        private readonly CompoundExpression members = new CompoundExpression();

        private RegularExpression()
        {
        }

        public IEnumerable<IExpression> Members => members.Members;
        
        internal void Add(IExpression member)
        {
            if (member == null)
            {
                throw new ArgumentNullException(nameof(member));
            }
            members.Add(member);
        }
        
        public Regex ToRegex(RegexOptions options = RegexOptions.None)
        {
            var pattern = EncodingExpressionVisitor.ToString(members);
            return new Regex(pattern, options);
        }

        public Regex ToRegex(RegexOptions options, TimeSpan matchTimeout)
        {
            var pattern = EncodingExpressionVisitor.ToString(members);
            return new Regex(pattern, options, matchTimeout);
        }

        public IExpression AsExpression()
        {
            return CompoundExpression.From(Members);
        }

        void IVisitableExpression.Accept(ExpressionVisitor visitor) => visitor.Visit(this);

        public override string ToString() => EncodingExpressionVisitor.ToString(this);
    }
}
