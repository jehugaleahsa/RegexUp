using System;
using System.Collections.Generic;
using System.Linq;
using RegexUp.Properties;

namespace RegexUp
{
    /// <summary>
    /// Provides factory methods for creating different groups.
    /// </summary>
    public abstract class Group : IGroup, IExpressionEncoder
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
            /// <param name="members">The sub-expressions appearing in the group.</param>
            /// <returns>The capture group.</returns>
            public static ICaptureGroup Of(params IExpression[] members)
            {
                return From(null, members);
            }

            /// <summary>
            /// Creates a new group that captures its subexpressions by number or name (optional).
            /// </summary>
            /// <param name="options">The capture group options to use.</param>
            /// <param name="members">The sub-expressions appearing in the group.</param>
            /// <returns>The capture group.</returns>
            public static ICaptureGroup Of(CaptureGroupOptions options, params IExpression[] members)
            {
                return From(options, members);
            }

            /// <summary>
            /// Creates a new group that captures its subexpressions by number or name (optional).
            /// </summary>
            /// <param name="members">The sub-expressions appearing in the group.</param>
            /// <returns>The capture group.</returns>
            public static ICaptureGroup From(IEnumerable<IExpression> members)
            {
                return From(null, members);
            }

            /// <summary>
            /// Creates a new group that captures its subexpressions by number or name (optional).
            /// </summary>
            /// <param name="options">The capture group options to use -or- null, if no options are provided.</param>
            /// <param name="members">The sub-expressions appearing in the group.</param>
            /// <returns>The capture group.</returns>
            public static ICaptureGroup From(CaptureGroupOptions options, IEnumerable<IExpression> members)
            {
                if (members == null)
                {
                    throw new ArgumentNullException(nameof(members));
                }
                var group = new CaptureGroup();
                if (options != null)
                {
                    string name = String.IsNullOrWhiteSpace(options.Name) ? null : options.Name;
                    ValidateCaptureGroupName(nameof(options), name);
                    group.Name = name;
                    group.UseQuotes = options.UseQuotes;
                }
                foreach (var member in members)
                {
                    group.Add(member);
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
            /// <param name="members">The sub-expressions appearing in the group.</param>
            /// <returns>The balance group.</returns>
            public static IBalancedGroup Of(string current, string previous, params IExpression[] members)
            {
                return From(current, previous, null, members);
            }

            /// <summary>
            /// Creates a balanced group.
            /// </summary>
            /// <param name="current">The name to store the result in.</param>
            /// <param name="previous">The starting group name to replace.</param>
            /// <param name="members">The sub-expressions appearing in the group.</param>
            /// <param name="options">The balance group options to use -or- null, if no options are provided.</param>
            /// <returns>The balance group.</returns>
            public static IBalancedGroup Of(string current, string previous, BalanceGroupOptions options, params IExpression[] members)
            {
                return From(current, previous, options, members);
            }

            /// <summary>
            /// Creates a balanced group.
            /// </summary>
            /// <param name="current">The name to store the result in.</param>
            /// <param name="previous">The starting group name to replace.</param>
            /// <param name="members">The sub-expressions appearing in the group.</param>
            /// <returns>The balance group.</returns>
            public static IBalancedGroup From(string current, string previous, IEnumerable<IExpression> members)
            {
                return From(current, previous, null, members);
            }

            /// <summary>
            /// Creates a balanced group.
            /// </summary>
            /// <param name="current">The name to store the result in.</param>
            /// <param name="previous">The starting group name to replace.</param>
            /// <param name="options">The sub-expressions appearing in the group.</param>
            /// <param name="options">The balance group options to use -or- null, if no options are provided.</param>
            /// <returns>The balance group.</returns>
            public static IBalancedGroup From(string current, string previous, BalanceGroupOptions options, IEnumerable<IExpression> members)
            {
                if (String.IsNullOrWhiteSpace(current))
                {
                    current = null;
                }
                ValidateCaptureGroupName(nameof(current), current);
                if (String.IsNullOrWhiteSpace(previous))
                {
                    throw new ArgumentException(Resources.PreviousGroupNameBlank, nameof(previous));
                }
                ValidateCaptureGroupName(nameof(previous), previous);
                var group = new BalancedGroup()
                {
                    Current = current,
                    Previous = previous,
                    UseQuotes = options?.UseQuotes ?? false
                };
                foreach (var member in members)
                {
                    group.Add(member);
                }
                return group;
            }
        }

        internal static void ValidateCaptureGroupName(string parameterName, string name)
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
            public static INonCaptureGroup Of(params IExpression[] members)
            {
                return From(members);
            }

            /// <summary>
            /// Creates a group that is not captured.
            /// </summary>
            /// <returns>The non-capture group.</returns>
            public static INonCaptureGroup From(IEnumerable<IExpression> members)
            {
                if (members == null)
                {
                    throw new ArgumentNullException(nameof(members));
                }
                var group = new NonCaptureGroup();
                foreach (var expression in members)
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
            public static IOptionsGroup Of(GroupRegexOptions enabled, GroupRegexOptions disabled, params IExpression[] members)
            {
                return From(enabled, disabled, members);
            }

            /// <summary>
            /// Creates a group that can enable or disable options for its subexpressions.
            /// </summary>
            /// <param name="enabled">The options to enable.</param>
            /// <param name="disabled">The options to disable.</param>
            /// <returns>The options group.</returns>
            public static IOptionsGroup From(GroupRegexOptions enabled, GroupRegexOptions disabled, IEnumerable<IExpression> members)
            {
                if (members == null)
                {
                    throw new ArgumentNullException(nameof(members));
                }
                ValidateRegexOptions(nameof(enabled), enabled);
                ValidateRegexOptions(nameof(disabled), disabled);
                var group = new OptionsGroup() { EnabledOptions = enabled, DisabledOptions = disabled };
                foreach (var member in members)
                {
                    group.Add(member);
                }
                return group;
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

        /// <summary>
        /// Provides factory methods for creating group constructs defining a zero-width positive lookahead assertion.
        /// </summary>
        public static class LookaheadAssertions
        {
            /// <summary>
            /// Provides factory methods for creating positive lookahead assertion groups.
            /// </summary>
            public static class Positive
            {
                /// <summary>
                /// Creates a group that is not captured.
                /// </summary>
                /// <returns>The non-capture group.</returns>
                public static IPositiveLookaheadAssertionGroup Of(params IExpression[] members)
                {
                    return From(members);
                }

                /// <summary>
                /// Creates a group that is not captured.
                /// </summary>
                /// <returns>The non-capture group.</returns>
                public static IPositiveLookaheadAssertionGroup From(IEnumerable<IExpression> members)
                {
                    if (members == null)
                    {
                        throw new ArgumentNullException(nameof(members));
                    }
                    var group = new PositiveLookaheadAssertionGroup();
                    foreach (var expression in members)
                    {
                        group.Add(expression);
                    }
                    return group;
                }
            }

            /// <summary>
            /// Provides factory methods for creating negative lookahead assertion groups.
            /// </summary>
            public static class Negative
            {
                /// <summary>
                /// Creates a group that is not captured.
                /// </summary>
                /// <returns>The non-capture group.</returns>
                public static INegativeLookaheadAssertionGroup Of(params IExpression[] members)
                {
                    return From(members);
                }

                /// <summary>
                /// Creates a group that is not captured.
                /// </summary>
                /// <returns>The non-capture group.</returns>
                public static INegativeLookaheadAssertionGroup From(IEnumerable<IExpression> members)
                {
                    if (members == null)
                    {
                        throw new ArgumentNullException(nameof(members));
                    }
                    var group = new NegativeLookaheadAssertionGroup();
                    foreach (var expression in members)
                    {
                        group.Add(expression);
                    }
                    return group;
                }
            }
        }

        /// <summary>
        /// Provides factory methods for creating group constructs defining a zero-width positive lookahead assertion.
        /// </summary>
        public static class LookbehindAssertions
        {
            /// <summary>
            /// Provides factory methods for creating positive lookahead assertion groups.
            /// </summary>
            public static class Positive
            {
                /// <summary>
                /// Creates a group that is not captured.
                /// </summary>
                /// <returns>The non-capture group.</returns>
                public static IPositiveLookbehindAssertionGroup Of(params IExpression[] members)
                {
                    return From(members);
                }

                /// <summary>
                /// Creates a group that is not captured.
                /// </summary>
                /// <returns>The non-capture group.</returns>
                public static IPositiveLookbehindAssertionGroup From(IEnumerable<IExpression> members)
                {
                    if (members == null)
                    {
                        throw new ArgumentNullException(nameof(members));
                    }
                    var group = new PositiveLookbehindAssertionGroup();
                    foreach (var expression in members)
                    {
                        group.Add(expression);
                    }
                    return group;
                }
            }

            /// <summary>
            /// Provides factory methods for creating negative lookahead assertion groups.
            /// </summary>
            public static class Negative
            {
                /// <summary>
                /// Creates a group that is not captured.
                /// </summary>
                /// <returns>The non-capture group.</returns>
                public static INegativeLookbehindAssertionGroup Of(params IExpression[] members)
                {
                    return From(members);
                }

                /// <summary>
                /// Creates a group that is not captured.
                /// </summary>
                /// <returns>The non-capture group.</returns>
                public static INegativeLookbehindAssertionGroup From(IEnumerable<IExpression> members)
                {
                    if (members == null)
                    {
                        throw new ArgumentNullException(nameof(members));
                    }
                    var group = new NegativeLookbehindAssertionGroup();
                    foreach (var expression in members)
                    {
                        group.Add(expression);
                    }
                    return group;
                }
            }
        }

        /// <summary>
        /// Provides factory methods for creating non-backtracking sub-expressions.
        /// </summary>
        public static class Nonbacktracking
        {
            /// <summary>
            /// Creates a group that cannot be backtracked.
            /// </summary>
            /// <returns>The non-backtracked expression group.</returns>
            public static INonbacktrackingGroup Of(params IExpression[] members)
            {
                return From(members);
            }

            /// <summary>
            /// Creates a group that cannot be backtracked.
            /// </summary>
            /// <returns>The non-backtracked expression group.</returns>
            public static INonbacktrackingGroup From(IEnumerable<IExpression> members)
            {
                if (members == null)
                {
                    throw new ArgumentNullException(nameof(members));
                }
                var group = new NonbacktrackingGroup();
                foreach (var expression in members)
                {
                    group.Add(expression);
                }
                return group;
            }
        }

        private readonly List<IExpression> members = new List<IExpression>();

        protected Group()
        {
        }

        bool IExpression.NeedsGroupedToQuantify()
        {
            return false;
        }

        string IExpressionEncoder.Encode(ExpressionContext context)
        {
            return OnEncode();
        }

        protected abstract string OnEncode();

        protected virtual string EncodeMembers()
        {
            var encoded = String.Join(String.Empty, members.Cast<IExpressionEncoder>().Select(m => m.Encode(ExpressionContext.Group)));
            return encoded;
        }

        public void Add(IExpression member)
        {
            if (member == null)
            {
                throw new ArgumentNullException(nameof(member));
            }
            members.Add(member);
        }

        public override string ToString() => ((IExpressionEncoder)this).Encode(ExpressionContext.Group);
    }
}
