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

        public override bool Equals(object obj)
        {
            if (!(obj is Token)) return false;

            var token2 = (Token) obj;

            return Class == token2.Class && Id == token2.Id;
        }

        public bool Equals(TokenClass @class, int id)
        {
            return @class.Equals(Class) && id.Equals(Id);
        }

        public override int GetHashCode()
        {
            return Class.GetHashCode() + Id.GetHashCode();
        }
    }
}