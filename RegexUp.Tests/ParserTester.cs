using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RegexUp.Tests
{
    [TestClass]
    public class ParserTester
    {
        #region Anchors

        [TestMethod]
        public void Parse_Anchor_StartsWith()
        {
            RoundTripHelper.AssertRoundTrips("^");
        }

        [TestMethod]
        public void Parse_Anchor_EndsWith()
        {
            RoundTripHelper.AssertRoundTrips("$");
        }

        [TestMethod]
        public void Parse_Anchor_A()
        {
            RoundTripHelper.AssertRoundTrips(@"\A");
        }

        [TestMethod]
        public void Parse_Anchor_Z()
        {
            RoundTripHelper.AssertRoundTrips(@"\Z");
        }

        [TestMethod]
        public void Parse_Anchor_z()
        {
            RoundTripHelper.AssertRoundTrips(@"\z");
        }

        [TestMethod]
        public void Parse_Anchor_G()
        {
            RoundTripHelper.AssertRoundTrips(@"\G");
        }

        [TestMethod]
        public void Parse_Anchor_b()
        {
            RoundTripHelper.AssertRoundTrips(@"\b");
        }

        [TestMethod]
        public void Parse_Anchor_B()
        {
            RoundTripHelper.AssertRoundTrips(@"\B");
        }

        [TestMethod]
        public void Parse_Anchor_Quantified()
        {
            RoundTripHelper.AssertRoundTrips(@"^*");
        }

        #endregion

        #region CharacterClass

        [TestMethod]
        public void Parse_CharacterClass_Wildcard()
        {
            RoundTripHelper.AssertRoundTrips(".");
        }

        [TestMethod]
        public void Parse_CharacterClass_Word()
        {
            RoundTripHelper.AssertRoundTrips(@"\w");
        }

        [TestMethod]
        public void Parse_CharacterClass_NonWord()
        {
            RoundTripHelper.AssertRoundTrips(@"\W");
        }

        [TestMethod]
        public void Parse_CharacterClass_Whitespace()
        {
            RoundTripHelper.AssertRoundTrips(@"\s");
        }

        [TestMethod]
        public void Parse_CharacterClass_NonWhitespace()
        {
            RoundTripHelper.AssertRoundTrips(@"\S");
        }

        [TestMethod]
        public void Parse_CharacterClass_Digit()
        {
            RoundTripHelper.AssertRoundTrips(@"\d");
        }

        [TestMethod]
        public void Parse_CharacterClass_NonDigit()
        {
            RoundTripHelper.AssertRoundTrips(@"\D");
        }

        [TestMethod]
        public void Parse_CharacterClass_Wildcard_ZeroOrMore()
        {
            RoundTripHelper.AssertRoundTrips(".*");
        }

        #endregion

        #region CharacterEscapes

        [TestMethod]
        public void Parse_CharacterEscape_Bell()
        {
            RoundTripHelper.AssertRoundTrips(@"\a");
        }

        [TestMethod]
        public void Parse_CharacterEscape_Tab()
        {
            RoundTripHelper.AssertRoundTrips(@"\t");
        }

        [TestMethod]
        public void Parse_CharacterEscape_CarriageReturn()
        {
            RoundTripHelper.AssertRoundTrips(@"\r");
        }

        [TestMethod]
        public void Parse_CharacterEscape_VerticalTab()
        {
            RoundTripHelper.AssertRoundTrips(@"\v");
        }

        [TestMethod]
        public void Parse_CharacterEscape_FormFeed()
        {
            RoundTripHelper.AssertRoundTrips(@"\f");
        }

        [TestMethod]
        public void Parse_CharacterEscape_Newline()
        {
            RoundTripHelper.AssertRoundTrips(@"\n");
        }

        [TestMethod]
        public void Parse_CharacterEscape_Escape()
        {
            RoundTripHelper.AssertRoundTrips(@"\e");
        }

        [TestMethod]
        public void Parse_CharacterEscape_Period()
        {
            RoundTripHelper.AssertRoundTrips(@"\.");
        }

        [TestMethod]
        public void Parse_CharacterEscape_Dollar()
        {
            RoundTripHelper.AssertRoundTrips(@"\$");
        }

        [TestMethod]
        public void Parse_CharacterEscape_Carot()
        {
            RoundTripHelper.AssertRoundTrips(@"\^");
        }

        [TestMethod]
        public void Parse_CharacterEscape_LeftCurlyBrace()
        {
            RoundTripHelper.AssertRoundTrips(@"\{");
        }

        [TestMethod]
        public void Parse_CharacterEscape_LeftSquareBracket()
        {
            RoundTripHelper.AssertRoundTrips(@"\[");
        }

        [TestMethod]
        public void Parse_CharacterEscape_LeftParenthesis()
        {
            RoundTripHelper.AssertRoundTrips(@"\{");
        }

        [TestMethod]
        public void Parse_CharacterEscape_Pipe()
        {
            RoundTripHelper.AssertRoundTrips(@"\|");
        }

        [TestMethod]
        public void Parse_CharacterEscape_RightParenthesis()
        {
            RoundTripHelper.AssertRoundTrips(@"\)");
        }

        [TestMethod]
        public void Parse_CharacterEscape_Asterisk()
        {
            RoundTripHelper.AssertRoundTrips(@"\*");
        }

        [TestMethod]
        public void Parse_CharacterEscape_Plus()
        {
            RoundTripHelper.AssertRoundTrips(@"\+");
        }

        [TestMethod]
        public void Parse_CharacterEscape_QuestionMark()
        {
            RoundTripHelper.AssertRoundTrips(@"\?");
        }

        [TestMethod]
        public void Parse_CharacterEscape_Backslash()
        {
            RoundTripHelper.AssertRoundTrips(@"\\");
        }

        [TestMethod]
        public void Parse_CharacterEscape_Quantified()
        {
            RoundTripHelper.AssertRoundTrips(@"\t*");
        }

        #endregion

        #region Unicode Categories

        [TestMethod]
        public void Parse_UnicodeCategory_Positive()
        {
            RoundTripHelper.AssertRoundTrips(@"\p{L}");
        }

        [TestMethod]
        public void Parse_UnicodeCategory_Negative()
        {
            RoundTripHelper.AssertRoundTrips(@"\p{L}");
        }

        #endregion

        #region Unicode Named Block

        [TestMethod]
        public void Parse_UnicodeNamedBlock_Positive()
        {
            RoundTripHelper.AssertRoundTrips(@"\p{IsBasicLatin}");
        }

        [TestMethod]
        public void Parse_UnicodeNamedBlock_Negative()
        {
            RoundTripHelper.AssertRoundTrips(@"\P{IsBasicLatin}");
        }

        #endregion

        #region CharacterGroup

        [TestMethod]
        public void Parse_CharacterGroup_SingleLiteral()
        {
            RoundTripHelper.AssertRoundTrips("[a]");
        }

        [TestMethod]
        public void Parse_CharacterGroup_MultipleLiterals()
        {
            RoundTripHelper.AssertRoundTrips("[ab]");
        }

        [TestMethod]
        public void Parse_CharacterGroup_Literals_Negated()
        {
            RoundTripHelper.AssertRoundTrips("[^ab]");
        }

        [TestMethod]
        public void Parse_CharacterGroup_Range()
        {
            RoundTripHelper.AssertRoundTrips("[a-z]");
        }

        [TestMethod]
        public void Parse_CharacterGroup_Range_Negated()
        {
            RoundTripHelper.AssertRoundTrips("[^a-z]");
        }

        [TestMethod]
        public void Parse_CharacterGroup_Range_WithExclusions()
        {
            RoundTripHelper.AssertRoundTrips("[^a-z-[bj-sx]]");
        }

        [TestMethod]
        public void Parse_CharacterGroup_Range_WithNestedExclusions()
        {
            RoundTripHelper.AssertRoundTrips("[^a-z-[bj-sx-[m-n]]]");
        }

        [TestMethod]
        public void Parse_CharacterGroup_LeadingDash()
        {
            RoundTripHelper.AssertRoundTrips("[-z]", @"[\-z]");
        }

        [TestMethod]
        public void Parse_CharacterGroup_LeadingDash_Negated()
        {
            RoundTripHelper.AssertRoundTrips("[^-z]", @"[^\-z]");
        }

        [TestMethod]
        public void Parse_CharacterGroup_TrailingDash()
        {
            RoundTripHelper.AssertRoundTrips("[a-]", @"[a\-]");
        }

        [TestMethod]
        public void Parse_CharacterGroup_TrailingDash_Negated()
        {
            RoundTripHelper.AssertRoundTrips("[^a-]", @"[^a\-]");
        }

        [TestMethod]
        public void Parse_CharacterGroup_Quantified()
        {
            RoundTripHelper.AssertRoundTrips("[^0-9,a-zA-Z.-[bj-sx]]*");
        }

        #endregion

        #region Group

        [TestMethod]
        public void Parse_CaptureGroup_Empty()
        {
            RoundTripHelper.AssertRoundTrips("()");
        }

        [TestMethod]
        public void Parse_CaptureGroup_SingleSubExpression()
        {
            RoundTripHelper.AssertRoundTrips("(a)");
        }

        [TestMethod]
        public void Parse_CaptureGroup_MultipleSubExpressions()
        {
            RoundTripHelper.AssertRoundTrips("(abc+)");
        }

        [TestMethod]
        public void Parse_CaptureGroup_NestedGroups()
        {
            RoundTripHelper.AssertRoundTrips("((((a))))");
        }

        [TestMethod]
        public void Parse_CaptureGroup_Named_WithQuotes()
        {
            RoundTripHelper.AssertRoundTrips("(?'a'a)");
        }

        [TestMethod]
        public void Parse_CaptureGroup_Named_WithBrackets()
        {
            RoundTripHelper.AssertRoundTrips("(?<a>a)");
        }

        [TestMethod]
        public void Parse_NonCaptureGroup()
        {
            RoundTripHelper.AssertRoundTrips("(?:a)");
        }

        [TestMethod]
        public void Parse_PositiveLookupahead()
        {
            RoundTripHelper.AssertRoundTrips("(?=a)a");
        }

        [TestMethod]
        public void Parse_NegativeLookupahead()
        {
            RoundTripHelper.AssertRoundTrips("(?!a)a");
        }

        [TestMethod]
        public void Parse_PositiveLookupbehind()
        {
            RoundTripHelper.AssertRoundTrips("(?<=a)bc");
        }

        [TestMethod]
        public void Parse_NegativeLookupbehind()
        {
            RoundTripHelper.AssertRoundTrips("(?<!a)bc");
        }

        [TestMethod]
        public void Parse_OptionsGroup_EnableOptions()
        {
            RoundTripHelper.AssertRoundTrips("(?imnx:abc)");
        }

        [TestMethod]
        public void Parse_OptionsGroup_DisableOptions()
        {
            RoundTripHelper.AssertRoundTrips("(?-imnx:abc)");
        }

        [TestMethod]
        public void Parse_OptionsGroup_EnableDisableOptions()
        {
            RoundTripHelper.AssertRoundTrips("(?imnx-imnx:abc)");
        }

        [TestMethod]
        public void Parse_OptionsGroup_EnableDisableSingleline()
        {
            RoundTripHelper.AssertRoundTrips("(?s-s:abc)");
        }

        [TestMethod]
        public void Parse_BalanceGroup()
        {
            RoundTripHelper.AssertRoundTrips(@"((?'Open'\()[^)]*)+((?'Close-Open')[^()]*)+");
        }

        [TestMethod]
        public void Parse_BalanceGroup_MissingCurrent()
        {
            RoundTripHelper.AssertRoundTrips(@"((?'Open'\()[^)]*)+((?'-Open')[^()]*)+");
        }

        #endregion

        #region Backreferences

        // TODO - Need to implement grouping first

        #endregion

        #region Literal

        [TestMethod]
        public void Parse_Literal()
        {
            RoundTripHelper.AssertRoundTrips("a");
        }

        [TestMethod]
        public void Parse_Literal_MultipleCharacters()
        {
            RoundTripHelper.AssertRoundTrips("abc");
        }

        #endregion

        #region Quantifiers

        [TestMethod]
        public void Parse_Literal_ZeroOrMore()
        {
            RoundTripHelper.AssertRoundTrips("a*");
        }

        [TestMethod]
        public void Parse_Literal_OneOrMore()
        {
            RoundTripHelper.AssertRoundTrips("a+");
        }

        [TestMethod]
        public void Parse_Literal_ZeroOrOne()
        {
            RoundTripHelper.AssertRoundTrips("a?");
        }

        [TestMethod]
        public void Parse_Literal_Exactly()
        {
            RoundTripHelper.AssertRoundTrips("a{5}");
        }

        [TestMethod]
        public void Parse_Literal_AtLeast()
        {
            RoundTripHelper.AssertRoundTrips("a{5,}");
        }

        [TestMethod]
        public void Parse_Literal_Between()
        {
            RoundTripHelper.AssertRoundTrips("a{5,10}");
        }

        [TestMethod]
        public void Parse_Literal_Between_WithSpaces()
        {
            RoundTripHelper.AssertRoundTrips("a{   5   ,   10  }", "a{5,10}");
        }

        #endregion

        #region Alternations

        [TestMethod]
        public void Parse_Alternation_TwoOptions()
        {
            RoundTripHelper.AssertRoundTrips(@"a|b");
        }

        [TestMethod]
        public void Parse_Alternation_ThreeOptions()
        {
            RoundTripHelper.AssertRoundTrips(@"a|b|c");
        }

        [TestMethod]
        public void Parse_Alternation_SeveralCompoundOptions()
        {
            RoundTripHelper.AssertRoundTrips(@"abc|def|ghi|jkl|mno|pqrs|tuv|wxyz");
        }

        [TestMethod]
        public void Parse_Alternation_InsideGroup()
        {
            RoundTripHelper.AssertRoundTrips(@"(a|b)");
        }

        [TestMethod]
        public void Parse_Alternation_NestedGroups()
        {
            RoundTripHelper.AssertRoundTrips(@"a(b|(c|d|e)|f)g");
        }

        #endregion
    }
}
