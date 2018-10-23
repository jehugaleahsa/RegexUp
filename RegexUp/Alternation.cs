using System;
using System.Collections.Generic;
using System.Linq;
using RegexUp.Properties;

namespace RegexUp
{
    /// <summary>
    /// Provides factory methods for creating alternations.
    /// </summary>
    public sealed class Alternation : IAlternation, IExpression, IContainer, IExpressionEncoder
    {
        /// <summary>
        /// Creates an alternation consisting of the given alternatives.
        /// </summary>
        /// <param name="alternatives">The alternatives comprising the alternation.</param>
        /// <returns>The alternation.</returns>
        public static IAlternation Of(params IExpression[] alternatives)
        {
            return From(alternatives);
        }

        /// <summary>
        /// Creates an alternation consisting of the given alternatives.
        /// </summary>
        /// <param name="alternatives">The alternatives comprising the alternation.</param>
        /// <returns>The alternation.</returns>
        public static IAlternation From(IEnumerable<IExpression> alternatives)
        {
            if (alternatives == null)
            {
                throw new ArgumentNullException(nameof(alternatives));
            }
            var alternation = new Alternation();
            foreach (var alternative in alternatives)
            {
                alternation.Add(alternative);
            }
            return alternation;
        }

        /// <summary>
        /// Provides factory methods for creating conditional alternations.
        /// </summary>
        public static class Conditional
        {
            /// <summary>
            /// Creates an alternation that tries to match the 'yes' option if the expression is matched.
            /// </summary>
            /// <param name="expression">The expression that must be matched for the 'yes' option to be matched.</param>
            /// <param name="yes">The alternative to match if the capture group is matched.</param>
            /// <param name="no">The alternative to match if the capture group is not matched.</param>
            /// <returns>The alternation.</returns>
            public static IExpressionBasedConditionalAlternation ByExpression(IExpression expression, IExpression yes, IExpression no)
            {
                if (expression == null)
                {
                    throw new ArgumentNullException(nameof(expression));
                }
                if (yes == null)
                {
                    throw new ArgumentNullException(nameof(yes));
                }
                var alternation = new ExpressionBasedConditionalAlternation()
                {
                    Expression = expression,
                    YesOption = yes,
                    NoOption = no
                };
                return alternation;
            }

            /// <summary>
            /// Creates an alternation that tries to match the 'yes' option if the capture corresponding
            /// to the given number was matched.
            /// </summary>
            /// <param name="number">The number of the capture group that must be matched in order for the 'yes' alternative to be matched.</param>
            /// <param name="yes">The alternative to match if the capture group is matched.</param>
            /// <param name="no">The alternative to match if the capture group is not matched.</param>
            /// <returns>The alternation.</returns>
            public static ICaptureBasedConditionalAlternation ByCapture(int number, IExpression yes, IExpression no)
            {
                if (number < 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(number), number, Resources.InvalidBackreferenceNumber);
                }
                if (yes == null)
                {
                    throw new ArgumentNullException(nameof(yes));
                }
                var alternation = new CaptureBasedConditionalAlternation()
                {
                    Name = number.ToString(),
                    IsNamed = false,
                    YesOption = yes,
                    NoOption = no
                };
                return alternation;
            }

            /// <summary>
            /// Creates an alternation that tries to match the 'yes' option if the capture corresponding
            /// to the given name was matched.
            /// </summary>
            /// <param name="name">The name of the capture group that must be matched in order for the 'yes' alternative to be matched.</param>
            /// <param name="yes">The alternative to match if the capture group is matched.</param>
            /// <param name="no">The alternative to match if the capture group is not matched.</param>
            /// <returns>The alternation.</returns>
            public static ICaptureBasedConditionalAlternation ByCapture(string name, IExpression yes, IExpression no)
            {
                if (String.IsNullOrWhiteSpace(name))
                {
                    throw new ArgumentException(Resources.InvalidCaptureGroupName, nameof(name));
                }
                Group.ValidateCaptureGroupName(nameof(name), name);
                if (yes == null)
                {
                    throw new ArgumentNullException(nameof(yes));
                }
                var alternation = new CaptureBasedConditionalAlternation()
                {
                    Name = name,
                    IsNamed = true,
                    YesOption = yes,
                    NoOption = no
                };
                return alternation;
            }
        }

        private readonly List<IExpression> alternatives = new List<IExpression>();

        internal Alternation()
        {
        }

        public IEnumerable<IExpression> Alternatives => alternatives;

        public void Add(IExpression alterivative)
        {
            if (alterivative == null)
            {
                throw new ArgumentNullException(nameof(alterivative));
            }
            alternatives.Add(alterivative);
        }

        bool IExpression.NeedsGroupedToQuantify()
        {
            return alternatives.Count > 1 || alternatives[0].NeedsGroupedToQuantify();
        }

        string IExpressionEncoder.Encode(ExpressionContext context)
        {
            string encoded = String.Join("|", alternatives.Cast<IExpressionEncoder>().Select(e => e.Encode(ExpressionContext.Alternation)));
            return encoded;
        }

        public override string ToString() => ((IExpressionEncoder)this).Encode(ExpressionContext.Group);
    }
}
