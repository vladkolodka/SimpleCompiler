namespace Compiler.Data
{
    public class Identifier
    {
        public Identifier(string identity, int type = 0, int scope = 0)
        {
            Type = type;
            Identity = identity;
            ScopeLevel = scope;
        }

        public int Type { get; set; }
        public string Identity { get; }
        public int ScopeLevel { get; set; }

        public override bool Equals(object obj)
        {
            var identifier2 = obj as Identifier;
            if (identifier2 == null) return false;

            return Type.Equals(identifier2.Type) && Identity.Equals(identifier2.Identity) &&
                   ScopeLevel.Equals(identifier2.ScopeLevel);
        }

        public override int GetHashCode()
        {
            return Identity.GetHashCode();
        }
    }
}