using System;
using System.Collections.Generic;
using System.Linq;

namespace RegexUp
{
    internal sealed class RegularExpressionParser
    {
        public void Parse(RegularExpression regularExpression, string regex)
        {
            var context = new ParserContext(regex);
            context.Parse(regularExpression);
        }

        private class ParserContext
        {
            private readonly string regex;
            private int index;

            public ParserContext(string regex)
            {
                this.regex = regex;
            }

            public void Parse(RegularExpression regularExpression)
            {
                var container = Parse();
                regularExpression.Add(container);
            }

            private IExpression Parse()
            {
                var alternatives = new List<IExpression>();
                IContainer container = new CompoundExpression();
                while (index != regex.Length)
                {
                    var item = ParseInternal(container);
                    if (item == null)
                    {
                        break;
                    }
                    // If we get back the container we passed in, then we've encountered a child alternation.
                    if (item == container)
                    {
                        alternatives.Add(container);
                        container = new CompoundExpression();
                    }
                    else
                    {
                        container.Add(item);
                    }
                }
                if (alternatives.Count == 0)
                {
                    return container;
                }
                else
                {
                    alternatives.Add(container);
                    return Alternation.From(alternatives);
                }
            }

            private IExpression ParseInternal(IContainer container)
            {
                char nextChar = regex[index];
                switch (nextChar)
                {
                    case '^': return ParseAnchor(container, Anchors.Carot);
                    case '$': return ParseAnchor(container, Anchors.Dollar);
                    case '.': return ParseCharacterClass(container, CharacterClasses.Wildcard);
                    case '\\': return ParseEscapeSequence(container);
                    case '[': return ParseCharacterGroup(container);
                    case '(': return ParseGroup(container);
                    case '|': return ParseAlternation(container);
                    case ')':
                        return null;
                    default: return ParseLiteral(container, Literal.For(regex[index]));
                }
            }

            private IExpression ParseAnchor(IContainer container, IExpression current)
            {
                ++index;
                current = Quantify(current);
                return current;
            }

            private IExpression ParseLiteral(IContainer container, IExpression current)
            {
                ++index;
                current = Quantify(current);
                return current;
            }

            private static bool IsLiteral(char nextChar)
            {
                switch (nextChar)
                {
                    case '^':
                    case '$':
                    case '.':
                    case '\\':
                    case '[':
                    case '(':
                    case '|':
                    case ')':
                    case '*':
                    case '+':
                    case '?':
                    case '{':
                        return false;
                    default:
                        return true;
                }
            }

            private IExpression ParseCharacterClass(IContainer container, IExpression current)
            {
                ++index;
                current = Quantify(current);
                return current;
            }

            private IExpression ParseEscapeSequence(IContainer container)
            {
                var current = ParseEscapeSequenceInternal(ExpressionContext.Group);
                ++index;
                current = Quantify(current);
                return current;
            }

            private IExpression ParseEscapeSequenceInternal(ExpressionContext context)
            {
                ++index;
                char nextChar = regex[index];
                switch (nextChar)
                {
                    case 'A': return Anchors.A;
                    case 'Z': return Anchors.Z;
                    case 'z': return Anchors.z;
                    case 'G': return Anchors.G;
                    case 'b': return (context == ExpressionContext.CharacterGroup) ? (IExpression)CharacterEscapes.Backspace : Anchors.b;
                    case 'B': return Anchors.B;
                    case 'k': return ParseNamedBackreference();
                    case 'w': return CharacterClasses.Word;
                    case 'W': return CharacterClasses.NonWord;
                    case 's': return CharacterClasses.Whitespace;
                    case 'S': return CharacterClasses.NonWhitespace;
                    case 'd': return CharacterClasses.Digit;
                    case 'D': return CharacterClasses.NonDigit;
                    case 'p': return ParseUnicodeCategory(isPositive: true);
                    case 'P': return ParseUnicodeCategory(isPositive: false);
                    case 'a': return CharacterEscapes.Bell;
                    case 't': return CharacterEscapes.Tab;
                    case 'r': return CharacterEscapes.CarriageReturn;
                    case 'v': return CharacterEscapes.VerticalTab;
                    case 'f': return CharacterEscapes.FormFeed;
                    case 'n': return CharacterEscapes.NewLine;
                    case 'e': return CharacterEscapes.Escape;
                    case '.': return CharacterEscapes.Period;
                    case '$': return CharacterEscapes.Dollar;
                    case '^': return CharacterEscapes.Carot;
                    case '{': return CharacterEscapes.LeftCurlyBrace;
                    case '[': return CharacterEscapes.LeftSquareBracket;
                    case '(': return CharacterEscapes.LeftParenthesis;
                    case '|': return CharacterEscapes.Pipe;
                    case ')': return CharacterEscapes.RightParenthesis;
                    case '*': return CharacterEscapes.Asterisk;
                    case '+': return CharacterEscapes.Plus;
                    case '?': return CharacterEscapes.QuestionMark;
                    case '\\': return CharacterEscapes.Backslash;
                    case 'x': return ParseHexidecimalEscape();
                    case 'c': return ParseControlCharacterEscape();
                    case 'u': return ParseUnicodeCharacterEscape();
                    case '0': return ParseOctalEscape();
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        return ParseNumberedBackreference();
                    default: return CharacterEscapes.For(nextChar);
                }
            }

            private IExpression ParseHexidecimalEscape()
            {
                ++index; // swallow 'x'
                int endIndex = GetHexidecimalNumberEndIndex(Math.Min(index + 2, regex.Length));
                string numberString = regex.Substring(index, endIndex - index);
                index = endIndex - 1;
                return CharacterEscapes.Hexidecimal(numberString);
            }

            private IExpression ParseControlCharacterEscape()
            {
                ++index; // swallow 'c'
                char controlCharacter = regex[index];
                return CharacterEscapes.ControlCharacter(controlCharacter);
            }

            private IExpression ParseUnicodeCharacterEscape()
            {
                ++index; // swallow 'u'
                int endIndex = GetHexidecimalNumberEndIndex(Math.Min(index + 4, regex.Length));
                string numberString = regex.Substring(index, endIndex - index);
                index = endIndex - 1;
                return CharacterEscapes.Unicode(numberString);
            }

            private IExpression ParseOctalEscape()
            {
                int endIndex = GetNumberEndIndex(Math.Min(index + 3, regex.Length));
                string numberString = regex.Substring(index, endIndex - index);
                index = endIndex - 1;
                return CharacterEscapes.Octal(numberString);
            }

            private IExpression ParseNamedBackreference()
            {
                ++index; // swallow 'k'
                bool useQuotes = regex[index] == '\'';
                char closingChar = useQuotes ? '\'' : '>';
                ++index; // swallow opening char
                int endIndex = regex.IndexOf(closingChar, index);
                string name = regex.Substring(index, endIndex - index);
                index = endIndex;
                return Backreference.For(name, useQuotes);
            }

            private IExpression ParseNumberedBackreference()
            {
                int endIndex = GetNumberEndIndex(regex.Length);
                string numberString = regex.Substring(index, endIndex - index);
                index = endIndex - 1;
                int number = Int32.Parse(numberString);
                return Backreference.For(number);
            }

            private IExpression ParseUnicodeCategory(bool isPositive)
            {
                index += 2; // swallow {
                int endIndex = regex.IndexOf('}', index);
                string category = regex.Substring(index, endIndex - index);
                index = endIndex;
                return new UnicodeCategory($@"\{(isPositive ? 'p' : 'P')}{{{category}}}");
            }

            private IExpression ParseCharacterGroup(IContainer container)
            {
                IExpression current = ParseCharacterGroupInternal();
                ++index;
                current = Quantify(current);
                return current;
            }

            private ICharacterGroup ParseCharacterGroupInternal()
            {
                ++index;
                var members = new List<ICharacterGroupMember>();
                bool isNegated = regex[index] == '^';
                if (isNegated)
                {
                    ++index;
                }
                bool isDone = false;
                ICharacterGroup exclusions = null;
                while (!isDone)
                {
                    var nextChar = regex[index];
                    switch (nextChar)
                    {
                        case ']':
                            isDone = true;
                            break;
                        case '-':
                            if (regex[index + 1] == '[')
                            {
                                ++index; // swallow the '-'
                                exclusions = ParseCharacterGroupInternal();
                            }
                            else if (members.Count == 0 || regex[index + 1] == ']')
                            {
                                // If '-' is the first or last character, add it literally
                                members.Add(Literal.For('-'));
                            }
                            else
                            {
                                var lastMember = members[members.Count - 1];
                                var range = ParseRange(lastMember);
                                members.RemoveAt(members.Count - 1);
                                members.Add(range);
                            }
                            ++index;
                            break;
                        default:                            
                            var member = ParseCharacterGroupMember();
                            members.Add(member);
                            ++index;
                            break;
                    }
                }
                var options = new CharacterGroupOptions() { IsNegated = isNegated, Exclusions = exclusions };
                return CharacterGroup.From(options, members);
            }

            private ICharacterGroupMember ParseRange(ICharacterGroupMember startLiteral)
            {
                ++index; // swallow '-'
                var lastLiteral = ParseCharacterGroupMember();
                return Range.For(startLiteral, lastLiteral);
            }

            private ICharacterGroupMember ParseCharacterGroupMember()
            {
                char nextChar = regex[index];
                switch (nextChar)
                {
                    case '\\': return (ICharacterGroupMember)ParseEscapeSequenceInternal(ExpressionContext.CharacterGroup);
                    default: return Literal.For(nextChar);
                }
            }

            private IExpression ParseGroup(IContainer container)
            {
                IExpression current = ParseGroupInternal();
                ++index; // swallow ')'
                current = Quantify(current);
                return current;
            }

            private IExpression ParseGroupInternal()
            {
                ++index; // swallow '('
                char nextChar = regex[index];
                switch (nextChar)
                {
                    case '?': return ParseSpecialGroup();
                    default: return ParseCaptureGroupOrBalanceGroup(null, false);
                }
            }

            private IExpression ParseSpecialGroup()
            {
                ++index;
                char nextChar = regex[index];
                switch (nextChar)
                {
                    case '\'': return ParseCaptureGroupOrBalanceGroup(ParseCaptureGroupName('\'', index + 1), true);
                    case '!': return ParseNegativeLookaheadGroup();
                    case ':': return ParseNonCaptureGroup();
                    case '=': return ParsePositiveLookaheadGroup();
                    case '<': return ParseNamedCaptureGroupBalanceGroupOrLookupbehindGroup();
                    case '>': return ParseNonbacktrackingAssertion();
                    case '(': return ParseConditionalAlternation();
                    case '#': return ParseInlineComment();
                    case 'i':
                    case 'm':
                    case 'n':
                    case 's':
                    case 'x':
                    case '-':
                        return ParseOptions();
                    default: throw new InvalidOperationException();
                }
            }

            private IGroup ParseCaptureGroupOrBalanceGroup(string name, bool useQuotes)
            {
                var names = name?.Split(new[] { '-' }, 2);
                if (name == null || names.Length == 1)
                {
                    var container = Parse();
                    var group = new CaptureGroup() { Name = name, UseQuotes = useQuotes };
                    group.Add(container);
                    return group;
                }
                else
                {
                    var container = Parse();
                    var (current, previous) = (names[0], names[1]);
                    var group = new BalancedGroup() { Current = current, Previous = previous, UseQuotes = useQuotes };
                    group.Add(container);
                    return group;
                }
            }

            private string ParseCaptureGroupName(char closingChar, int startIndex)
            {
                int endIndex = regex.IndexOf(closingChar, startIndex);
                var name = regex.Substring(startIndex, endIndex - startIndex);
                index = endIndex + 1; // swallow the closing char
                return name;
            }

            private INonCaptureGroup ParseNonCaptureGroup()
            {
                ++index; // swallow ':'
                var container = Parse();
                var group = new NonCaptureGroup();
                group.Add(container);
                return group;
            }

            private IGroup ParseNamedCaptureGroupBalanceGroupOrLookupbehindGroup()
            {
                ++index; // swallow '<'
                char nextChar = regex[index];
                switch (nextChar)
                {
                    case '!': return ParseNegativeLookbehindGroup();
                    case '=': return ParsePositiveLookbehindGroup();
                    default: return ParseCaptureGroupOrBalanceGroup(ParseCaptureGroupName('>', index), false);
                }
            }

            private INegativeLookbehindAssertion ParseNegativeLookbehindGroup()
            {
                ++index; // swallow the '!'
                var container = Parse();
                var group = new NegativeLookbehindAssertion();
                group.Add(container);
                return group;
            }

            private IPositiveLookbehindAssertion ParsePositiveLookbehindGroup()
            {
                ++index; // swallow the '='
                var container = Parse();
                var group = new PositiveLookbehindAssertion();
                group.Add(container);
                return group;
            }

            private INegativeLookaheadAssertion ParseNegativeLookaheadGroup()
            {
                ++index; // swallow the '!'
                var container = Parse();
                var group = new NegativeLookaheadAssertion();
                group.Add(container);
                return group;
            }

            private IPositiveLookaheadAssertion ParsePositiveLookaheadGroup()
            {
                ++index; // swallow the '='
                var container = Parse();
                var group = new PositiveLookaheadAssertion();
                group.Add(container);
                return group;
            }

            private IExpression ParseOptions()
            {
                var enabled = GroupRegexOptions.None;
                while (regex[index] != '-' && regex[index] != ':' && regex[index] != ')')
                {
                    enabled |= GetOption();
                    ++index;
                }
                if (regex[index] == '-')
                {
                    ++index;
                }
                var disabled = GroupRegexOptions.None;
                while (regex[index] != ':' && regex[index] != ')')
                {
                    disabled |= GetOption();
                    ++index;
                }
                if (regex[index] == ':')
                {
                    ++index; // swallow ':'
                    var container = Parse();
                    var group = new OptionsGroup() { EnabledOptions = enabled, DisabledOptions = disabled };
                    group.Add(container);
                    return group;
                }
                else
                {
                    return InlineOptions.For(enabled, disabled);
                }
            }

            private GroupRegexOptions GetOption()
            {
                char nextChar = regex[index];
                switch (nextChar)
                {
                    case 'i': return GroupRegexOptions.IgnoreCase;
                    case 'm': return GroupRegexOptions.Multiline;
                    case 'n': return GroupRegexOptions.ExplicitCapture;
                    case 's': return GroupRegexOptions.Singleline;
                    case 'x': return GroupRegexOptions.IgnorePatternWhitespace;
                    default: throw new InvalidOperationException();
                }
            }

            private INonbacktrackingAssertion ParseNonbacktrackingAssertion()
            {
                ++index; // swallow the '>'
                var container = Parse();
                var group = new NonbacktrackingAssertion();
                group.Add(container);
                return group;
            }

            private IExpression ParseConditionalAlternation()
            {
                ++index; // swallow '('
                var expressionOrName = Parse();
                ++index; // swallow ')'

                var container = Parse();
                
                IExpression yes = null;
                IExpression no = null;
                if (container is IAlternation alternation)
                {
                    yes = alternation.Alternatives.First();
                    no = alternation.Alternatives.Skip(1).FirstOrDefault();
                }
                else
                {
                    yes = container;
                }
                var group = new ConditionalAlternation()
                {
                    Expression = expressionOrName,
                    YesOption = yes,
                    NoOption = no
                };
                return group;
            }

            private IExpression ParseInlineComment()
            {
                ++index; // swallow the '#'
                int endIndex = regex.IndexOf(')', index);
                var comment = regex.Substring(index, endIndex - index);
                index = endIndex;
                return InlineComment.For(comment);
            }

            private IExpression Quantify(IExpression expression)
            {
                var quantifierType = GetQuantifierType();
                if (quantifierType == QuantifierType.None)
                {
                    return expression;
                }
                return GetQuantifier(quantifierType, expression);
            }

            private QuantifierType GetQuantifierType()
            {
                if (index == regex.Length)
                {
                    return QuantifierType.None;
                }
                var nextChar = regex[index];
                switch (nextChar)
                {
                    case '*': return QuantifierType.ZeroOrMore;
                    case '+': return QuantifierType.OneOrMore;
                    case '?': return QuantifierType.ZeroOrOne;
                    case '{': return QuantifierType.Explicit;
                    default: return QuantifierType.None;
                }
            }

            private IQuantifiedExpression GetQuantifier(QuantifierType type, IExpression expression)
            {
                ++index;
                int min = 0;
                int? max = null;
                if (type == QuantifierType.Explicit)
                {
                    (min, max) = GetQuantifierRange();
                }
                bool isGreedy = (index == regex.Length || regex[index] != '?') ? true : false;
                if (!isGreedy)
                {
                    ++index;
                }
                switch (type)
                {
                    case QuantifierType.ZeroOrMore: return Quantifiers.ZeroOrMore(expression, isGreedy);
                    case QuantifierType.OneOrMore: return Quantifiers.OneOrMore(expression, isGreedy);
                    case QuantifierType.ZeroOrOne: return Quantifiers.ZeroOrOne(expression, isGreedy);
                    case QuantifierType.Explicit:
                        {
                            if (max == null)
                            {
                                return Quantifiers.AtLeast(expression, min, isGreedy);
                            }
                            else if (min == max.Value)
                            {
                                return Quantifiers.Exactly(expression, min, isGreedy);
                            }
                            else
                            {
                                return Quantifiers.Between(expression, min, max.Value, isGreedy);
                            }
                        }
                    default: throw new InvalidOperationException();
                }
            }

            private ValueTuple<int, int?> GetQuantifierRange()
            {
                int min = GetQuantifierNumber();
                if (regex[index] == '}')
                {
                    ++index; // swallow the '}'
                    return ValueTuple.Create(min, min);
                }
                ++index; // swallow the ','
                while (Char.IsWhiteSpace(regex[index]))
                {
                    ++index; // swallow whitespace
                }
                if (regex[index] == '}')
                {
                    ++index; // swallow the '}'
                    return ValueTuple.Create(min, (int?)null);
                }
                int max = GetQuantifierNumber();
                ++index; // swallow the '}'
                return ValueTuple.Create(min, max);
            }

            private IExpression ParseAlternation(IContainer container)
            {
                ++index; // swallow '|'
                return container;
            }

            private int GetQuantifierNumber()
            {
                int endIndex = GetNumberEndIndexWithWhitespace();
                string numberString = regex.Substring(index, endIndex - index).Trim();
                int number = Int32.Parse(numberString);
                index = endIndex;
                return number;
            }

            private int GetNumberEndIndexWithWhitespace()
            {
                int endIndex = index;
                while (endIndex != regex.Length && Char.IsWhiteSpace(regex[endIndex]) || Char.IsDigit(regex[endIndex]))
                {
                    ++endIndex;
                }
                return endIndex;
            }

            private int GetNumberEndIndex(int maxIndex)
            {
                int endIndex = index;
                while (endIndex != maxIndex && Char.IsDigit(regex[endIndex]))
                {
                    ++endIndex;
                }
                return endIndex;
            }

            private int GetHexidecimalNumberEndIndex(int maxIndex)
            {
                int endIndex = index;
                while (endIndex != maxIndex && IsHexidecimalDigit(regex[endIndex]))
                {
                    ++endIndex;
                }
                return endIndex;
            }

            private static bool IsHexidecimalDigit(char character)
            {
                switch (character)
                {
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                    case 'A':
                    case 'a':
                    case 'B':
                    case 'b':
                    case 'C':
                    case 'c':
                    case 'D':
                    case 'd':
                    case 'E':
                    case 'e':
                    case 'F':
                    case 'f':
                        return true;
                    default:
                        return false;
                }
            }

            private enum QuantifierType
            {
                None,
                ZeroOrMore,
                OneOrMore,
                ZeroOrOne,
                Explicit
            }
        }
    }
}
