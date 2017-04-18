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
            Idnetifiers.Add(new Identifier("terminal", -1));
            Idnetifiers.Add(new Identifier("fromFile", -2));
            Idnetifiers.Add(new Identifier("append", -2));
            Idnetifiers.Add(new Identifier("ReadSentences", -2));
            Idnetifiers.Add(new Identifier("where", -2));
            Idnetifiers.Add(new Identifier("firstLetter", -2));
            Idnetifiers.Add(new Identifier("isLowerCase", -2));
            Idnetifiers.Add(new Identifier("select", -2));
            Idnetifiers.Add(new Identifier("parentNode", -2));
            Idnetifiers.Add(new Identifier("count", -2));
            Idnetifiers.Add(new Identifier("out", -2));
            Idnetifiers.Add(new Identifier("in", -2));

        }

        public ICollection<Token> Tokens { get; } = new List<Token>();
        public ICollection<string> Literals { get; } = new List<string>();
        public ICollection<Identifier> Idnetifiers = new List<Identifier>();
        public string Code { get; set; }
        public string FileName { get; set; }
        public int CodePosition { get; set; } = 0;
    }
}