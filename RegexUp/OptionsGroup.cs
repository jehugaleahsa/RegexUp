using System;
using System.Collections.Generic;

namespace RegexUp
{
    internal sealed class OptionsGroup : Group, IOptionsGroup
    {
        public GroupRegexOptions EnabledOptions { get; set; }

        public GroupRegexOptions DisabledOptions { get; set; }

        protected override string OnEncode()
        {
            var parts = new List<string>() { "(?", EncodeOptions(EnabledOptions) };
            if (DisabledOptions != GroupRegexOptions.None)
            {
                parts.Add("-");
                parts.Add(EncodeOptions(DisabledOptions));
            }
            parts.Add(":");
            parts.Add(EncodeMembers());
            parts.Add(")");
            var encoded = String.Join(String.Empty, parts);
            return encoded;
        }

        private string EncodeOptions(GroupRegexOptions options)
        {
            var parts = new List<string>();
            if ((options & GroupRegexOptions.IgnoreCase) == GroupRegexOptions.IgnoreCase)
            {
                parts.Add("i");
            }
            if ((options & GroupRegexOptions.Multiline) == GroupRegexOptions.Multiline)
            {
                parts.Add("m");
            }
            if ((options & GroupRegexOptions.ExplicitCapture) == GroupRegexOptions.ExplicitCapture)
            {
                parts.Add("n");
            }
            if ((options & GroupRegexOptions.Singleline) == GroupRegexOptions.Singleline)
            {
                parts.Add("s");
            }
            if ((options & GroupRegexOptions.IgnorePatternWhitespace) == GroupRegexOptions.IgnorePatternWhitespace)
            {
                parts.Add("x");
            }
            var encoded = String.Join(String.Empty, parts);
            return encoded;
        }
    }
}
