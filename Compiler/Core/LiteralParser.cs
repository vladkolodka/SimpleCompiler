using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiler.Data;

namespace Compiler.Core
{
    public static class LiteralParser
    {
        public static bool IsLiteral(CompilationPool pool)
        {
            var currentPosition = pool.CodePosition;
            if (IsString(pool))
            {
                return true;
            }
            if (IsNumber(pool))
            {
                return true;
            }
            pool.CodePosition = currentPosition;
            return false;
        }

        public static bool IsString(CompilationPool pool)
        {
            if (pool.Code[pool.CodePosition] == '"')
            {

                int index = pool.Code.IndexOf('"', pool.CodePosition + 1);
                if (index != -1)
                {
                    pool.Tokens.Add(new Data.Token(TokenClass.Literal, 0, pool.Code.Substring(pool.CodePosition+1, index - pool.CodePosition-1)));
                    pool.CodePosition = index+1;
                    return true;
                }
                return false;
            }
            return false;
        }

        public static bool IsNumber(CompilationPool pool)
        {
            int tempIndex = pool.CodePosition;
            if (pool.Code[tempIndex] >= 48 && 57 <= pool.Code[tempIndex])
            {
                int temp;
                while (Int32.TryParse(pool.Code[tempIndex].ToString(),out temp))
                {
                    tempIndex++;
                }
                if (Constraints.Instance.Tokens.OperationSigns.Contains(
                    pool.Code[tempIndex].ToString()) == true || pool.Code[tempIndex] == 32)
                {
                    pool.Tokens.Add(new Data.Token(TokenClass.Literal, 0, pool.Code.Substring(pool.CodePosition, tempIndex - pool.CodePosition)));
                    pool.CodePosition = tempIndex;
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}
