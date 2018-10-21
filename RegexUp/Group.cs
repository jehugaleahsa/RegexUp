using System;
using System.Collections.Generic;
using System.Linq;
using RegexUp.Properties;

namespace RegexUp
{
    /// <summary>
    /// A group delineates subexpressions of a regular expression and can capture the substrings of an input string.
    /// </summary>
    public interface IGroup : IExpression
    {
        void Add(IExpression expression);
    }

    /// <summary>
    /// A grouping construct that captures the matched subexpression and lets you access it by number or name (optional).
    /// </summary>
    public interface ICaptureGroup : IGroup
    {
        string Name { get; }
    }

    /// <summary>
    /// A balancing group definition deletes the definition of a previously defined group and stores, in the current group, the interval between the previously defined group and the current group.
    /// </summary>
    public interface IBalancedGroup : IGroup
    {
        string Current { get; }

        string Previous { get; }
    }

    /// <summary>
    /// Groups the subexpressions but does not create a capture.
    /// </summary>
    public interface INonCaptureGroup : IGroup
    {
    }

    /// <summary>
    /// A group that enables and/or disables regular expression options for the subexpressions.
    /// </summary>
    public interface IOptionsGroup : IGroup
    {
        GroupRegexOptions EnabledOptions { get; }

        GroupRegexOptions DisabledOptions { get; }
    }

    /// <summary>
    /// Provides factory methods for creating different groups.
    /// </summary>
    public static class Groups
    {
        /// <summary>
        /// Creates a new group that captures its subexpressions by number or name (optional).
        /// </summary>
        /// <param name="name">An optional name to associate with the capture group.</param>
        /// <param name="expressions">The sub-expressions appearing in the group.</param>
        /// <returns>The capture group.</returns>
        public static ICaptureGroup Capture(params IExpression[] expressions)
        {
            return Capture(null, expressions);
        }

        /// <summary>
        /// Creates a new group that captures its subexpressions by number or name (optional).
        /// </summary>
        /// <param name="name">An optional name to associate with the capture group.</param>
        /// <param name="expressions">The sub-expressions appearing in the group.</param>
        /// <returns>The capture group.</returns>
        public static ICaptureGroup Capture(string name, params IExpression[] expressions)
        {
            if (expressions == null)
            {
                throw new ArgumentNullException(nameof(expressions));
            }
            if (String.IsNullOrWhiteSpace(name))
            {
                name = null;
            }
            ValidateCaptureGroupName(nameof(name), name);
            var group = new CaptureGroup() { Name = name };
            foreach (var expression in expressions)
            {
                group.Add(expression);
            }
            return group;
        }

        /// <summary>
        /// Creates a balanced group.
        /// </summary>
        /// <param name="current">The name to store the result in.</param>
        /// <param name="previous">The starting group name to replace.</param>
        /// <returns>The balance group.</returns>
        public static IBalancedGroup Balanced(string current, string previous)
        {
            if (String.IsNullOrWhiteSpace(current))
            {
                throw new ArgumentException(Resources.CurrentGroupNameBlank, nameof(current));
            }
            ValidateCaptureGroupName(nameof(current), current);
            if (String.IsNullOrWhiteSpace(previous))
            {
                throw new ArgumentException(Resources.PreviousGroupNameBlank, nameof(previous));
            }
            ValidateCaptureGroupName(nameof(previous), previous);
            return new BalancedGroup() { Current = current, Previous = previous };
        }

        private static void ValidateCaptureGroupName(string parameterName, string name)
        {
            if (name != null && (Char.IsDigit(name[0]) || name.Any(Char.IsPunctuation)))
            {
                throw new ArgumentException(Resources.InvalidCaptureGroupName, parameterName);
            }
        }

        /// <summary>
        /// Creates a group that is not captured.
        /// </summary>
        /// <returns>The non-capture group.</returns>
        public static INonCaptureGroup NonCapture(params IExpression[] expressions)
        {
            if (expressions == null)
            {
                throw new ArgumentNullException(nameof(expressions));
            }
            var group = new NonCaptureGroup();
            foreach (var expression in expressions)
            {
                group.Add(expression);
            }
            return group;
        }

        /// <summary>
        /// Creates a group that can enable or disable options for its subexpressions.
        /// </summary>
        /// <param name="enabled">The options to enable.</param>
        /// <param name="disabled">The options to disable.</param>
        /// <returns>The options group.</returns>
        public static IOptionsGroup Options(GroupRegexOptions enabled, GroupRegexOptions disabled)
        {
            ValidateRegexOptions(nameof(enabled), enabled);
            ValidateRegexOptions(nameof(disabled), disabled);
            return new OptionsGroup() { EnabledOptions = enabled, DisabledOptions = disabled };
        }

        private static void ValidateRegexOptions(string parameterName, GroupRegexOptions options)
        {
            if ((options & GroupRegexOptions.Multiline) == GroupRegexOptions.Multiline && (options & GroupRegexOptions.Singleline) == GroupRegexOptions.Singleline)
            {
                throw new ArgumentException(Resources.SingleAndMultilineMode, parameterName);
            }
            var allOptions = GroupRegexOptions.IgnoreCase | GroupRegexOptions.Multiline | GroupRegexOptions.ExplicitCapture | GroupRegexOptions.Singleline | GroupRegexOptions.IgnorePatternWhitespace;
            options &= ~allOptions;
            if (options != 0)
            {
                throw new ArgumentException(Resources.InvalidGroupOptions, parameterName);
            }
        }
    }

    internal abstract class Group : IGroup
    {
        private readonly List<IExpression> members = new List<IExpression>();

        protected Group()
        {
        }

        public string Encode(ExpressionContext context)
        {
            if (members.Count == 0)
            {
                return String.Empty;
            }
            return OnEncode();
        }

        protected abstract string OnEncode();

        protected virtual string EncodeMembers()
        {
            var encoded = String.Join(String.Empty, members.Select(m => m.Encode(ExpressionContext.Group)));
            return encoded;
        }

        public void Add(IExpression expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }
            members.Add(expression);
        }

        public override string ToString() => Encode(ExpressionContext.TopLevel);
    }

    internal sealed class CaptureGroup : Group, ICaptureGroup
    {
        public string Name { get; set; }

        protected override string OnEncode()
        {
            var parts = new List<string>();
            parts.Add("(");
            if (Name != null)
            {
                parts.Add("?<");
                parts.Add(Name);
                parts.Add(">");
            }
            parts.Add(EncodeMembers());
            parts.Add(")");
            var encoded = String.Join(String.Empty, parts);
            return encoded;
        }
    }

    internal sealed class BalancedGroup : Group, IBalancedGroup
    {
        public string Current { get; set; }

        public string Previous { get; set; }

        protected override string OnEncode()
        {
            var parts = new[] { "(?<", Current, "-", Previous, ">", EncodeMembers(), ")" };
            var encoded = String.Join(String.Empty, parts);
            return encoded;
        }
    }

    internal sealed class NonCaptureGroup : Group, INonCaptureGroup
    {
        protected override string OnEncode()
        {
            var parts = new[] { "(?:", EncodeMembers(), ")" };
            var encoded = String.Join(String.Empty, parts);
            return encoded;
        }
    }

    internal sealed class OptionsGroup : Group, IOptionsGroup
    {
        public GroupRegexOptions EnabledOptions { get; set;  }

        public GroupRegexOptions DisabledOptions { get; set; }

        protected override string OnEncode()
        {
            var parts = new List<string>();
            parts.Add("(");
            parts.Add(EncodeOptions(EnabledOptions));
            if (DisabledOptions != GroupRegexOptions.None)
            {
                parts.Add("-");
                parts.Add(EncodeOptions(DisabledOptions));
            }
            parts.Add(EncodeMembers());
            parts.Add(")");
            var encoded = String.Join(String.Empty, parts);
            return encoded;
        }

        private string EncodeOptions(GroupRegexOptions options)
        {
            var parts = new List<string>();
            if ((options & GroupRegexOptions.IgnoreCase) == GroupRegexOptions.IgnoreCase)
            {
                parts.Add("i");
            }
            if ((options & GroupRegexOptions.Multiline) == GroupRegexOptions.Multiline)
            {
                parts.Add("m");
            }
            if ((options & GroupRegexOptions.ExplicitCapture) == GroupRegexOptions.ExplicitCapture)
            {
                parts.Add("n");
            }
            if ((options & GroupRegexOptions.Singleline) == GroupRegexOptions.Singleline)
            {
                parts.Add("s");
            }
            if ((options & GroupRegexOptions.IgnorePatternWhitespace) == GroupRegexOptions.IgnorePatternWhitespace)
            {
                parts.Add("x");
            }
            var encoded = String.Join(String.Empty, parts);
            return encoded;
        }
    }
}
