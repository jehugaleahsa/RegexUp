using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace RegexUp
{
    /// <summary>
    /// Represents the top-level regular expression.
    /// </summary>
    public sealed class RegularExpression
    {
        /// <summary>
        /// Creates a regular expression composed of the given subexpressions.
        /// </summary>
        /// <param name="expressions">The subexpressions making the regular expression.</param>
        /// <returns>The regular expression.</returns>
        public static RegularExpression Of(params IGroupMember[] expressions)
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

        /// <summary>
        /// Adds the given expression to the regular expression.
        /// </summary>
        /// <param name="expression">The expression to add.</param>
        public void Add(IGroupMember expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }
            expressions.Add(expression);
        }

        /// <summary>
        /// Builds a regular expression from the expression.
        /// </summary>
        /// <param name="expression">The expression to build the regular expression from.</param>
        /// <param name="options">A bitwise combination of the enumeration values that modify the regular expression.</param>
        /// <returns>The regular expression.</returns>
        public Regex ToRegex(RegexOptions options = RegexOptions.None)
        {
            var pattern = EncodeExpressions();
            return new Regex(pattern, options);
        }

        /// <summary>
        /// Builds a regular expression from the expression.
        /// </summary>
        /// <param name="expression">The expression to build the regular expression from.</param>
        /// <param name="options">A bitwise combination of the enumeration values that modify the regular expression.</param>
        /// <param name="matchTimeout">A time-out interval, or InfiniteMatchTimeout to indicate that the method should not time out.</param>
        /// <returns></returns>
        public Regex ToRegex(RegexOptions options, TimeSpan matchTimeout)
        {
            var pattern = EncodeExpressions();
            return new Regex(pattern, options, matchTimeout);
        }

        private string EncodeExpressions()
        {
            var encoded = String.Join(String.Empty, expressions.Cast<IExpression>().Select(e => e.Encode(ExpressionContext.Group)));
            return encoded;
        }

        public override string ToString() => EncodeExpressions();
    }
}
