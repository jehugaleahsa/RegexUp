using System;
using System.Collections.Generic;

namespace RegexUp
{
    /// <summary>
    /// Defines a series of valid characters.
    /// </summary>
    public interface ICharacterGroup
    {
    }

    /// <summary>
    /// Defines a series a valid characters.
    /// </summary>
    public sealed class CharacterGroup : ICharacterGroup, IGroupMember, IQuantifiable, IExpression
    {
        /// <summary>
        /// Creates a new character group with the given sub-expressions.
        /// </summary>
        /// <param name="expressions">The sub-expressions to add to the character group.</param>
        /// <returns>The character group.</returns>
        public static CharacterGroup Of(params ICharacterGroupMember[] expressions)
        {
            return Of(false, expressions);
        }

        /// <summary>
        /// Creates a new character group with the given sub-expressions.
        /// </summary>
        /// <param name="isNegated">Indicates whether the character group should be negated.</param>
        /// <param name="expressions">The sub-expressions to add to the character group.</param>
        /// <returns>The character group.</returns>
        public static CharacterGroup Of(bool isNegated, params ICharacterGroupMember[] expressions)
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

        /// <summary>
        /// Initializes a new instance of CharacterGroup.
        /// </summary>
        public CharacterGroup()
        {
        }

        /// <summary>
        /// Gets or sets whether the the character group should be negated.
        /// If true, only characters not found in the group will match.
        /// If false, only characters found in the group will match.
        /// </summary>
        public bool IsNegated { get; set; }

        /// <summary>
        /// Add the literal, escaped characters or range to the character group.
        /// </summary>
        /// <param name="member">The literal, escape characters or range to add.</param>
        public void Add(ICharacterGroupMember member)
        {
            if (member == null)
            {
                throw new ArgumentNullException(nameof(member));
            }
            members.Add(member);
        }
        
        public string Encode(ExpressionContext context)
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

        public override string ToString() => Encode(ExpressionContext.Group);
    }
}
