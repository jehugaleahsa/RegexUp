using System;
using System.Collections.Generic;
using System.Linq;
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
        public static IRegularExpression Of(params IGroupMember[] expressions)
        {
            return From(expressions);
        }

        /// <summary>
        /// Creates a regular expression composed of the given subexpressions.
        /// </summary>
        /// <param name="expressions">The subexpressions making the regular expression.</param>
        /// <returns>The regular expression.</returns>
        public static IRegularExpression From(IEnumerable<IGroupMember> expressions)
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

        private readonly List<IGroupMember> expressions = new List<IGroupMember>();

        private RegularExpression()
        {
        }
        
        public void Add(IGroupMember expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }
            expressions.Add(expression);
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
            var encoded = String.Join(String.Empty, expressions.Cast<IExpression>().Select(e => e.Encode(ExpressionContext.Group)));
            return encoded;
        }

        public override string ToString() => EncodeMembers();
    }
}
