using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Core
{
    public static class IdentifierParser
    {
        
        public static bool IsIndentifier(CompilationPool pool)
        {
            var currentPosition = pool.CodePosition;
            var count = 1;

            while (StateMachine.HasNextSymbol(pool, false))
            {
                count++;
                pool.CodePosition++;
            }
            if (pool.Idnetifiers.Count != 0 &&
                pool.Idnetifiers.Where(s => s.Type!=null && s.Identity == pool.Code.Substring(currentPosition, pool.CodePosition - currentPosition + 1)).Count() != 0)
            {
                pool.Tokens.Add(new Data.Token(TokenClass.Identifier, 0, pool.Code.Substring(currentPosition, pool.CodePosition - currentPosition+1)));
                pool.CodePosition++;
                return true;
            }
            else
            {
                pool.Idnetifiers.Add(new Data.Identifier(pool.Code.Substring(currentPosition, pool.CodePosition - currentPosition+1)));
                pool.Tokens.Add(new Data.Token(TokenClass.Identifier, 0, pool.Code.Substring(currentPosition, pool.CodePosition - currentPosition + 1)));
                pool.CodePosition++;
                return true;
            }
        }
    }
}
