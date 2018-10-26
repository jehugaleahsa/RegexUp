using System;
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
        
        [TestMethod]
        public void QuantifyAnchor()
        {
            var regex = new Regex(@"^*");
            Assert.IsTrue(regex.IsMatch("*"));
            Assert.IsTrue(regex.IsMatch("abc"));
        }

        [TestMethod]
        public void EmptyCaptureGroup_CapturesEmpty()
        {
            var regex = new Regex("()");
            var match = regex.Match("hello");
            Assert.IsTrue(match.Success);
            Assert.IsTrue(match.Groups[1].Success);
            Assert.AreEqual("", match.Groups[1].Value);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CaptureGroup_LeadingQuestionMark_Illegal()
        {
            new Regex("(?abc)");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CaptureGroup_EmptyName_Illegal()
        {
            new Regex("(?''abc)");
        }

        [TestMethod]
        public void BalanceGroup_MissingCurrentName_IsOptional()
        {
            new Regex(@"((?'Open'\()[^)]*)+((?'-Open')[^()]*)+");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void BalanceGroup_MissingPreviousName_Illegal()
        {
            new Regex(@"((?'Open'\()[^)]*)+((?'Current-')[^()]*)+");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SolitaryRightParanthesis_Illegal()
        {
            new Regex(@")");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InlineComment_EscapedClosingParenthesis_Illegal()
        {
            new Regex(@"(?#This is a message with an \) escaped parenthesis)");
        }

        [TestMethod]
        public void PatternWhitespace_WhitespaceIgnored()
        {
            var regex = new Regex(@"This is a pattern with whitespace", RegexOptions.IgnorePatternWhitespace);
            Assert.IsTrue(regex.IsMatch("Thisisapatternwithwhitespace"));
        }

        [TestMethod]
        public void PatternWhitespace_CharacterGroup_WhitespaceNotIgnored()
        {
            var regex = new Regex(@"[a-z ]", RegexOptions.IgnorePatternWhitespace);
            Assert.IsTrue(regex.IsMatch(" "));
        }

        [TestMethod]
        public void DuplicateGroupNames_BothMatch_SingleIndex_MultipleCaptures()
        {
            var regex = new Regex(@"(?<name>abc)\d*(?<name>def)");
            var match = regex.Match("abc123def");
            var nameGroup = match.Groups["name"];
            Assert.AreEqual(2, nameGroup.Captures.Count);
        }
    }
}
