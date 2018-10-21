using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RegexUp.Tests
{
    [TestClass]
    public class AxiomTester
    {
        [TestMethod]
        public void AnchorsWithinCaptureGroups_Allowed()
        {
            Assert.IsTrue(new Regex("(^abc$)").IsMatch("abc"));
        }

        [TestMethod]
        public void AnchorsOutOfOrder_Allowed()
        {
            Assert.IsFalse(new Regex("$abc^").IsMatch("abc"));
        }

        [TestMethod]
        public void FindUnescapedDash_MiddleCharacter()
        {
            var regex = new Regex(@"(?<!\\)-");
            Assert.IsTrue(regex.IsMatch("a-z"));
        }

        [TestMethod]
        public void FindUnescapedDash_FirstCharacter()
        {
            var regex = new Regex(@"(?<!\\)-");
            Assert.IsTrue(regex.IsMatch("-z"));
        }

        [TestMethod]
        public void IgnoreEscapedDash()
        {
            var regex = new Regex(@"(?<!\\)-");
            Assert.IsFalse(regex.IsMatch(@"a\-z"));
        }

        [TestMethod]
        public void ReplaceUnescapedDashes()
        {
            var regex = new Regex(@"(?<!\\)-");
            var replacement = regex.Replace(@"a-zA-Z0-9\-$_&", @"\-");
            Assert.AreEqual(@"a\-zA\-Z0\-9\-$_&", replacement);
        }
    }
}
