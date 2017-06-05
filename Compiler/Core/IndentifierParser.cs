using System.Linq;
using Compiler.Data;

namespace Compiler.Core
{
    public static class IdentifierParser
    {
        public static bool IsIndentifier(CompilationPool pool)
        {
            var identifierName = GetIdentifierName(pool);
            if (identifierName.Length == 0) return false;

            pool.CodePosition += identifierName.Length;

            if (pool.Identifiers.Any(s => s.Type != null && s.Identity == identifierName) ||
                Constraints.Instance.Identifiers.Core.Any(identifier => identifier.Identity.Equals(identifierName)))
            {
                pool.Tokens.Add(new Token(TokenClass.Identifier, -1, identifierName));
                return true;
            }

            pool.Identifiers.Add(new Identifier(identifierName));
            pool.Tokens.Add(new Token(TokenClass.Identifier, -1, identifierName));

            return true;
        }

        private static string GetIdentifierName(CompilationPool pool)
        {
            var position = pool.CodePosition;
            if (!char.IsLetterOrDigit(pool.Code[position])) return "";

            while (char.IsLetterOrDigit(pool.Code[position]) ||
                   pool.Code[position].Equals('_') && position < pool.Code.Length)
                position++;

            return pool.Code.Substring(pool.CodePosition, position - pool.CodePosition);
        }
    }
}