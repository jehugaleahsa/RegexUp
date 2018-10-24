using System.Collections.Generic;

namespace RegexUp
{
    /// <summary>
    /// Matches any single character in the group.
    /// </summary>
    public interface ICharacterGroup : ICharacterClass
    {
        /// <summary>
        /// Gets or sets whether the the character group should be negated.
        /// If true, only characters not found in the group will match.
        /// If false, only characters found in the group will match.
        /// </summary>
        bool IsNegated { get; set; }

        /// <summary>
        /// Gets the members of the character group.
        /// </summary>
        IEnumerable<ICharacterGroupMember> Members { get; }


        /// <summary>
        /// Gets the character group representing the subset of characters to exclude -or null.
        /// </summary>
        ICharacterGroup Exclusions { get; set; }

        /// <summary>
        /// Add the literal, escaped characters or range to the character group.
        /// </summary>
        /// <param name="member">The literal, escape characters or range to add.</param>
        void Add(ICharacterGroupMember member);
    }
}
