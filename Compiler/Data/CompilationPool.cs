using System.Collections.Generic;

namespace Compiler.Data
{
    public class CompilationPool
    {
        public ICollection<Token> Tokens { get; } = new List<Token>();

        public CompilationPool(string code)
        {
            Code = code;
        }

        public string Code { get; set; }

        // TODO
    }
}