namespace Compiler.Data
{
    public class Identifier
    {
        public Identifier(string identity, int? type = null)
        {
            Type = type;
            Identity = identity;
        }

        public int? Type { get; set; }
        public string Identity { get; }
    }
}