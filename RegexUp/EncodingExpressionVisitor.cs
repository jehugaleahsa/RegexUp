using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RegexUp
{
    internal sealed class EncodingExpressionVisitor : ExpressionVisitor
    {
        private readonly StringBuilder builder = new StringBuilder();
        private readonly Stack<RegexState> state = new Stack<RegexState>();

        public EncodingExpressionVisitor(GroupRegexOptions options)
        {
            PushState(ExpressionContext.Group);
            Options = options;
        }

        private void PushState(ExpressionContext context, int length = 1)
        {
            var options = state.Count == 0 ? GroupRegexOptions.None : Options;
            var newState = new RegexState()
            {
                Context = context,
                Options = options,
                Position = 0,
                Length = length
            };
            state.Push(newState);
        }

        private void PopState()
        {
            state.Pop();
        }

        public GroupRegexOptions Options
        {
            get => state.Peek().Options;
            set => state.Peek().Options = value;
        }

        public ExpressionContext Context => state.Peek().Context;

        public int Position
        {
            get => state.Peek().Position;
            set => state.Peek().Position = value;
        }

        public int Length => state.Peek().Length;

        public override string ToString() => builder.ToString();

        public static string ToString(IVisitableExpression expression, RegexOptions options = RegexOptions.None)
        {
            var groupOptions = GetGroupOptions(options);
            var visitor = new EncodingExpressionVisitor(groupOptions);
            visitor.Visit(expression);
            return visitor.ToString();
        }

        private static GroupRegexOptions GetGroupOptions(RegexOptions options)
        {
            var result = GroupRegexOptions.None;
            if ((options & RegexOptions.IgnoreCase) == RegexOptions.IgnoreCase)
            {
                result |= GroupRegexOptions.IgnoreCase;
            }
            if ((options & RegexOptions.Multiline) == RegexOptions.Multiline)
            {
                result |= GroupRegexOptions.Multiline;
            }
            if ((options & RegexOptions.ExplicitCapture) == RegexOptions.ExplicitCapture)
            {
                result |= GroupRegexOptions.ExplicitCapture;
            }
            if ((options & RegexOptions.Singleline) == RegexOptions.Singleline)
            {
                result |= GroupRegexOptions.Singleline;
            }
            if ((options & RegexOptions.IgnorePatternWhitespace) == RegexOptions.IgnorePatternWhitespace)
            {
                result |= GroupRegexOptions.IgnorePatternWhitespace;
            }
            return result;
        }

        public override void Visit(IAlternation instance)
        {
            PushState(ExpressionContext.Alternation, instance.Alternatives.Count());
            Join("|", instance.Alternatives);
            PopState();
        }

        public override void Visit(IAnchor instance)
        {
            builder.Append(instance.Value);
        }

        public override void Visit(IBackreference instance)
        {
            if (instance.IsNamed)
            {
                builder.Append(instance.UseQuotes ? $@"\k'{instance.Reference}'" : $@"\k<{instance.Reference}>");
            }
            else
            {
                builder.Append($@"\{instance.Reference}");
            }
        }

        public override void Visit(IBalancedGroup instance)
        {
            builder.Append("(?");
            builder.Append(instance.UseQuotes ? "'" : "<");
            builder.Append(instance.Current);
            builder.Append("-");
            builder.Append(instance.Previous);
            builder.Append(instance.UseQuotes ? "'" : ">");
            PushState(ExpressionContext.Group, instance.Members.Count());
            Join(String.Empty, instance.Members);
            PopState();
            builder.Append(")");
        }

        public override void Visit(ICaptureGroup instance)
        {
            builder.Append("(");
            if (instance.Name != null)
            {
                builder.Append("?");
                builder.Append(instance.UseQuotes ? "'" : "<");
                builder.Append(instance.Name);
                builder.Append(instance.UseQuotes ? "'" : ">");
            }
            PushState(ExpressionContext.Group, instance.Members.Count());
            Join(String.Empty, instance.Members);
            PopState();
            builder.Append(")");
        }

        public override void Visit(ICharacterEscape instance)
        {
            builder.Append(instance.Value);
        }

        public override void Visit(ICharacterGroup instance)
        {
            builder.Append("[");
            if (instance.IsNegated)
            {
                builder.Append("^");
            }
            PushState(ExpressionContext.CharacterGroup, instance.Members.Count());
            Join(String.Empty, instance.Members);
            if (instance.Exclusions != null && instance.Exclusions.Members.Any())
            {
                builder.Append("-");
                instance.Exclusions.Accept(this);
            }
            PopState();
            builder.Append("]");
        }

        public override void Visit(ICompoundExpression instance)
        {
            PushState(ExpressionContext.Group, instance.Members.Count());
            Join(String.Empty, instance.Members);
            PopState();
        }

        public override void Visit(ICompoundLiteral instance)
        {
            PushState(Context, instance.Literals.Count());
            Join(String.Empty, instance.Literals);
            PopState();
        }

        public override void Visit(IConditionalAlternation instance)
        {
            builder.Append("(?(");

            PushState(ExpressionContext.Group);
            instance.Expression.Accept(this);
            PopState();
            builder.Append(")");

            PushState(ExpressionContext.Alternation);
            instance.YesOption.Accept(this);
            ++Position;
            if (instance.NoOption != null)
            {
                builder.Append("|");
                instance.NoOption.Accept(this);
            }
            PopState();

            builder.Append(")");
        }

        public override void Visit(IControlCharacterEscape instance)
        {
            builder.Append($@"\c{instance.ControlCharacter}");
        }

        public override void Visit(IHexidecimalEscape instance)
        {
            var hexString = Convert.ToString(instance.CharacterCode, 16).PadLeft(2, '0');
            builder.Append($@"\x{hexString}");
        }

        public override void Visit(IInlineComment instance)
        {
            builder.Append($"(?#{instance.Comment})");
        }

        public override void Visit(IInlineOptions instance)
        {
            builder.Append("(?");
            EncodeOptions(instance.EnabledOptions);
            if (instance.DisabledOptions != GroupRegexOptions.None)
            {
                builder.Append("-");
                EncodeOptions(instance.DisabledOptions);
            }
            builder.Append(")");
            Options |= instance.EnabledOptions;
            Options &= ~instance.DisabledOptions;
        }

        public override void Visit(ILiteral instance)
        {
            if (Char.IsWhiteSpace(instance.Value))
            {
                VisitWhitespace(instance);
            }
            else if (Context == ExpressionContext.CharacterGroup)
            {
                VisitCharacterGroupMember(instance);
            }
            else
            {
                VisitGroupMember(instance);
            }
        }

        private void VisitWhitespace(ILiteral instance)
        {
            bool ignoreWhiteSpace = (Options & GroupRegexOptions.IgnorePatternWhitespace) == GroupRegexOptions.IgnorePatternWhitespace;
            if (ignoreWhiteSpace)
            {
                builder.Append(instance.Value);
            }
            else
            {
                builder.Append($@"\{instance.Value}");
            }
        }

        private void VisitCharacterGroupMember(ILiteral instance)
        {
            switch (instance.Value)
            {
                case '\\':
                    builder.Append(@"\\");
                    break;
                case '-':
                    if (Position == Length - 1)
                    {
                        builder.Append(instance.Value);
                    }
                    else
                    {
                        builder.Append(@"\-");
                    }
                    break;
                case '^':
                    if (Position == 0)
                    {
                        builder.Append(@"\^");
                    }
                    else
                    {
                        builder.Append(instance.Value);
                    }
                    break;
                default:
                    builder.Append(instance.Value);
                    break;
            }
        }

        private void VisitGroupMember(ILiteral instance)
        {
            switch (instance.Value)
            {
                case '\\':
                    builder.Append(@"\\");
                    break;
                case '*':
                    builder.Append(@"\*");
                    break;
                case '+':
                    builder.Append(@"\+");
                    break;
                case '?':
                    builder.Append(@"\?");
                    break;
                case '|':
                    builder.Append(@"\|");
                    break;
                case '{':
                    builder.Append(@"\{");
                    break;
                case '[':
                    builder.Append(@"\[");
                    break;
                case '(':
                    builder.Append(@"\(");
                    break;
                case ')':
                    builder.Append(@"\)");
                    break;
                case '^':
                    builder.Append(@"\^");
                    break;
                case '$':
                    builder.Append(@"\$");
                    break;
                case '.':
                    builder.Append(@"\");
                    break;
                case '#':
                    builder.Append(@"\#");
                    break;
                default:
                    builder.Append(instance.Value);
                    break;
            }
        }

        public override void Visit(INegativeLookaheadAssertion instance)
        {
            builder.Append("(?!");
            PushState(ExpressionContext.Group, instance.Members.Count());
            Join(String.Empty, instance.Members);
            PopState();
            builder.Append(")");
        }

        public override void Visit(INegativeLookbehindAssertion instance)
        {
            builder.Append("(?<!");
            PushState(ExpressionContext.Group, instance.Members.Count());
            Join(String.Empty, instance.Members);
            PopState();
            builder.Append(")");
        }

        public override void Visit(INonbacktrackingAssertion instance)
        {
            builder.Append("(?>");
            PushState(ExpressionContext.Group, instance.Members.Count());
            Join(String.Empty, instance.Members);
            PopState();
            builder.Append(")");
        }

        public override void Visit(INonCaptureGroup instance)
        {
            builder.Append("(?:");
            PushState(ExpressionContext.Group, instance.Members.Count());
            Join(String.Empty, instance.Members);
            PopState();
            builder.Append(")");
        }

        public override void Visit(IOctalEscape instance)
        {
            var octalString = Convert.ToString(instance.CharacterCode, 8).PadLeft(instance.Width, '0');
            builder.Append($@"\{octalString}");
        }

        public override void Visit(IOptionsGroup instance)
        {
            builder.Append("(?");
            EncodeOptions(instance.EnabledOptions);
            if (instance.DisabledOptions != GroupRegexOptions.None)
            {
                builder.Append("-");
                EncodeOptions(instance.DisabledOptions);
            }
            builder.Append(":");
            PushState(ExpressionContext.Group, instance.Members.Count());
            Options |= instance.EnabledOptions;
            Options &= ~instance.DisabledOptions;
            Join(String.Empty, instance.Members);
            PopState();
            builder.Append(")");
        }

        public override void Visit(IPositiveLookaheadAssertion instance)
        {
            builder.Append("(?=");
            PushState(ExpressionContext.Group, instance.Members.Count());
            Join(String.Empty, instance.Members);
            PopState();
            builder.Append(")");
        }

        public override void Visit(IPositiveLookbehindAssertion instance)
        {
            builder.Append("(?<=");
            PushState(ExpressionContext.Group, instance.Members.Count());
            Join(String.Empty, instance.Members);
            PopState();
            builder.Append(")");
        }

        public override void Visit(IQuantifiedExpression instance)
        {
            PushState(Context, 1);
            if (instance.Expression.NeedsGroupedToQuantify())
            {
                var group = NonCaptureGroup.Of(instance.Expression);
                group.Accept(this);
            }
            else
            {
                instance.Expression.Accept(this);
            }
            PopState();
            builder.Append(instance.Quantifier);
            if (!instance.IsGreedy)
            {
                builder.Append("?");
            }
        }

        public override void Visit(IRange instance)
        {
            instance.First.Accept(this);
            builder.Append("-");
            instance.Last.Accept(this);
        }

        public override void Visit(IRegularExpression instance)
        {
            PushState(ExpressionContext.Group, instance.Members.Count());
            Join(String.Empty, instance.Members);
            PopState();
        }

        public override void Visit(IUnicodeCategory instance)
        {
            builder.Append(instance.Value);
        }

        public override void Visit(IUnicodeEscape instance)
        {
            var hexidecimalString = Convert.ToString(instance.CharacterCode, 16).PadLeft(4, '0');
            builder.Append($@"\u{hexidecimalString}");
        }

        private void Join(string separator, IEnumerable<IVisitableExpression> expressions)
        {
            using (var enumerator = expressions.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    enumerator.Current.Accept(this);
                    while (enumerator.MoveNext())
                    {
                        ++Position;
                        builder.Append(separator);
                        enumerator.Current.Accept(this);
                    }
                }
            }
        }

        private void EncodeOptions(GroupRegexOptions options)
        {
            if ((options & GroupRegexOptions.IgnoreCase) == GroupRegexOptions.IgnoreCase)
            {
                builder.Append("i");
            }
            if ((options & GroupRegexOptions.Multiline) == GroupRegexOptions.Multiline)
            {
                builder.Append("m");
            }
            if ((options & GroupRegexOptions.ExplicitCapture) == GroupRegexOptions.ExplicitCapture)
            {
                builder.Append("n");
            }
            if ((options & GroupRegexOptions.Singleline) == GroupRegexOptions.Singleline)
            {
                builder.Append("s");
            }
            if ((options & GroupRegexOptions.IgnorePatternWhitespace) == GroupRegexOptions.IgnorePatternWhitespace)
            {
                builder.Append("x");
            }
        }

        private sealed class RegexState
        {
            public GroupRegexOptions Options { get; set; }

            public ExpressionContext Context { get; set; }

            public int Position { get; set; }

            public int Length { get; set; }
        }
    }
}
