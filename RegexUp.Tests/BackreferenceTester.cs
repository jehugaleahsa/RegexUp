using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RegexUp.Tests
{
    [TestClass]
    public class BackreferenceTester
    {
        [TestMethod]
        public void Backreference_Number()
        {
            var regex = RegularExpression.Of(
                Group.Capture.Of(Quantifiers.OneOrMore(Literal.For("a"))),
                Quantifiers.ZeroOrMore(Literal.For("b")),
                Backreference.For(1)
            ).ToRegex();
            Assert.AreEqual(@"(a+)b*\1", regex.ToString());
        }

        [TestMethod]
        public void Backreference_Name()
        {
            var regex = RegularExpression.Of(
                Group.Capture.Of(new CaptureGroupOptions() { Name = "a" }, Quantifiers.OneOrMore(Literal.For("a"))),
                Quantifiers.ZeroOrMore(Literal.For("b")),
                Backreference.For("a")
            ).ToRegex();
            Assert.AreEqual(@"(?<a>a+)b*\k<a>", regex.ToString());
        }

        [TestMethod]
        public void Backreference_Name_WithQuotes()
        {
            var regex = RegularExpression.Of(
                Group.Capture.Of(new CaptureGroupOptions() { Name = "a", UseQuotes = true }, Quantifiers.OneOrMore(Literal.For("a"))),
                Quantifiers.ZeroOrMore(Literal.For("b")),
                Backreference.For("a", true)
            ).ToRegex();
            Assert.AreEqual(@"(?'a'a+)b*\k'a'", regex.ToString());
        }
    }
}
