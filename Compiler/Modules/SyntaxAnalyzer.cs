using System.Collections.Generic;
using System.Linq;
using Compiler.Core;
using Compiler.Resources;
using Compiler.Util;

namespace Compiler.Modules
{
    internal class SyntaxAnalyzer : CompilerModuleBase
    {
        public override bool TryBypass(CompilationPool compilationPool)
        {
            if (!compilationPool.Tokens.Any())
                return false;

            var parsingTable = Parser.ParseTransitionsTable(ParsingTables.MainParsnigTable);

            var currentToken = 0;
            var currentState = parsingTable.ElementAt(0);
            var stack = new Stack<int>();
            var tokens = compilationPool.Tokens;

/*
            while (currentState.TransitionState != -1)
            {
                var token = tokens.ElementAt(currentToken);

                if (token.Equals(currentState.Class, currentState.NumberInClass) ||
                    token.Class == TokenClass.Identifier && currentState.Class == TokenClass.Identifier ||
                    token.Class == TokenClass.Literal && currentState.Class == TokenClass.Literal)
                {
                    if (currentState.IsAcceptRequired) currentToken++;

                    if(currentState.PushToStack.HasValue) stack.Push(currentState.PushToStack.Value);
                    else if (currentState.IsPopFromStackRequired)
                    {
                        currentState = parsingTable.ElementAt(stack.Pop() - 1);
                        continue;
                    }

                    if(currentState.TransisionStateNumber == -1) continue;
                    currentState = parsingTable.ElementAt(currentState.TransisionStateNumber - 1);
                } else if (currentState.IsErrorOccured)
                {
                    // TODO add error message
                    return false;
                }
            }
*/

            return true;
        }
    }
}