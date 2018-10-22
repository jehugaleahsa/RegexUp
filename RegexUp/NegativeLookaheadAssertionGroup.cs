using System;

namespace RegexUp
{
    internal sealed class NegativeLookaheadAssertionGroup : Group, INegativeLookaheadAssertionGroup
    {
        public bool NeedsGroupedToQuantify() => false;

        protected override string OnEncode()
        {
            var parts = new[] { "(?!", EncodeMembers(), ")" };
            var encoded = String.Join(String.Empty, parts);
            return encoded;
        }
    }
}
