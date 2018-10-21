using System;

namespace RegexUp
{
    internal sealed class NonCaptureGroup : Group, INonCaptureGroup
    {
        protected override string OnEncode()
        {
            var parts = new[] { "(?:", EncodeMembers(), ")" };
            var encoded = String.Join(String.Empty, parts);
            return encoded;
        }
    }
}
