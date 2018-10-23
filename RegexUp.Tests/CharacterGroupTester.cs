using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RegexUp.Tests
{
    [TestClass]
    public class CharacterGroupTester
    {
        [TestMethod]
        public void LeadingCarot_Matches()
        {
            var regex = RegularExpression.Of(CharacterGroup.Of(Literal.For("^"))).ToRegex();
            Assert.IsTrue(regex.IsMatch("^"));
        }

        [TestMethod]
        public void LeadingCarot_Negated_Matches()
        {
            var regex = RegularExpression.Of(
                CharacterGroup.Of(new CharacterGroupOptions() { IsNegated = true }, Literal.For("^"))
            ).ToRegex();
            Assert.IsFalse(regex.IsMatch("^"));
        }

        [TestMethod]
        public void LeadingDash_Matches()
        {
            var regex = RegularExpression.Of(CharacterGroup.Of(Literal.For("-abc"))).ToRegex();
            Assert.IsTrue(regex.IsMatch("-"));
        }

        [TestMethod]
        public void TrailingDash_Matches()
        {
            var regex = RegularExpression.Of(CharacterGroup.Of(Literal.For("abc-"))).ToRegex();
            Assert.IsTrue(regex.IsMatch("-"));
        }

        [TestMethod]
        public void EmbeddedDash_Escapes_Matches()
        {
            var regex = RegularExpression.Of(CharacterGroup.Of(Literal.For("a-z"))).ToRegex();
            Assert.IsTrue(regex.IsMatch("-"));
        }
    }
}
