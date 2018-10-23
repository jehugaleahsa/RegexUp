using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace RegexUp
{
    /// <summary>
    /// Provides factory methods for creating regular expressions.
    /// </summary>
    public sealed class RegularExpression : IRegularExpression, IContainer
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

        private readonly List<IExpression> members = new List<IExpression>();

        private RegularExpression()
        {
        }

        public IEnumerable<IExpression> Members => members;
        
        public void Add(IExpression members)
        {
            if (members == null)
            {
                throw new ArgumentNullException(nameof(members));
            }
            this.members.Add(members);
        }
        
        public Regex ToRegex(RegexOptions options = RegexOptions.None)
        {
            var pattern = EncodeMembers();
            return new Regex(pattern, options);
        }

        public Regex ToRegex(RegexOptions options, TimeSpan matchTimeout)
        {
            var pattern = EncodeMembers();
            return new Regex(pattern, options, matchTimeout);
        }

        private string EncodeMembers()
        {
            var encoded = String.Join(String.Empty, members.Cast<IExpressionEncoder>().Select(e => e.Encode(ExpressionContext.Group)));
            return encoded;
        }

        public override string ToString() => EncodeMembers();
    }
}
