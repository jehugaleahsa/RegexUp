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
                Group.Capture.Of(
                    Literal.For("A")
                )
            ).ToRegex();
            Assert.AreEqual("(A)", regex.ToString());
        }

        [TestMethod]
        public void CaptureGroup_Name()
        {
            var regex = RegularExpression.Of(
                Group.Capture.Of(
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
                Group.Capture.Of(
                    new CaptureGroupOptions() { Name = "A", UseQuotes = true },
                    Literal.For("A")
                )
            ).ToRegex();
            Assert.AreEqual("(?'A'A)", regex.ToString());
        }

        [TestMethod]
        public void BalancedGroup()
        {
            var regex = RegularExpression.Of(
                Group.Capture.Of(new CaptureGroupOptions() { Name = "Open" }, Literal.For("<")),
                Group.Balanced.Of("Close", "Open", Literal.For(">"))
            ).ToRegex();
            Assert.AreEqual("(?<Open><)(?<Close-Open>>)", regex.ToString());
        }

        [TestMethod]
        public void BalancedGroup_WithQuotes()
        {
            var regex = RegularExpression.Of(
                Group.Capture.Of(new CaptureGroupOptions() { Name = "Open", UseQuotes = true }, Literal.For("<")),
                Group.Balanced.Of("Close", "Open", new BalanceGroupOptions() { UseQuotes = true }, Literal.For(">"))
            ).ToRegex();
            Assert.AreEqual("(?'Open'<)(?'Close-Open'>)", regex.ToString());
        }

        [TestMethod]
        public void NonCaptureGroup()
        {
            var regex = RegularExpression.Of(
                Group.NonCapture.Of(Literal.For("A"))
            ).ToRegex();
            Assert.AreEqual("(?:A)", regex.ToString());
        }

        [TestMethod]
        public void OptionsGroup_EnableOnly()
        {
            var enabled = GroupRegexOptions.IgnoreCase | GroupRegexOptions.Multiline | GroupRegexOptions.ExplicitCapture | GroupRegexOptions.IgnorePatternWhitespace;
            var regex = RegularExpression.Of(
                Group.Options.Of(enabled, GroupRegexOptions.None, Literal.For("A"))
            ).ToRegex();
            Assert.AreEqual("(?imnx:A)", regex.ToString());
        }

        [TestMethod]
        public void OptionsGroup_DisableOnly()
        {
            var disabled = GroupRegexOptions.IgnoreCase | GroupRegexOptions.Multiline | GroupRegexOptions.ExplicitCapture | GroupRegexOptions.IgnorePatternWhitespace;
            var regex = RegularExpression.Of(
                Group.Options.Of(GroupRegexOptions.None, disabled, Literal.For("A"))
            ).ToRegex();
            Assert.AreEqual("(?-imnx:A)", regex.ToString());
        }

        [TestMethod]
        public void OptionsGroup_EnableAndDisable()
        {
            var options = GroupRegexOptions.IgnoreCase | GroupRegexOptions.Multiline | GroupRegexOptions.ExplicitCapture | GroupRegexOptions.IgnorePatternWhitespace;
            var regex = RegularExpression.Of(
                Group.Options.Of(options, options, Literal.For("A"))
            ).ToRegex();
            Assert.AreEqual("(?imnx-imnx:A)", regex.ToString());
        }

        [TestMethod]
        public void OptionsGroup_NoOptions()
        {
            var regex = RegularExpression.Of(
                Group.Options.Of(GroupRegexOptions.None, GroupRegexOptions.None, Literal.For("A"))
            ).ToRegex();
            Assert.AreEqual("(?:A)", regex.ToString());
        }

        [TestMethod]
        public void ZeroWidthPositiveLookaheadAssertion()
        {
            var regex = RegularExpression.Of(
                Group.LookaheadAssertions.Positive.Of(Literal.For("A"))
            ).ToRegex();
            Assert.AreEqual("(?=A)", regex.ToString());
        }

        [TestMethod]
        public void ZeroWidthNegativeLookaheadAssertion()
        {
            var regex = RegularExpression.Of(
                Group.LookaheadAssertions.Negative.Of(Literal.For("A"))
            ).ToRegex();
            Assert.AreEqual("(?!A)", regex.ToString());
        }

        [TestMethod]
        public void ZeroWidthPositiveLookbehindAssertion()
        {
            var regex = RegularExpression.Of(
                Group.LookbehindAssertions.Positive.Of(Literal.For("A"))
            ).ToRegex();
            Assert.AreEqual("(?<=A)", regex.ToString());
        }

        [TestMethod]
        public void ZeroWidthNegativeLookbehindAssertion()
        {
            var regex = RegularExpression.Of(
                Group.LookbehindAssertions.Negative.Of(Literal.For("A"))
            ).ToRegex();
            Assert.AreEqual("(?<!A)", regex.ToString());
        }

        [TestMethod]
        public void NonbacktrackingExpression()
        {
            var regex = RegularExpression.Of(
                Group.Nonbacktracking.Of(Literal.For("A"))
            ).ToRegex();
            Assert.AreEqual("(?>A)", regex.ToString());
        }
    }
}
