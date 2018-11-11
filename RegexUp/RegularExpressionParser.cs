using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace RegexUp
{
    internal sealed class RegularExpressionParser
    {
        public void Parse(RegularExpression regularExpression, string regex, RegexOptions options)
        {
            var context = new ParserContext(regex, options);
            context.Parse(regularExpression);
        }

        private class ParserContext
        {
            private readonly string regex;
            private readonly Dictionary<string, int> groupIndexes = new Dictionary<string, int>();
            private readonly List<string> groupNames = new List<string>();
            private readonly Stack<GroupRegexOptions> options = new Stack<GroupRegexOptions>();
            private int index;

            public ParserContext(string regex, RegexOptions options)
            {
                this.regex = regex;
                this.options.Push(GroupRegexOptionsUtilties.GetGroupOptions(options));
            }

            public void Parse(RegularExpression regularExpression)
            {
                var item = Parse();
                regularExpression.Add(item);
            }

            private IExpression Parse()
            {
                var alternatives = new List<IExpression>();
                var expression = new CompoundExpression();
                while (index != regex.Length)
                {
                    if (regex[index] == '|')
                    {
                        ++index;
                        alternatives.Add(expression.Normalize());
                        expression = new CompoundExpression();
                    }
                    else
                    {
                        var item = ParseInternal();
                        if (item == null)
                        {
                            break;
                        }
                        expression.Add(item);
                    }
                }
                if (alternatives.Count == 0)
                {
                    return expression.Normalize();
                }
                else
                {
                    alternatives.Add(expression.Normalize());
                    return Alternation.From(alternatives);
                }
            }

            private IExpression ParseInternal()
            {
                char nextChar = regex[index];
                switch (nextChar)
                {
                    case '^': return ParseAnchor(Anchors.Carot);
                    case '$': return ParseAnchor(Anchors.Dollar);
                    case '.': return ParseCharacterClass(CharacterClasses.Wildcard);
                    case '\\': return ParseEscapeSequence();
                    case '[': return ParseCharacterGroup();
                    case '(': return ParseGroup();
                    case '#': return ParseXModeComment();
                    case ')': return null;
                    default: return ParseLiteral(Literal.For(regex[index]));
                }
            }

            private IExpression ParseAnchor(IExpression current)
            {
                ++index;
                current = Quantify(current);
                return current;
            }

            private IExpression ParseLiteral(IExpression current)
            {
                ++index;
                current = Quantify(current);
                return current;
            }

            private IExpression ParseCharacterClass(IExpression current)
            {
                ++index;
                current = Quantify(current);
                return current;
            }

            private IExpression ParseEscapeSequence()
            {
                return ParseEscapeSequenceInternal(ExpressionContext.Group);
            }

            private IExpression ParseEscapeSequenceInternal(ExpressionContext context)
            {
                ++index;
                char nextChar = regex[index];
                switch (nextChar)
                {
                    case 'A': return ParseAnchor(Anchors.A);
                    case 'Z': return ParseAnchor(Anchors.Z);
                    case 'z': return ParseAnchor(Anchors.z);
                    case 'G': return ParseAnchor(Anchors.G);
                    case 'b':
                        return (context == ExpressionContext.CharacterGroup) 
                            ? ParseCharacterEscape(CharacterEscapes.Backspace)
                            : ParseAnchor(Anchors.b);
                    case 'B': return ParseAnchor(Anchors.B);
                    case 'k': return ParseNamedBackreference();
                    case 'w': return ParseCharacterClass(CharacterClasses.Word);
                    case 'W': return ParseCharacterClass(CharacterClasses.NonWord);
                    case 's': return ParseCharacterClass(CharacterClasses.Whitespace);
                    case 'S': return ParseCharacterClass(CharacterClasses.NonWhitespace);
                    case 'd': return ParseCharacterClass(CharacterClasses.Digit);
                    case 'D': return ParseCharacterClass(CharacterClasses.NonDigit);
                    case 'p': return ParseUnicodeCategory(isPositive: true);
                    case 'P': return ParseUnicodeCategory(isPositive: false);
                    case 'a': return ParseCharacterEscape(CharacterEscapes.Bell);
                    case 't': return ParseCharacterEscape(CharacterEscapes.Tab);
                    case 'r': return ParseCharacterEscape(CharacterEscapes.CarriageReturn);
                    case 'v': return ParseCharacterEscape(CharacterEscapes.VerticalTab);
                    case 'f': return ParseCharacterEscape(CharacterEscapes.FormFeed);
                    case 'n': return ParseCharacterEscape(CharacterEscapes.NewLine);
                    case 'e': return ParseCharacterEscape(CharacterEscapes.Escape);
                    case '.': return ParseCharacterEscape(CharacterEscapes.Period);
                    case '$': return ParseCharacterEscape(CharacterEscapes.Dollar);
                    case '^': return ParseCharacterEscape(CharacterEscapes.Carot);
                    case '{': return ParseCharacterEscape(CharacterEscapes.LeftCurlyBrace);
                    case '[': return ParseCharacterEscape(CharacterEscapes.LeftSquareBracket);
                    case '(': return ParseCharacterEscape(CharacterEscapes.LeftParenthesis);
                    case '|': return ParseCharacterEscape(CharacterEscapes.Pipe);
                    case ')': return ParseCharacterEscape(CharacterEscapes.RightParenthesis);
                    case '*': return ParseCharacterEscape(CharacterEscapes.Asterisk);
                    case '+': return ParseCharacterEscape(CharacterEscapes.Plus);
                    case '?': return ParseCharacterEscape(CharacterEscapes.QuestionMark);
                    case '\\': return ParseCharacterEscape(CharacterEscapes.Backslash);
                    case 'x': return ParseHexidecimalEscape();
                    case 'c': return ParseControlCharacterEscape();
                    case 'u': return ParseUnicodeCharacterEscape();
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
                        return ParseOctalEscapeOrNumberedBackreference();
                    default: return ParseCharacterEscape(CharacterEscapes.For(nextChar));
                }
            }

            private IExpression ParseCharacterEscape(IExpression current)
            {
                ++index;
                current = Quantify(current);
                return current;
            }

            private IExpression ParseHexidecimalEscape()
            {
                ++index; // swallow 'x'
                int endIndex = GetHexidecimalNumberEndIndex(Math.Min(index + 2, regex.Length));
                string numberString = regex.Substring(index, endIndex - index);
                index = endIndex; // swallow hex number
                return CharacterEscapes.Hexidecimal(numberString);
            }

            private IExpression ParseControlCharacterEscape()
            {
                ++index; // swallow 'c'
                char controlCharacter = regex[index];
                ++index; // swallow control character
                return CharacterEscapes.ControlCharacter(controlCharacter);
            }

            private IExpression ParseUnicodeCharacterEscape()
            {
                ++index; // swallow 'u'
                int endIndex = GetHexidecimalNumberEndIndex(Math.Min(index + 4, regex.Length));
                string numberString = regex.Substring(index, endIndex - index);
                index = endIndex; // swallow code
                return CharacterEscapes.Unicode(numberString);
            }

            private IExpression ParseNamedBackreference()
            {
                ++index; // swallow 'k'
                bool useQuotes = regex[index] == '\'';
                char closingChar = useQuotes ? '\'' : '>';
                ++index; // swallow opening char
                int endIndex = regex.IndexOf(closingChar, index);
                string name = regex.Substring(index, endIndex - index);
                index = endIndex + 1;  // swallow closing char
                return Backreference.For(name, useQuotes);
            }

            private IExpression ParseOctalEscapeOrNumberedBackreference()
            {
                int endIndex = GetNumberEndIndex();
                string numberString = regex.Substring(index, endIndex - index);
                index = endIndex; // swallow number
                int number = Int32.Parse(numberString);
                if (number >= 1 && number <= 9)
                {
                    return Backreference.For(number);
                }
                else if (numberString[0] == '8' || numberString[0] == '9')
                {
                    return CharacterEscapes.Octal(numberString);
                }
                else if (number >= 10)
                {
                    if (number < groupNames.Count)
                    {
                        return Backreference.For(number);
                    }
                    else
                    {
                        return CharacterEscapes.Octal(numberString);
                    }
                }
                // The Regex constructor to prevent backreferences to \0 and invalid octal codes
                throw new InvalidOperationException();
            }

            private IExpression ParseUnicodeCategory(bool isPositive)
            {
                ++index; // swallow 'p' or 'P'
                if (regex[index] != '{')
                {
                    return CharacterEscapes.For('p');
                }
                ++index; // swallow {
                int endIndex = regex.IndexOf('}', index);
                if (endIndex == -1)
                {
                    // The Regex constructor should prevent this from happening.
                    throw new InvalidOperationException();
                }
                string category = regex.Substring(index, endIndex - index);
                index = endIndex + 1; // swallow category and '}'
                return new UnicodeCategory($@"\{(isPositive ? 'p' : 'P')}{{{category}}}");
            }

            private IExpression ParseCharacterGroup()
            {
                IExpression current = ParseCharacterGroupInternal();
                current = Quantify(current);
                return current;
            }

            private ICharacterGroup ParseCharacterGroupInternal()
            {
                ++index; // swallow '['
                var members = new List<ICharacterGroupMember>();
                bool isNegated = regex[index] == '^';
                if (isNegated)
                {
                    ++index; // swallow '^'
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
                            ++index; // swallow ']'
                            break;
                        case '-':
                            ++index; // swallow '-'
                            if (index == regex.Length)
                            {
                                // The Regex constructor should prevent this
                                throw new InvalidOperationException();
                            }
                            else if (regex[index] == '[')
                            {
                                exclusions = ParseCharacterGroupInternal();
                            }
                            else if (members.Count == 0 || regex[index] == ']')
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
                            break;
                        default:                            
                            var member = ParseCharacterGroupMember();
                            members.Add(member);
                            break;
                    }
                }
                var options = new CharacterGroupOptions() { IsNegated = isNegated, Exclusions = exclusions };
                return CharacterGroup.From(options, members);
            }

            private ICharacterGroupMember ParseRange(ICharacterGroupMember startLiteral)
            {
                var lastLiteral = ParseCharacterGroupMember();
                return Range.For(startLiteral, lastLiteral);
            }

            private ICharacterGroupMember ParseCharacterGroupMember()
            {
                char nextChar = regex[index];
                switch (nextChar)
                {
                    case '\\':
                        return (ICharacterGroupMember)ParseEscapeSequenceInternal(ExpressionContext.CharacterGroup);
                    default:
                        ++index;
                        return Literal.For(nextChar);
                }
            }

            private IExpression ParseGroup()
            {
                ++index; // swallow '('
                options.Push(options.Peek()); // Copy current options
                IExpression current = ParseGroupInternal();
                options.Pop();
                if (index == regex.Length || regex[index] != ')')
                {
                    // The Regex constructor should prevent this
                    throw new InvalidOperationException();
                }
                ++index; // swallow ')'
                current = Quantify(current);
                return current;
            }

            private IExpression ParseGroupInternal()
            {
                char nextChar = regex[index];
                switch (nextChar)
                {
                    case '?': return ParseSpecialGroup();
                    default: return ParseCaptureGroup(null, false);
                }
            }

            private IExpression ParseSpecialGroup()
            {
                ++index; // swallow '?'
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

            private IGroup ParseCaptureGroup(string name, bool useQuotes)
            {
                RegisterGroupName(name);
                var options = new CaptureGroupOptions() { Name = name, UseQuotes = useQuotes };
                var item = Parse();
                return CaptureGroup.Of(options, item);
            }

            private IGroup ParseCaptureGroupOrBalanceGroup(string name, bool useQuotes)
            {
                var names = name.Split(new[] { '-' }, 2);
                if (names.Length == 1)
                {
                    return ParseCaptureGroup(name, useQuotes);
                }
                var (current, previous) = (names[0], names[1]);
                RegisterGroupName(current);
                var options = new BalanceGroupOptions() { UseQuotes = useQuotes };
                var item = Parse();
                return BalancedGroup.Of(current, previous, options, item);
            }

            private void RegisterGroupName(string name)
            {
                int positon = groupNames.Count;
                groupNames.Add(name);
                if (name != null && !groupIndexes.ContainsKey(name))
                {
                    groupIndexes.Add(name, positon);
                }
            }

            private string ParseCaptureGroupName(char closingChar, int startIndex)
            {
                int endIndex = regex.IndexOf(closingChar, Math.Min(startIndex, regex.Length));
                if (endIndex == -1)
                {
                    // The Regex constructor should prevent this
                    throw new InvalidOperationException();
                }
                var name = regex.Substring(startIndex, endIndex - startIndex);
                index = endIndex + 1; // swallow the closing char
                return name;
            }

            private INonCaptureGroup ParseNonCaptureGroup()
            {
                ++index; // swallow ':'
                var item = Parse();
                return NonCaptureGroup.Of(item);
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
                var item = Parse();
                return NegativeLookbehindAssertion.Of(item);
            }

            private IPositiveLookbehindAssertion ParsePositiveLookbehindGroup()
            {
                ++index; // swallow the '='
                var item = Parse();
                return PositiveLookbehindAssertion.Of(item);
            }

            private INegativeLookaheadAssertion ParseNegativeLookaheadGroup()
            {
                ++index; // swallow the '!'
                var item = Parse();
                return NegativeLookaheadAssertion.Of(item);
            }

            private IPositiveLookaheadAssertion ParsePositiveLookaheadGroup()
            {
                ++index; // swallow the '='
                var item = Parse();
                return PositiveLookaheadAssertion.Of(item);
            }

            private IExpression ParseOptions()
            {
                var enabled = GroupRegexOptions.None;
                while (index != regex.Length && TryGetOption(out var option))
                {
                    enabled |= option;
                    ++index;
                }
                if (index == regex.Length || (regex[index] != '-' && regex[index] != ':' && regex[index] != ')'))
                {
                    // The Regex constructor should prevent this
                    throw new InvalidOperationException();
                }
                var disabled = GroupRegexOptions.None;
                if (regex[index] == '-')
                {
                    ++index;
                    while (index != regex.Length && TryGetOption(out var option))
                    {
                        disabled |= option;
                        ++index;
                    }
                    if (index == regex.Length || (regex[index] != ':' && regex[index] != ')'))
                    {
                        // The Regex constructor should prevent this
                        throw new InvalidOperationException();
                    }
                }
                var currentOptions = options.Pop();
                currentOptions |= enabled;
                currentOptions &= ~disabled;
                options.Push(currentOptions);
                if (regex[index] == ':')
                {
                    ++index; // swallow ':'
                    var item = Parse();
                    return OptionsGroup.Of(enabled, disabled, item);
                }
                else
                {
                    return InlineOptions.For(enabled, disabled);
                }
            }

            private bool TryGetOption(out GroupRegexOptions option)
            {
                char nextChar = regex[index];
                switch (nextChar)
                {
                    case 'i':
                        option = GroupRegexOptions.IgnoreCase;
                        return true;
                    case 'm':
                        option = GroupRegexOptions.Multiline;
                        return true;
                    case 'n':
                        option = GroupRegexOptions.ExplicitCapture;
                        return true;
                    case 's':
                        option = GroupRegexOptions.Singleline;
                        return true;
                    case 'x':
                        option = GroupRegexOptions.IgnorePatternWhitespace;
                        return true;
                    default:
                        option = GroupRegexOptions.None;
                        return false;
                }
            }

            private INonbacktrackingAssertion ParseNonbacktrackingAssertion()
            {
                ++index; // swallow the '>'
                var item = Parse();
                return NonbacktrackingAssertion.Of(item);
            }

            private IExpression ParseConditionalAlternation()
            {
                ++index; // swallow '('
                var expressionOrName = Parse();
                if (index == regex.Length || regex[index] != ')')
                {
                    // The Regex constructor should prevent this
                    throw new InvalidOperationException();
                }
                ++index; // swallow ')'

                var item = Parse();
                
                IExpression yes = null;
                IExpression no = null;
                if (item is IAlternation alternation)
                {
                    if (alternation.Alternatives.Count() > 2)
                    {
                        // The Regex constructor should prevent this
                        throw new InvalidOperationException();
                    }
                    yes = alternation.Alternatives.First();
                    no = alternation.Alternatives.Skip(1).FirstOrDefault();
                }
                else
                {
                    yes = item;
                }
                return ConditionalAlternation.For(expressionOrName, yes, no);
            }

            private IExpression ParseInlineComment()
            {
                ++index; // swallow the '#'
                int endIndex = regex.IndexOf(')', index);
                if (endIndex == -1)
                {
                    // The Regex constructor should prevent this
                    throw new InvalidOperationException();
                }
                var comment = regex.Substring(index, endIndex - index);
                index = endIndex;
                return InlineComment.For(comment);
            }

            private IExpression ParseXModeComment()
            {
                ++index; // swallow '#'
                if ((options.Peek() & GroupRegexOptions.IgnorePatternWhitespace) != GroupRegexOptions.IgnorePatternWhitespace)
                {
                    return Literal.For('#');
                }
                int endIndex = regex.IndexOf('\n', index);
                if (endIndex == -1)
                {
                    endIndex = regex.Length;
                }
                else if (regex[endIndex - 1] == '\r')
                {
                    --endIndex;
                }
                string comment = regex.Substring(index, endIndex - index);
                index = endIndex;
                return XModeComment.For(comment);
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
                ++index; // swallow '*', '+', '?' or '{'
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
                if (regex[index] != ',')
                {
                    // The Regex constructor should prevent this
                    throw new InvalidOperationException();
                }
                ++index; // swallow the ','
                SkipWhitespace();
                if (regex[index] == '}')
                {
                    ++index; // swallow the '}'
                    return ValueTuple.Create(min, (int?)null);
                }
                int max = GetQuantifierNumber();
                if (regex[index] != '}')
                {
                    // The Regex constructor should prevent this
                    throw new InvalidOperationException();
                }
                ++index; // swallow the '}'
                return ValueTuple.Create(min, max);
            }

            private int GetQuantifierNumber()
            {
                SkipWhitespace();
                int endIndex = GetNumberEndIndex();
                string numberString = regex.Substring(index, endIndex - index).Trim();
                if (!Int32.TryParse(numberString, out int number))
                {
                    // The Regex constructor should prevent this
                    throw new InvalidOperationException();
                }
                index = endIndex;
                SkipWhitespace();
                return number;
            }

            private void SkipWhitespace()
            {
                while (index != regex.Length && Char.IsWhiteSpace(regex[index]))
                {
                    ++index; // swallow whitespace
                }
                if (index == regex.Length)
                {
                    // The Regex constructor should prevent this
                    throw new InvalidOperationException();
                }
            }

            private int GetNumberEndIndex()
            {
                int endIndex = index;
                while (endIndex != regex.Length && IsDigit(regex[endIndex]))
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

            private static bool IsDigit(char character)
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
                        return true;
                    default:
                        return false;
                }
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
