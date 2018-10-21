﻿using System;
using System.Collections.Generic;

namespace RegexUp
{
    internal sealed class CaptureGroup : Group, ICaptureGroup
    {
        public string Name { get; set; }

        protected override string OnEncode()
        {
            var parts = new List<string>() { "(" };
            if (Name != null)
            {
                parts.Add("?<");
                parts.Add(Name);
                parts.Add(">");
            }
            parts.Add(EncodeMembers());
            parts.Add(")");
            var encoded = String.Join(String.Empty, parts);
            return encoded;
        }
    }
}