﻿using System.Collections.Generic;
using Compiler.Data;

namespace Compiler.Core
{
    public class CompilationPool
    {
        public CompilationPool(string code)
        {
            Code = code;
        }

        public ICollection<Token> Tokens { get; } = new List<Token>();

        public string Code { get; set; }
        public int CodePosition { get; set; } = 0;
    }
}