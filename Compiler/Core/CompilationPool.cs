using System.Collections.Generic;
using Compiler.Data;

namespace Compiler.Core
{
    public class CompilationPool
    {
        public List<Identifier> Identifiers { get; } = new List<Identifier>();

        public CompilationPool(string fileName, string code)
        {
            FileName = fileName;
            Code = code;
        }

        public ICollection<Token> Tokens { get; } = new List<Token>();
        public ICollection<string> Literals { get; } = new List<string>();
        public string Code { get; set; }
        public string FileName { get; set; }
        public int CodePosition { get; set; } = 0;
    }
}