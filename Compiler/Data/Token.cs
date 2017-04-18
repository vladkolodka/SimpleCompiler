using Compiler.Core;

namespace Compiler.Data
{
    public struct Token
    {
        public Token(TokenClass tokenClass, int id = 0, string value = "")
        {
            Value = value;
            Class = tokenClass;
            Id = id;
        }

        public string Value { get; }
        public TokenClass Class { get; }
        public int Id { get; }
    }
}