﻿namespace RegexUp
{
    /// <summary>
    /// A sequence of characters to be matched verbatim.
    /// </summary>
    public interface ILiteral : IGroupMember, ICharacterGroupMember, IQuantifiable
    {
    }
}