using System.Text.RegularExpressions;

namespace RegexUp
{
    internal static class GroupRegexOptionsUtilties
    {
        public static GroupRegexOptions GetGroupOptions(RegexOptions options)
        {
            var sanitized = options & (
                RegexOptions.ExplicitCapture 
                | RegexOptions.IgnoreCase 
                | RegexOptions.IgnorePatternWhitespace
                | RegexOptions.Multiline
                | RegexOptions.Singleline
            );
            var result = (GroupRegexOptions)sanitized;
            return result;
        }
    }
}
