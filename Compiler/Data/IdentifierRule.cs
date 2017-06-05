using System;
using System.Collections.Generic;
using Compiler.Core;

namespace Compiler.Data
{
    public class IdentifierRule
    {
        public int IdentifierType { get; set; }
        public List<Tuple<int, TokenClass, int>> Rules { get; } = new List<Tuple<int, TokenClass, int>>();
    }
}