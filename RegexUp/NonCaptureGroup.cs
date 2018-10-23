﻿using System;
using System.Collections.Generic;

namespace RegexUp
{
    /// <summary>
    /// Provides factory methods for creating non-capture groups.
    /// </summary>
    public sealed class NonCaptureGroup : Group, INonCaptureGroup
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

        internal NonCaptureGroup()
        {
        }

        protected override string OnEncode()
        {
            var parts = new[] { "(?:", EncodeMembers(), ")" };
            var encoded = String.Join(String.Empty, parts);
            return encoded;
        }
    }
}
