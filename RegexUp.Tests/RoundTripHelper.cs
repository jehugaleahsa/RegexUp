using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RegexUp.Tests
{
    public static class RoundTripHelper
    {
        public static void AssertRoundTrips(string regex, RegexOptions options = RegexOptions.None, string expected = null)
        {
            var result = RegularExpression.From(new Regex(regex, options)).ToRegex(options);
            Assert.AreEqual(expected ?? regex, result.ToString());
        }
    }
}
