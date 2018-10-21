using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RegexUp.Tests
{
    [TestClass]
    public class CharacterGroupTester
    {
        [TestMethod]
        public void LeadingCarot_Matches()
        {
            CharacterGroup group = new CharacterGroup();
            group.Add(Literal.For("^"));
            var regex = ToRegex(group);
            Assert.IsTrue(regex.IsMatch("^"));
        }

        [TestMethod]
        public void LeadingCarot_Negated_Matches()
        {
            CharacterGroup group = new CharacterGroup() { IsNegated = true };
            group.Add(Literal.For("^"));
            var regex = ToRegex(group);
            Assert.IsFalse(regex.IsMatch("^"));
        }

        [TestMethod]
        public void LeadingDash_Matches()
        {
            CharacterGroup group = new CharacterGroup();
            group.Add(Literal.For("-abc"));
            var regex = ToRegex(group);
            Assert.IsTrue(regex.IsMatch("-"));
        }

        [TestMethod]
        public void TrailingDash_Matches()
        {
            CharacterGroup group = new CharacterGroup();
            group.Add(Literal.For("abc-"));
            var regex = ToRegex(group);
            Assert.IsTrue(regex.IsMatch("-"));
        }

        [TestMethod]
        public void EmbeddedDash_Escapes_Matches()
        {
            CharacterGroup group = new CharacterGroup();
            group.Add(Literal.For("a-z"));
            var regex = ToRegex(group);
            Assert.IsTrue(regex.IsMatch("-"));
        }

        private static Regex ToRegex(IExpression expression)
        {
            RegularExpression regularExpression = new RegularExpression();
            regularExpression.Add(expression);
            return regularExpression.ToRegex();
        }
    }
}
