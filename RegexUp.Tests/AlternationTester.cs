using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RegexUp.Tests
{
    [TestClass]
    public class AlternationTester
    {
        [TestMethod]
        public void Alternation_Multiple()
        {
            var regex = RegularExpression.Of(
                Alternation.Of(
                    Literal.For("a"), Literal.For("b"), Literal.For("c")
                )
            ).ToRegex();
            Assert.AreEqual("a|b|c", regex.ToString());
        }

        [TestMethod]
        public void Alternation_ByExpression()
        {
            var regex = RegularExpression.Of(
                Alternation.Conditional.ByExpression(Literal.For("A"), Literal.For("Abc"), Literal.For("Xyz"))
            ).ToRegex();
            Assert.AreEqual("(?(A)Abc|Xyz)", regex.ToString());
        }

        [TestMethod]
        public void Alternation_ByCapture()
        {
            var regex = RegularExpression.Of(
                Group.Capture.Of(new CaptureGroupOptions() { Name = "A" }, Quantifiers.OneOrMore(Literal.For("A"))), 
                Alternation.Conditional.ByExpression(Literal.For("A"), Literal.For("Abc"), Literal.For("Xyz"))
            ).ToRegex();
            Assert.AreEqual("(?<A>A+)(?(A)Abc|Xyz)", regex.ToString());
        }
    }
}
