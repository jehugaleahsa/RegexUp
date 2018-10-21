using System;
using System.Collections.Generic;
using System.Linq;
using RegexUp.Properties;

namespace RegexUp
{
    /// <summary>
    /// Provides factory methods for creating different groups.
    /// </summary>
    public abstract class Group : IGroup, IExpression
    {
        /// <summary>
        /// Provides factory methods for creating capture groups.
        /// </summary>
        public static class Capture
        {
            /// <summary>
            /// Creates a new group that captures its subexpressions by number or name (optional).
            /// </summary>
            /// <param name="name">An optional name to associate with the capture group.</param>
            /// <param name="expressions">The sub-expressions appearing in the group.</param>
            /// <returns>The capture group.</returns>
            public static ICaptureGroup Of(params IGroupMember[] expressions)
            {
                return From(null, expressions);
            }

            /// <summary>
            /// Creates a new group that captures its subexpressions by number or name (optional).
            /// </summary>
            /// <param name="name">An optional name to associate with the capture group.</param>
            /// <param name="expressions">The sub-expressions appearing in the group.</param>
            /// <returns>The capture group.</returns>
            public static ICaptureGroup Of(string name, params IGroupMember[] expressions)
            {
                return From(name, expressions);
            }

            /// <summary>
            /// Creates a new group that captures its subexpressions by number or name (optional).
            /// </summary>
            /// <param name="name">An optional name to associate with the capture group.</param>
            /// <param name="expressions">The sub-expressions appearing in the group.</param>
            /// <returns>The capture group.</returns>
            public static ICaptureGroup From(IEnumerable<IGroupMember> expressions)
            {
                return From(null, expressions);
            }

            /// <summary>
            /// Creates a new group that captures its subexpressions by number or name (optional).
            /// </summary>
            /// <param name="name">An optional name to associate with the capture group.</param>
            /// <param name="expressions">The sub-expressions appearing in the group.</param>
            /// <returns>The capture group.</returns>
            public static ICaptureGroup From(string name, IEnumerable<IGroupMember> expressions)
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
        }

        /// <summary>
        /// Provides factory methods for creating balanced groups.
        /// </summary>
        public static class Balanced
        {
            /// <summary>
            /// Creates a balanced group.
            /// </summary>
            /// <param name="current">The name to store the result in.</param>
            /// <param name="previous">The starting group name to replace.</param>
            /// <returns>The balance group.</returns>
            public static IBalancedGroup Of(string current, string previous, params IGroupMember[] members)
            {
                return From(current, previous, members);
            }

            /// <summary>
            /// Creates a balanced group.
            /// </summary>
            /// <param name="current">The name to store the result in.</param>
            /// <param name="previous">The starting group name to replace.</param>
            /// <returns>The balance group.</returns>
            public static IBalancedGroup From(string current, string previous, IEnumerable<IGroupMember> members)
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
        }

        private static void ValidateCaptureGroupName(string parameterName, string name)
        {
            if (name != null && (Char.IsDigit(name[0]) || name.Any(Char.IsPunctuation)))
            {
                throw new ArgumentException(Resources.InvalidCaptureGroupName, parameterName);
            }
        }

        /// <summary>
        /// Provides factory methods for creating non-capture groups.
        /// </summary>
        public static class NonCapture
        {
            /// <summary>
            /// Creates a group that is not captured.
            /// </summary>
            /// <returns>The non-capture group.</returns>
            public static INonCaptureGroup Of(params IGroupMember[] expressions)
            {
                return From(expressions);
            }

            /// <summary>
            /// Creates a group that is not captured.
            /// </summary>
            /// <returns>The non-capture group.</returns>
            public static INonCaptureGroup From(IEnumerable<IGroupMember> expressions)
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
        }

        /// <summary>
        /// Provides factory methods for creating groups with different regex options.
        /// </summary>
        public static class Options
        {
            /// <summary>
            /// Creates a group that can enable or disable options for its subexpressions.
            /// </summary>
            /// <param name="enabled">The options to enable.</param>
            /// <param name="disabled">The options to disable.</param>
            /// <returns>The options group.</returns>
            public static IOptionsGroup Of(GroupRegexOptions enabled, GroupRegexOptions disabled, params IGroupMember[] members)
            {
                return From(enabled, disabled, members);
            }

            /// <summary>
            /// Creates a group that can enable or disable options for its subexpressions.
            /// </summary>
            /// <param name="enabled">The options to enable.</param>
            /// <param name="disabled">The options to disable.</param>
            /// <returns>The options group.</returns>
            public static IOptionsGroup From(GroupRegexOptions enabled, GroupRegexOptions disabled, IEnumerable<IGroupMember> members)
            {
                if (members == null)
                {
                    throw new ArgumentNullException(nameof(members));
                }
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

        private readonly List<IGroupMember> members = new List<IGroupMember>();

        protected Group()
        {
        }

        string IExpression.Encode(ExpressionContext context)
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
            var encoded = String.Join(String.Empty, members.Cast<IExpression>().Select(m => m.Encode(ExpressionContext.Group)));
            return encoded;
        }

        public void Add(IGroupMember expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }
            members.Add(expression);
        }

        public override string ToString() => ((IExpression)this).Encode(ExpressionContext.Group);
    }
}
