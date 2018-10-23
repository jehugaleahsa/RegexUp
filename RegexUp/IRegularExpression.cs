using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RegexUp
{
    /// <summary>
    /// Represents the top-level regular expression.
    /// </summary>
    public interface IRegularExpression
    {
        IEnumerable<IExpression> Members { get; }

        /// <summary>
        /// Adds the given expression to the regular expression.
        /// </summary>
        /// <param name="member">The expression to add.</param>
        void Add(IExpression member);

        /// <summary>
        /// Builds a regular expression from the expression.
        /// </summary>
        /// <param name="expression">The expression to build the regular expression from.</param>
        /// <param name="options">A bitwise combination of the enumeration values that modify the regular expression.</param>
        /// <returns>The regular expression.</returns>
        Regex ToRegex(RegexOptions options = RegexOptions.None);

        /// <summary>
        /// Builds a regular expression from the expression.
        /// </summary>
        /// <param name="expression">The expression to build the regular expression from.</param>
        /// <param name="options">A bitwise combination of the enumeration values that modify the regular expression.</param>
        /// <param name="matchTimeout">A time-out interval, or InfiniteMatchTimeout to indicate that the method should not time out.</param>
        /// <returns></returns>
        Regex ToRegex(RegexOptions options, TimeSpan matchTimeout);
    }
}
