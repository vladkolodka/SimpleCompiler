using Compiler.Data;

namespace Compiler.Core
{
    public static class LiteralParser
    {
        public static bool IsLiteral(CompilationPool pool)
        {
            var currentPosition = pool.CodePosition;
            if (TryFindString(pool))
                return true;
            if (TryFindNumber(pool))
                return true;
            pool.CodePosition = currentPosition;
            return false;
        }

        public static bool TryFindString(CompilationPool pool)
        {
            if (pool.Code[pool.CodePosition] != '"') return false;

            var index = pool.Code.IndexOf('"', pool.CodePosition + 1);
            if (index == -1) return false;

            pool.Tokens.Add(new Token(TokenClass.Literal, 0,
                pool.Code.Substring(pool.CodePosition + 1, index - pool.CodePosition - 1)));
            pool.CodePosition = index + 1;
            return true;
        }

        public static bool TryFindNumber(CompilationPool pool)
        {
            if (!char.IsDigit(pool.Code[pool.CodePosition])) return false;

            var startIndex = pool.CodePosition;

            var isDouble = false;
            for (; pool.CodePosition < pool.Code.Length; pool.CodePosition++)
            {
                if (char.IsDigit(pool.Code[pool.CodePosition])) continue;
                if (pool.Code[pool.CodePosition] == '.' && isDouble == false)
                {
                    isDouble = true;
                    continue;
                }
                break;
            }
            pool.Tokens.Add(new Token(TokenClass.Literal, 0,
                pool.Code.Substring(startIndex, pool.CodePosition - startIndex)));

            return true;
        }
    }
}