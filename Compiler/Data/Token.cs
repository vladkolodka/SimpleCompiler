using Compiler.Core;

namespace Compiler.Data
{
    public struct Token
    {
        public Token(TokenClass tokenClass, int id)
        {
            Class = tokenClass;
            Id = id;
        }

        public TokenClass Class { get; }
        public int Id { get; }
    }
}