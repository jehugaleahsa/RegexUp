using System;

namespace RegexUp
{
    internal sealed class NegativeLookbehindAssertionGroup : Group, INegativeLookbehindAssertionGroup
    {
        protected override string OnEncode()
        {
            var parts = new[] { "(?<!", EncodeMembers(), ")" };
            var encoded = String.Join(String.Empty, parts);
            return encoded;
        }
    }
}
