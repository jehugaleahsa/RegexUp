﻿using System.Collections.Generic;

namespace RegexUp
{
    /// <summary>
    /// A group delineates subexpressions of a regular expression and can capture the substrings of an input string.
    /// </summary>
    public interface IGroup : IExpression
    {
        /// <summary>
        /// Gets the members within the group.
        /// </summary>
        IEnumerable<IExpression> Members { get; }
    }
}
