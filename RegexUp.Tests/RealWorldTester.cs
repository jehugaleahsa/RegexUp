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
                    Group.NonCapture.Of(
                        Group.Capture.Of(
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
                Group.Capture.Of(
                    Quantifiers.Between(Literal.For("/"), 0, 3)
                ),

                // host - ([0-9.\-A-Za-z]+)
                Group.Capture.Of(
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
                    Group.NonCapture.Of(
                        Literal.For(":"),
                        Group.Capture.Of(
                            Quantifiers.OneOrMore(CharacterClasses.Digit)
                        )
                    )
                ),

                // path - (/[^?#]*)?
                Quantifiers.ZeroOrOne(
                    Group.Capture.Of(
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
                    Group.NonCapture.Of(
                        Literal.For("?"),
                        Group.Capture.Of(
                            Quantifiers.ZeroOrMore(
                                CharacterGroup.Of(true, Literal.For("#"))
                            )
                        )
                    )
                ),

                // hash - (?:#(.*))?
                Quantifiers.ZeroOrOne(
                    Group.NonCapture.Of(
                        Literal.For("#"),
                        Group.Capture.Of(
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
