using System;
using System.Collections.Generic;
using System.Linq;

namespace RegexUp
{
    /// <summary>
    /// Provides factory methods from creating a character group.
    /// </summary>
    public sealed class CharacterGroup : ICharacterGroup, IExpressionEncoder
    {
        /// <summary>
        /// Creates a new character group with the given sub-expressions.
        /// </summary>
        /// <param name="expressions">The sub-expressions to add to the character group.</param>
        /// <returns>The character group.</returns>
        public static ICharacterGroup Of(params ICharacterGroupMember[] expressions)
        {
            return From(null, expressions);
        }

        /// <summary>
        /// Creates a new character group with the given sub-expressions.
        /// </summary>
        /// <param name="isNegated">Indicates whether the character group should be negated.</param>
        /// <param name="expressions">The sub-expressions to add to the character group.</param>
        /// <returns>The character group.</returns>
        public static ICharacterGroup Of(CharacterGroupOptions options, params ICharacterGroupMember[] expressions)
        {
            return From(options, expressions);
        }

        /// <summary>
        /// Creates a new character group with the given sub-expressions.
        /// </summary>
        /// <param name="expressions">The sub-expressions to add to the character group.</param>
        /// <returns>The character group.</returns>
        public static ICharacterGroup From(IEnumerable<ICharacterGroupMember> expressions)
        {
            return From(null, expressions);
        }

        /// <summary>
        /// Creates a new character group with the given sub-expressions.
        /// </summary>
        /// <param name="isNegated">Indicates whether the character group should be negated.</param>
        /// <param name="expressions">The sub-expressions to add to the character group.</param>
        /// <returns>The character group.</returns>
        public static ICharacterGroup From(CharacterGroupOptions options, IEnumerable<ICharacterGroupMember> expressions)
        {
            if (expressions == null)
            {
                throw new ArgumentNullException(nameof(expressions));
            }
            var group = new CharacterGroup()
            {
                IsNegated = options?.IsNegated ?? false,
                Exclusions = options?.Exclusions
            };
            foreach (var expression in expressions)
            {
                group.Add(expression);
            }
            return group;
        }

        private readonly List<ICharacterGroupMember> members = new List<ICharacterGroupMember>();
        
        private CharacterGroup()
        {
        }

        public bool IsNegated { get; set; }

        public IEnumerable<ICharacterGroupMember> Members => members;

        public ICharacterGroup Exclusions { get; set; }
        
        public void Add(ICharacterGroupMember member)
        {
            if (member == null)
            {
                throw new ArgumentNullException(nameof(member));
            }
            members.Add(member);
        }

        bool IExpression.NeedsGroupedToQuantify() => false;

        string IExpressionEncoder.Encode(ExpressionContext context, int position, int length)
        {
            var parts = new List<string>() { "[" };
            if (IsNegated)
            {
                parts.Add("^");
            }
            parts.AddRange(members
                .Cast<IExpressionEncoder>()
                .Select((m, i) => m.Encode(ExpressionContext.CharacterGroup, i, members.Count))
            );
            if (Exclusions != null && Exclusions.Members.Any())
            {
                parts.Add("-");
                parts.Add(((IExpressionEncoder)Exclusions).Encode(ExpressionContext.CharacterGroup, 0, 1));
            }
            parts.Add("]");
            var encoded = String.Join(String.Empty, parts);
            return encoded;
        }

        public override string ToString() => ((IExpressionEncoder)this).Encode(ExpressionContext.Group, 0, 1);
    }
}
