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
                IContainer container = new Expression();
                container = Parse(container);
                InheritMembers(regularExpression, container);
            }

            private IContainer Parse(IContainer container)
            {
                if (index == regex.Length)
                {
                    return container;
                }
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
                    case ')': return container;
                    default: return ParseLiteral(container, Literal.For(nextChar));
                }
            }

            private IContainer ParseAnchor(IContainer container, IExpression current)
            {
                ++index;
                current = Quantify(current);
                container.Add(current);
                return Parse(container);
            }

            private IContainer ParseLiteral(IContainer container, IExpression current)
            {
                ++index;
                current = Quantify(current);
                container.Add(current);
                return Parse(container);
            }

            private IContainer ParseCharacterClass(IContainer container, IExpression current)
            {
                ++index;
                current = Quantify(current);
                container.Add(current);
                return Parse(container);
            }

            private IContainer ParseEscapeSequence(IContainer container)
            {
                var current = ParseEscapeSequenceInternal(ExpressionContext.Group);
                ++index;
                current = Quantify(current);
                container.Add(current);
                return Parse(container);
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
                        return ParseNumberedBackreference();
                    default: return CharacterEscapes.For(nextChar);
                }
            }

            private IExpression ParseNamedBackreference()
            {
                ++index;
                bool useQuotes = regex[index] == '\'';
                char closingChar = useQuotes ? '\'' : '>';
                ++index;
                int endIndex = regex.IndexOf(closingChar, index);
                string name = regex.Substring(index, endIndex - index);
                index = endIndex;
                return Backreference.For(name, useQuotes);
            }

            private IExpression ParseNumberedBackreference()
            {
                int endIndex = GetNumberEndIndex();
                string numberString = regex.Substring(index, endIndex - index);
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

            private IContainer ParseCharacterGroup(IContainer container)
            {
                IExpression current = ParseCharacterGroupInternal();
                ++index;
                current = Quantify(current);
                container.Add(current);
                return Parse(container);
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
                    char nextChar = regex[index];
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
                                var range = ParseRange((Literal)lastMember);
                                members.RemoveAt(members.Count - 1);
                                members.Add(range);
                            }
                            ++index;
                            break;
                        case '\\':
                            members.Add((ICharacterGroupMember)ParseEscapeSequenceInternal(ExpressionContext.CharacterGroup));
                            ++index;
                            break;
                        default:
                            members.Add(Literal.For(nextChar));
                            ++index;
                            break;
                    }
                }
                var options = new CharacterGroupOptions() { IsNegated = isNegated, Exclusions = exclusions };
                return CharacterGroup.From(options, members);
            }

            private ICharacterGroupMember ParseRange(Literal startLiteral)
            {
                char firstChar = startLiteral.Value;
                ++index; // swallow '-'
                char lastChar = regex[index];
                return Range.For(firstChar, lastChar);
            }

            private IContainer ParseGroup(IContainer container)
            {
                IExpression current = ParseGroupInternal();
                ++index; // swallow ')'
                current = Quantify(current);
                container.Add(current);
                return Parse(container);
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
                    case 'i':
                    case 'm':
                    case 'n':
                    case 's':
                    case 'x':
                    case '-':
                        return ParseOptionsGroup();
                    default: throw new InvalidOperationException();
                }
            }

            private IGroup ParseCaptureGroupOrBalanceGroup(string name, bool useQuotes)
            {
                var names = name?.Split(new[] { '-' }, 2);
                if (name == null || names.Length == 1)
                {
                    IContainer container = new Expression();
                    container = Parse(container);
                    var group = new CaptureGroup() { Name = name, UseQuotes = useQuotes };
                    InheritMembers(group, container);
                    return group;
                }
                else
                {
                    IContainer container = new Expression();
                    container = Parse(container);
                    var (current, previous) = (names[0], names[1]);
                    var group = new BalancedGroup() { Current = current, Previous = previous, UseQuotes = useQuotes };
                    InheritMembers(group, container);
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
                IContainer container = new Expression();
                container = Parse(container);
                var group = new NonCaptureGroup();
                InheritMembers(group, container);
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

            private INegativeLookbehindAssertionGroup ParseNegativeLookbehindGroup()
            {
                ++index; // swallow the '!'
                IContainer container = new Expression();
                container = Parse(container);
                var group = new NegativeLookbehindAssertionGroup();
                InheritMembers(group, container);
                return group;
            }

            private IPositiveLookbehindAssertionGroup ParsePositiveLookbehindGroup()
            {
                ++index; // swallow the '='
                IContainer container = new Expression();
                container = Parse(container);
                var group = new PositiveLookbehindAssertionGroup();
                InheritMembers(group, container);
                return group;
            }

            private INegativeLookaheadAssertionGroup ParseNegativeLookaheadGroup()
            {
                ++index; // swallow the '!'
                IContainer container = new Expression();
                container = Parse(container);
                var group = new NegativeLookaheadAssertionGroup();
                InheritMembers(group, container);
                return group;
            }

            private IPositiveLookaheadAssertionGroup ParsePositiveLookaheadGroup()
            {
                ++index; // swallow the '='
                IContainer container = new Expression();
                container = Parse(container);
                var group = new PositiveLookaheadAssertionGroup();
                InheritMembers(group, container);
                return group;
            }

            private IOptionsGroup ParseOptionsGroup()
            {
                var enabled = GroupRegexOptions.None;
                while (regex[index] != '-' && regex[index] != ':')
                {
                    enabled |= GetOption();
                    ++index;
                }
                if (regex[index] == '-')
                {
                    ++index;
                }
                var disabled = GroupRegexOptions.None;
                while (regex[index] != ':')
                {
                    disabled |= GetOption();
                    ++index;
                }
                ++index; // swallow ':'
                IContainer container = new Expression();
                container = Parse(container);
                var group = new OptionsGroup() { EnabledOptions = enabled, DisabledOptions = disabled };
                InheritMembers(group, container);
                return group;
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

            private INonbacktrackingGroup ParseNonbacktrackingAssertion()
            {
                ++index; // swallow the '>'
                IContainer container = new Expression();
                container = Parse(container);
                var group = new NonbacktrackingGroup();
                InheritMembers(group, container);
                return group;
            }

            private IExpression ParseConditionalAlternation()
            {
                ++index; // swallow '('
                IContainer expressionOrName = new Expression();
                expressionOrName = Parse(expressionOrName);
                ++index; // swallow ')'

                IContainer container = new Expression();
                container = Parse(container);
                
                IExpression yes = null;
                IExpression no = null;
                if (container is IAlternation alternation)
                {
                    yes = alternation.Alternatives.First();
                    no = alternation.Alternatives.Skip(1).FirstOrDefault();
                }
                else
                {
                    yes = (IExpression)container;
                }
                var group = new ConditionalAlternation()
                {
                    Expression = (IExpression)expressionOrName,
                    YesOption = yes,
                    NoOption = no
                };
                return group;
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
                int minEndIndex = GetNumberEndIndexWithWhitespace();
                string minString = regex.Substring(index, minEndIndex - index).Trim();
                int min = Int32.Parse(minString);
                index = minEndIndex;
                if (regex[index] == '}')
                {
                    ++index; // swallow the }
                    return ValueTuple.Create(min, min);
                }
                ++index; // swallow the ,
                if (regex[index] == '}')
                {
                    ++index; // swallow the }
                    return ValueTuple.Create(min, (int?)null);
                }
                int maxEndIndex = GetNumberEndIndexWithWhitespace();
                string maxString = regex.Substring(index, maxEndIndex - index).Trim();
                int max = Int32.Parse(maxString);
                index = maxEndIndex + 1; // swallow the }
                return ValueTuple.Create(min, max);
            }

            private IContainer ParseAlternation(IContainer container)
            {
                ++index; // swallow '|'
                var alternation = new Alternation();
                alternation.Add((IExpression)container);

                var rightContainer = new Expression();
                var right = Parse(rightContainer);
                if (right is IAlternation rightAlternation)
                {
                    // In a deeply nested alternation, this operation could be expensive.
                    // It might make more sense to use a linked list data structure and simply
                    // append the left-hand side to the front of the list instead.
                    foreach (var alternative in rightAlternation.Alternatives)
                    {
                        alternation.Add(alternative);
                    }
                }
                else
                {
                    alternation.Add((IExpression)right);
                }

                return alternation;
            }

            private int GetNumberEndIndexWithWhitespace()
            {
                int endIndex = index;
                while (Char.IsWhiteSpace(regex[endIndex]) || Char.IsDigit(regex[endIndex]))
                {
                    ++endIndex;
                }
                return endIndex;
            }

            private int GetNumberEndIndex()
            {
                int endIndex = index;
                while (Char.IsDigit(regex[endIndex]))
                {
                    ++endIndex;
                }
                return endIndex;
            }

            private enum QuantifierType
            {
                None,
                ZeroOrMore,
                OneOrMore,
                ZeroOrOne,
                Explicit
            }

            private static void InheritMembers(IContainer parent, IContainer child)
            {
                if (child is Expression expression)
                {
                    foreach (var subExpression in expression.Members)
                    {
                        parent.Add(subExpression);
                    }
                }
                else
                {
                    parent.Add((IExpression)child);
                }
            }
        }
    }
}
