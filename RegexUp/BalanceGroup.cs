using System;

namespace RegexUp
{
    internal sealed class BalancedGroup : Group, IBalancedGroup
    {
        public string Current { get; set; }

        public string Previous { get; set; }

        public bool UseQuotes { get; set; }

        public bool NeedsGroupedToQuantify() => false;

        protected override string OnEncode()
        {
            var parts = new[] 
            {
                "(?",
                UseQuotes ? "'" : "<",
                Current,
                "-",
                Previous,
                UseQuotes ? "'" : ">",
                EncodeMembers(),
                ")"
            };
            var encoded = String.Join(String.Empty, parts);
            return encoded;
        }
    }
}
