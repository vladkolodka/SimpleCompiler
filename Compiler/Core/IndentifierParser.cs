using System.Linq;
using Compiler.Data;

namespace Compiler.Core
{
    public static class IdentifierParser
    {
        // TODO scopes
        private const int Scope = 0;

        public static bool IsIndentifier(CompilationPool pool)
        {
            var identifierName = GetIdentifierName(pool);
            if (identifierName.Length == 0) return false;

            pool.CodePosition += identifierName.Length;

            var existingIdenfitier =
                pool.Identifiers.FirstOrDefault(
                    identifier => identifier.Identity.Equals(identifierName) && identifier.ScopeLevel.Equals(Scope));

            if (existingIdenfitier == null) pool.Identifiers.Add(new Identifier(identifierName));

            pool.Tokens.Add(new Token(TokenClass.Identifier,
                existingIdenfitier == null
                    ? pool.Identifiers.Count - 1
                    : pool.Identifiers.IndexOf(existingIdenfitier), identifierName));

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