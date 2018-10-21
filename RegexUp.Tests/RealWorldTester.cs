using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RegexUp.Tests
{
    [TestClass]
    public class RealWorldTester
    {
        [TestMethod]
        public void UrlScheme()
        {
            RegularExpression expression = RegularExpression.Of(
                Anchors.Carot,

                // scheme - (?:([A-Za-z]+):)?
                Quantifiers.ZeroOrOne(
                    Groups.NonCapture(
                        Groups.Capture(
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
                Groups.Capture(
                    Quantifiers.Between(Literal.For("/"), 0, 3)
                ),

                // host - ([0-9.\-A-Za-z]+)
                Groups.Capture(
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
                    Groups.NonCapture(
                        Literal.For(":"),
                        Groups.Capture(
                            Quantifiers.OneOrMore(CharacterClasses.Digit)
                        )
                    )
                ),

                // path - (/[^?#]*)?
                Quantifiers.ZeroOrOne(
                    Groups.Capture(
                        Literal.For("/"),
                        Quantifiers.ZeroOrMore(
                            CharacterGroup.Of(
                                true,
                                Literal.For("?"),
                                Literal.For("#")
                            )
                        )
                    )
                ),

                // query - (?:\?([^#]*))?
                Quantifiers.ZeroOrOne(
                    Groups.NonCapture(
                        Literal.For("?"),
                        Groups.Capture(
                            Quantifiers.ZeroOrMore(
                                CharacterGroup.Of(true, Literal.For("#"))
                            )
                        )
                    )
                ),

                // hash - (?:#(.*))?
                Quantifiers.ZeroOrOne(
                    Groups.NonCapture(
                        Literal.For("#"),
                        Groups.Capture(
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
    }
}
