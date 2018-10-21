using System;

namespace RegexUp
{
    internal sealed class BalancedGroup : Group, IBalancedGroup
    {
        public string Current { get; set; }

        public string Previous { get; set; }

        protected override string OnEncode()
        {
            var parts = new[] { "(?<", Current, "-", Previous, ">", EncodeMembers(), ")" };
            var encoded = String.Join(String.Empty, parts);
            return encoded;
        }
    }
}
