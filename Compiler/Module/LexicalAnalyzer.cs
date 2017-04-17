using System;
using System.Collections.Generic;
using System.Linq;
using Compiler.Core;
using Compiler.Data;
using Compiler.Resources;

namespace Compiler.Module
{
    public class LexicalAnalyzer : CompilerModuleBase
    {
        private readonly List<StateMachine> _stateMachines = new List<StateMachine>
        {
            new StateMachine(TokenClass.OperationSign, Constraints.Instance.StateMachines.OperationSigns, true),
            new StateMachine(TokenClass.ReservedWord, Constraints.Instance.StateMachines.ReservedWords)
        };

        public event Action<Token, int> TokenFounded;

        public override bool TryBypass(CompilationPool compilationPool)
        {
            var line = 1;
            while (compilationPool.CodePosition < compilationPool.Code.Length)
            {
                if (compilationPool.Code[compilationPool.CodePosition] == 32)
                {
                    // skip space
                    compilationPool.CodePosition++;
                    continue;
                }

                if (compilationPool.Code[compilationPool.CodePosition] == '\n')
                {
                    // linux-style new line
                    compilationPool.CodePosition++;
                    line++;
                    continue;
                }

                if (compilationPool.CodePosition + 1 < compilationPool.Code.Length &&
                    compilationPool.Code[compilationPool.CodePosition] == '\r' &&
                    compilationPool.Code[compilationPool.CodePosition + 1] == '\n')
                {
                    // windows-style new line
                    compilationPool.CodePosition += 2;
                    line++;
                    continue;
                }

                if (_stateMachines.Any(stateMachine => stateMachine.FindToken(compilationPool)))
                {
                    // token founded
                    TokenFounded?.Invoke(compilationPool.Tokens.Last(), line);
                    continue;
                }

                // token not found, trying to find identifier
                // if found, continue

                // try determine literal
                // if found, continue

                Console.WriteLine(Messages.UnexpectedSymbol, GetNextPartOfLexem(compilationPool), line);
                return false;
            }
            return true;
        }

        private string GetNextPartOfLexem(CompilationPool compilationPool) {
            var isSymbol = Constraints.Instance.Tokens.OperationSigns.Contains(compilationPool.Code[compilationPool.CodePosition].ToString());

            var codePositionBackup = compilationPool.CodePosition;
            var count = 1;

            while (StateMachine.HasNextSymbol(compilationPool, isSymbol))
            {
                count++;
                compilationPool.CodePosition++;
            }

            var str = compilationPool.Code.Substring(codePositionBackup, count);

            compilationPool.CodePosition = codePositionBackup;
            return str;
        }

    }
}