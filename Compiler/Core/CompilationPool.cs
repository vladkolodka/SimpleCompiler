using System.Collections.Generic;
using Compiler.Data;

namespace Compiler.Core
{
    public class CompilationPool
    {
        public CompilationPool(string fileName, string code)
        {
            FileName = fileName;
            Code = code;

            Identifiers.AddRange(Constraints.Instance.Identifiers.CoreList);
        }

        public List<Identifier> Identifiers { get; } = new List<Identifier>();

        public List<Token> Tokens { get; } = new List<Token>();
        public string Code { get; set; }
        public string FileName { get; set; }
        public int CodePosition { get; set; } = 0;
    }
}