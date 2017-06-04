using System.Linq;
using Compiler.Data;
using Compiler.Modules;

namespace Compiler.Core
{
    public static class IdentifierParser
    {
        public static bool IsIndentifier(CompilationPool pool)
        {
            var identifierName = LexicalAnalyzer.GetNextPartOfLexem(pool);
            pool.CodePosition += identifierName.Length;

            if (pool.Idnetifiers.Any(s => s.Type != null && s.Identity == identifierName) ||
                Constraints.Instance.Identifiers.Core.Any(identifier => identifier.Identity.Equals(identifierName)))
            {
                pool.Tokens.Add(new Token(TokenClass.Identifier, -1, identifierName));
                return true;
            }

            pool.Idnetifiers.Add(new Identifier(identifierName));
            pool.Tokens.Add(new Token(TokenClass.Identifier, -1, identifierName));

            return true;
        }
    }
}