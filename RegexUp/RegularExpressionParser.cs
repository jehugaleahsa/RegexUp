using System;
using System.Collections.Generic;

namespace RegexUp
{
    internal class RegularExpressionParser
    {
        public IExpression Parse(string regex)
        {
            var context = new ParserContext(regex);
            var parsed = context.Parse();
            // TODO - Normalize the tree structure
            return parsed;
        }

        private class ParserContext
        {
            private readonly string regex;
            private int index;

            public ParserContext(string regex)
            {
                this.regex = regex;
            }

            public IExpression Parse()
            {
                if (index == regex.Length)
                {
                    return null;
                }
                char nextChar = regex[index];
                switch (nextChar)
                {
                    case '^': return ParseAnchor(Anchors.Carot);
                    case '$': return ParseAnchor(Anchors.Dollar);
                    case '.': return ParseCharacterClass(CharacterClasses.Wildcard);
                    case '\\': return ParseEscapeSequence();
                    case '[': return ParseCharacterGroup();
                    case '(': return ParseGroup();
                    case ')': return null;
                    default: return ParseLiteral(Literal.For(nextChar));
                }
            }

            private IExpression ParseAnchor(IExpression current)
            {
                ++index;
                current = Quantify(current);
                var next = Parse();
                if (next == null)
                {
                    return current;
                }
                return Expression.Of(current, next);
            }

            private IExpression ParseLiteral(IExpression current)
            {
                ++index;
                current = Quantify(current);
                var next = Parse();
                if (next == null)
                {
                    return current;
                }
                return Expression.Of(current, next);
            }

            private IExpression ParseCharacterClass(IExpression current)
            {
                ++index;
                current = Quantify(current);
                var next = Parse();
                if (next == null)
                {
                    return current;
                }
                return Expression.Of(current, next);
            }

            private IExpression ParseEscapeSequence()
            {
                var current = ParseEscapeSequenceInternal();
                ++index;
                current = Quantify(current);
                var next = Parse();
                if (next == null)
                {
                    return current;
                }
                return Expression.Of(current, next);
            }

            private IExpression ParseEscapeSequenceInternal()
            {
                ++index;
                char nextChar = regex[index];
                switch (nextChar)
                {
                    case 'A': return Anchors.A;
                    case 'Z': return Anchors.Z;
                    case 'z': return Anchors.z;
                    case 'G': return Anchors.G;
                    case 'b': return Anchors.b;
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
                    default: return new CharacterEscape($@"\{nextChar}");
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
                int endIndex = GetUnicodeCategoryEndIndex();
                string category = regex.Substring(index, endIndex - index);
                index = endIndex;
                return new UnicodeCategory($@"\{(isPositive ? 'p' : 'P')}{{{category}}}");
            }

            private int GetUnicodeCategoryEndIndex()
            {
                int endIndex = index;
                while (regex[endIndex] != '}')
                {
                    ++endIndex;
                }
                return endIndex;
            }

            private IExpression ParseCharacterGroup()
            {
                IExpression current = ParseCharacterGroupInternal();
                ++index;
                current = Quantify(current);
                var next = Parse();
                if (next == null)
                {
                    return current;
                }
                return Expression.Of(current, next);
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
                            members.Add((ICharacterGroupMember)ParseEscapeSequenceInternal());
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
                char firstChar = startLiteral.Value[0];
                ++index; // swallow '-'
                char lastChar = regex[index];
                return Range.For(firstChar, lastChar);
            }

            private IExpression ParseGroup()
            {
                IExpression current = ParseGroupInternal();
                ++index; // swallow ')'
                current = Quantify(current);
                var next = Parse();
                if (next == null)
                {
                    return current;
                }
                return Expression.Of(current, next);
            }

            private IGroup ParseGroupInternal()
            {
                ++index; // swallow '('
                char nextChar = regex[index];
                switch (nextChar)
                {
                    case '?': return ParseSpecialGroup();
                    default: return ParseCaptureGroupOrBalanceGroup(null, false);
                }
            }

            private IGroup ParseSpecialGroup()
            {
                ++index;
                char nextChar = regex[index];
                switch (nextChar)
                {
                    case '\'': return ParseCaptureGroupOrBalanceGroup(ParseCaptureGroupName('\'', index + 1), true);
                    case '!': return ParseNegativeLookupaheadGroup();
                    case ':': return ParseNonCaptureGroup();
                    case '=': return ParsePositiveLookupaheadGroup();
                    case '<': return ParseNamedCaptureGroupBalanceGroupOrLookupbehindGroup();
                    case '>': return ParseNonbacktrackingAssertion();
                    case '(': throw new NotImplementedException();
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

            private INonbacktrackingGroup ParseNonbacktrackingAssertion()
            {
                ++index; // swallow the '>'
                var members = Parse();
                return members == null ? Group.Nonbacktracking.Of() : Group.Nonbacktracking.Of(members);
            }

            private IGroup ParseCaptureGroupOrBalanceGroup(string name, bool useQuotes)
            {
                var members = Parse();
                var names = name?.Split(new[] { '-' }, 2);
                if (name == null || names.Length == 1)
                {
                    var options = new CaptureGroupOptions() { Name = name, UseQuotes = useQuotes };
                    return members == null ? Group.Capture.Of(options) : Group.Capture.Of(options, members);
                }
                else
                {
                    var (current, previous) = (names[0], names[1]);
                    var options = new BalanceGroupOptions() { UseQuotes = useQuotes };
                    return members == null ? Group.Balanced.Of(current, previous, options) : Group.Balanced.Of(current, previous, options, members);
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
                var members = Parse();
                return members == null ? Group.NonCapture.Of() : Group.NonCapture.Of(members);
            }

            private IGroup ParseNamedCaptureGroupBalanceGroupOrLookupbehindGroup()
            {
                ++index; // swallow '<'
                char nextChar = regex[index];
                switch (nextChar)
                {
                    case '!': return ParseNegativeLookupbehindGroup();
                    case '=': return ParsePositiveLookupbehindGroup();
                    default: return ParseCaptureGroupOrBalanceGroup(ParseCaptureGroupName('>', index), false);
                }
            }

            private INegativeLookbehindAssertionGroup ParseNegativeLookupbehindGroup()
            {
                ++index; // swallow the '!'
                var members = Parse();
                return members == null ? Group.LookbehindAssertions.Negative.Of() : Group.LookbehindAssertions.Negative.Of(members);
            }

            private IPositiveLookbehindAssertionGroup ParsePositiveLookupbehindGroup()
            {
                ++index; // swallow the '='
                var members = Parse();
                return members == null ? Group.LookbehindAssertions.Positive.Of() : Group.LookbehindAssertions.Positive.Of(members);
            }

            private INegativeLookaheadAssertionGroup ParseNegativeLookupaheadGroup()
            {
                ++index; // swallow the '!'
                var members = Parse();
                return members == null ? Group.LookaheadAssertions.Negative.Of() : Group.LookaheadAssertions.Negative.Of(members);
            }

            private IPositiveLookaheadAssertionGroup ParsePositiveLookupaheadGroup()
            {
                ++index; // swallow the '='
                var members = Parse();
                return members == null ? Group.LookaheadAssertions.Positive.Of() : Group.LookaheadAssertions.Positive.Of(members);
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
                var members = Parse();
                return members == null ? Group.Options.Of(enabled, disabled) : Group.Options.Of(enabled, disabled, members);
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
        }
    }
}
