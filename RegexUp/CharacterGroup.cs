using System;
using System.Collections.Generic;

namespace RegexUp
{
    /// <summary>
    /// Provides factory methods from creating a character group.
    /// </summary>
    public sealed class CharacterGroup : ICharacterGroup, IExpression
    {
        /// <summary>
        /// Creates a new character group with the given sub-expressions.
        /// </summary>
        /// <param name="expressions">The sub-expressions to add to the character group.</param>
        /// <returns>The character group.</returns>
        public static ICharacterGroup Of(params ICharacterGroupMember[] expressions)
        {
            return From(false, expressions);
        }

        /// <summary>
        /// Creates a new character group with the given sub-expressions.
        /// </summary>
        /// <param name="isNegated">Indicates whether the character group should be negated.</param>
        /// <param name="expressions">The sub-expressions to add to the character group.</param>
        /// <returns>The character group.</returns>
        public static ICharacterGroup Of(bool isNegated, params ICharacterGroupMember[] expressions)
        {
            return From(isNegated, expressions);
        }

        /// <summary>
        /// Creates a new character group with the given sub-expressions.
        /// </summary>
        /// <param name="expressions">The sub-expressions to add to the character group.</param>
        /// <returns>The character group.</returns>
        public static ICharacterGroup From(IEnumerable<ICharacterGroupMember> expressions)
        {
            return From(false, expressions);
        }

        /// <summary>
        /// Creates a new character group with the given sub-expressions.
        /// </summary>
        /// <param name="isNegated">Indicates whether the character group should be negated.</param>
        /// <param name="expressions">The sub-expressions to add to the character group.</param>
        /// <returns>The character group.</returns>
        public static ICharacterGroup From(bool isNegated, IEnumerable<ICharacterGroupMember> expressions)
        {
            if (expressions == null)
            {
                throw new ArgumentNullException(nameof(expressions));
            }
            var group = new CharacterGroup() { IsNegated = isNegated };
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
        
        public void Add(ICharacterGroupMember member)
        {
            if (member == null)
            {
                throw new ArgumentNullException(nameof(member));
            }
            members.Add(member);
        }
        
        string IExpression.Encode(ExpressionContext context)
        {
            if (members.Count == 0)
            {
                return String.Empty;
            }
            var parts = new List<string>() { "[" };
            if (IsNegated)
            {
                parts.Add("^");
            }
            for (int memberIndex = 0; memberIndex != members.Count; ++memberIndex)
            {
                var member = members[memberIndex];
                parts.Add(((IExpression)member).Encode(ExpressionContext.CharacterGroup));
            }
            parts.Add("]");
            var encoded = String.Join(String.Empty, parts);
            return encoded;
        }

        public override string ToString() => ((IExpression)this).Encode(ExpressionContext.Group);
    }
}
