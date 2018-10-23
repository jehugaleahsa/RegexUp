using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RegexUp.Tests
{
    [TestClass]
    public class GroupTester
    {
        [TestMethod]
        public void CaptureGroup_NoName()
        {
            var regex = RegularExpression.Of(
                CaptureGroup.Of(
                    Literal.For("A")
                )
            ).ToRegex();
            Assert.AreEqual("(A)", regex.ToString());
        }

        [TestMethod]
        public void CaptureGroup_Name()
        {
            var regex = RegularExpression.Of(
                CaptureGroup.Of(
                    new CaptureGroupOptions() { Name = "A" },
                    Literal.For("A")
                )
            ).ToRegex();
            Assert.AreEqual("(?<A>A)", regex.ToString());
        }

        [TestMethod]
        public void CaptureGroup_Name_WithQuotes()
        {
            var regex = RegularExpression.Of(
                CaptureGroup.Of(
                    new CaptureGroupOptions() { Name = "A", UseQuotes = true },
                    Literal.For("A")
                )
            ).ToRegex();
            Assert.AreEqual("(?'A'A)", regex.ToString());
        }

        [TestMethod]
        public void BalancedGroup_AngleBrackets()
        {
            var regex = RegularExpression.Of(
                CaptureGroup.Of(new CaptureGroupOptions() { Name = "Open" }, Literal.For("<")),
                BalancedGroup.Of("Close", "Open", Literal.For(">"))
            ).ToRegex();
            Assert.AreEqual("(?<Open><)(?<Close-Open>>)", regex.ToString());
        }

        [TestMethod]
        public void BalancedGroup_WithQuotes()
        {
            var regex = RegularExpression.Of(
                CaptureGroup.Of(new CaptureGroupOptions() { Name = "Open", UseQuotes = true }, Literal.For("<")),
                BalancedGroup.Of("Close", "Open", new BalanceGroupOptions() { UseQuotes = true }, Literal.For(">"))
            ).ToRegex();
            Assert.AreEqual("(?'Open'<)(?'Close-Open'>)", regex.ToString());
        }

        [TestMethod]
        public void NonCaptureGroup_Literal()
        {
            var regex = RegularExpression.Of(
                NonCaptureGroup.Of(Literal.For("A"))
            ).ToRegex();
            Assert.AreEqual("(?:A)", regex.ToString());
        }

        [TestMethod]
        public void OptionsGroup_EnableOnly()
        {
            var enabled = GroupRegexOptions.IgnoreCase | GroupRegexOptions.Multiline | GroupRegexOptions.ExplicitCapture | GroupRegexOptions.IgnorePatternWhitespace;
            var regex = RegularExpression.Of(
                OptionsGroup.Of(enabled, GroupRegexOptions.None, Literal.For("A"))
            ).ToRegex();
            Assert.AreEqual("(?imnx:A)", regex.ToString());
        }

        [TestMethod]
        public void OptionsGroup_DisableOnly()
        {
            var disabled = GroupRegexOptions.IgnoreCase | GroupRegexOptions.Multiline | GroupRegexOptions.ExplicitCapture | GroupRegexOptions.IgnorePatternWhitespace;
            var regex = RegularExpression.Of(
                OptionsGroup.Of(GroupRegexOptions.None, disabled, Literal.For("A"))
            ).ToRegex();
            Assert.AreEqual("(?-imnx:A)", regex.ToString());
        }

        [TestMethod]
        public void OptionsGroup_EnableAndDisable()
        {
            var options = GroupRegexOptions.IgnoreCase | GroupRegexOptions.Multiline | GroupRegexOptions.ExplicitCapture | GroupRegexOptions.IgnorePatternWhitespace;
            var regex = RegularExpression.Of(
                OptionsGroup.Of(options, options, Literal.For("A"))
            ).ToRegex();
            Assert.AreEqual("(?imnx-imnx:A)", regex.ToString());
        }

        [TestMethod]
        public void OptionsGroup_NoOptions()
        {
            var regex = RegularExpression.Of(
                OptionsGroup.Of(GroupRegexOptions.None, GroupRegexOptions.None, Literal.For("A"))
            ).ToRegex();
            Assert.AreEqual("(?:A)", regex.ToString());
        }

        [TestMethod]
        public void ZeroWidthPositiveLookaheadAssertion()
        {
            var regex = RegularExpression.Of(
                PositiveLookaheadAssertion.Of(Literal.For("A"))
            ).ToRegex();
            Assert.AreEqual("(?=A)", regex.ToString());
        }

        [TestMethod]
        public void ZeroWidthNegativeLookaheadAssertion()
        {
            var regex = RegularExpression.Of(
                NegativeLookaheadAssertion.Of(Literal.For("A"))
            ).ToRegex();
            Assert.AreEqual("(?!A)", regex.ToString());
        }

        [TestMethod]
        public void ZeroWidthPositiveLookbehindAssertion()
        {
            var regex = RegularExpression.Of(
                PositiveLookbehindAssertion.Of(Literal.For("A"))
            ).ToRegex();
            Assert.AreEqual("(?<=A)", regex.ToString());
        }

        [TestMethod]
        public void ZeroWidthNegativeLookbehindAssertion()
        {
            var regex = RegularExpression.Of(
                NegativeLookbehindAssertion.Of(Literal.For("A"))
            ).ToRegex();
            Assert.AreEqual("(?<!A)", regex.ToString());
        }

        [TestMethod]
        public void NonbacktrackingExpression()
        {
            var regex = RegularExpression.Of(
                NonbacktrackingAssertion.Of(Literal.For("A"))
            ).ToRegex();
            Assert.AreEqual("(?>A)", regex.ToString());
        }
    }
}
