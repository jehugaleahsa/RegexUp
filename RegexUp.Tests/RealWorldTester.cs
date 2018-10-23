using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RegexUp.Tests
{
    [TestClass]
    public class RealWorldTester
    {
        [TestMethod]
        public void UrlScheme()
        {
            var expression = RegularExpression.Of(
                Anchors.Carot,

                // scheme - (?:([A-Za-z]+):)?
                Quantifiers.ZeroOrOne(
                    NonCaptureGroup.Of(
                        CaptureGroup.Of(
                            Quantifiers.OneOrMore(
                                CharacterGroup.Of(
                                    Range.For('A', 'Z'),
                                    Range.For('a', 'z')
                                )
                            )
                        ),
                        Literal.For(":")
                    )
                ),

                // slash - (/{0,3})
                CaptureGroup.Of(
                    Quantifiers.Between(Literal.For("/"), 0, 3)
                ),

                // host - ([0-9.\-A-Za-z]+)
                CaptureGroup.Of(
                    Quantifiers.OneOrMore(
                        CharacterGroup.Of(
                            Range.For('0', '9'),
                            Literal.For("."),
                            Literal.For("-"),
                            Range.For('A', 'Z'),
                            Range.For('a', 'z')
                        )
                    )
                ),

                // port - (?::(\d+))?
                Quantifiers.ZeroOrOne(
                    NonCaptureGroup.Of(
                        Literal.For(":"),
                        CaptureGroup.Of(
                            Quantifiers.OneOrMore(CharacterClasses.Digit)
                        )
                    )
                ),

                // path - (/[^?#]*)?
                Quantifiers.ZeroOrOne(
                    CaptureGroup.Of(
                        Literal.For("/"),
                        Quantifiers.ZeroOrMore(
                            CharacterGroup.Of(
                                new CharacterGroupOptions() { IsNegated = true },
                                Literal.For("?"),
                                Literal.For("#")
                            )
                        )
                    )
                ),

                // query - (?:\?([^#]*))?
                Quantifiers.ZeroOrOne(
                    NonCaptureGroup.Of(
                        Literal.For("?"),
                        CaptureGroup.Of(
                            Quantifiers.ZeroOrMore(
                                CharacterGroup.Of(new CharacterGroupOptions() { IsNegated = true }, Literal.For("#"))
                            )
                        )
                    )
                ),

                // hash - (?:#(.*))?
                Quantifiers.ZeroOrOne(
                    NonCaptureGroup.Of(
                        Literal.For("#"),
                        CaptureGroup.Of(
                            Quantifiers.ZeroOrMore(CharacterClasses.Wildcard)
                        )
                    )
                ),

                Anchors.Dollar
            );

            var regex = expression.ToRegex();
            var source = regex.ToString();

            const string urlRegex = @"^(?:([A-Za-z]+):)?(/{0,3})([0-9.\-A-Za-z]+)(?::(\d+))?(/[^?#]*)?(?:\?([^#]*))?(?:\#(.*))?$";
            Assert.AreEqual(urlRegex, source);
        }

        [TestMethod]
        public void UrlScheme_RoundTrips()
        {
            RoundTripHelper.AssertRoundTrips(@"^(?:([A-Za-z]+):)?(/{0,3})([0-9.\-A-Za-z]+)(?::(\d+))?(/[^?#]*)?(?:\?([^#]*))?(?:\#(.*))?$");
        }

        //[TestMethod]
        //public void EmailValidator_RoundTrips()
        //{
        //    RoundTripHelper.AssertRoundTrips(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01 -\x08\x0b\x0c\x0e -\x1f\x21\x23 -\x5b\x5d -\x7f] |\\[\x01 -\x09\x0b\x0c\x0e -\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])");
        //}
    }
}
