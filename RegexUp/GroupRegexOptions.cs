using System.Text.RegularExpressions;

namespace RegexUp
{
    /// <summary>
    /// The supported regular expression options within an options group.
    /// </summary>
    public enum GroupRegexOptions
    {
        /// <summary>
        /// Use default behavior.
        /// </summary>
        None = RegexOptions.None,
        /// <summary>
        /// Use case-insensitive matching. (i)
        /// </summary>
        IgnoreCase = RegexOptions.IgnoreCase,
        /// <summary>
        /// Use multiline mode, where ^ and $ match the beginning and end of each line (instead of the beginning and end of the input string). (m)
        /// </summary>
        Multiline = RegexOptions.Multiline,
        /// <summary>
        /// Do not capture unnamed groups. The only valid captures are explicitly named or numbered groups of the form (?&lt;name&gt; subexpression). (n)
        /// </summary>
        ExplicitCapture = RegexOptions.ExplicitCapture,
        /// <summary>
        /// Use single-line mode, where the period (.) matches every character (instead of every character except \n). (s)
        /// </summary>
        Singleline = RegexOptions.Singleline,
        /// <summary>
        /// Exclude unescaped white space from the pattern, and enable comments after a number sign (#). (x)
        /// </summary>
        IgnorePatternWhitespace = RegexOptions.IgnorePatternWhitespace
    }
}
