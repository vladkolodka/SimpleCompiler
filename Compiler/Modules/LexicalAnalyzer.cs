using System.Collections.Generic;
using System.Linq;
using Compiler.Core;
using Compiler.Data;

namespace Compiler.Modules
{
    public class LexicalAnalyzer : CompilerModuleBase
    {
        private readonly List<StateMachine> _stateMachines = new List<StateMachine>
        {
            new StateMachine(TokenClass.OperationSign, Constraints.Instance.StateMachines.OperationSigns, true),
            new StateMachine(TokenClass.ReservedWord, Constraints.Instance.StateMachines.ReservedWords)
        };

        public override bool TryBypass(CompilationPool compilationPool)
        {
            var line = 1;
            while (compilationPool.CodePosition < compilationPool.Code.Length)
            {
                if (Constraints.Instance.Tokens.Delmers.Contains(compilationPool.Code[compilationPool.CodePosition]))
                {
                    if (compilationPool.Code[compilationPool.CodePosition] == '\n') line++;
                    compilationPool.CodePosition++;
                    continue;
                }

                if (_stateMachines.Any(stateMachine => stateMachine.FindToken(compilationPool)))
                {
                    TokenFounded(compilationPool.Tokens.Last(), compilationPool.FileName, line);
                    continue;
                }

                if (LiteralParser.IsLiteral(compilationPool))
                {
                    TokenFounded(compilationPool.Tokens.Last(), compilationPool.FileName, line);
                    continue;
                }

                if (IdentifierParser.IsIndentifier(compilationPool))
                {
                    TokenFounded(compilationPool.Tokens.Last(), compilationPool.FileName, line);
                    continue;
                }


                Errors.Add(string.Format(Resources.Messages.UnexpectedSymbol, compilationPool.FileName,
                    GetNextPartOfLexem(compilationPool), line));
                return false;
            }
            foreach (var id in compilationPool.Idnetifiers)
            {
                Messages.Add(string.Format(Resources.Messages.IndentifierFounded,
                    compilationPool.FileName,
                    id.Type,
                    id.Identity));
                if (id.Type != null) continue;

//                Errors.Add(string.Format(Resources.Messages.IdentyfierNotDefined, compilationPool.FileName, id.Identity));
//                return false;
            }
            return true;
        }

        public static string GetNextPartOfLexem(CompilationPool compilationPool)
        {
            var isSymbol =
                Constraints.Instance.Tokens.OperationSigns.Contains(
                    compilationPool.Code[compilationPool.CodePosition].ToString());

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

        private void TokenFounded(Token token, string fileName, int line)
        {
            string tokenValue;

            switch (token.Class)
            {
                case TokenClass.OperationSign:
                    tokenValue = Constraints.Instance.Tokens.OperationSigns.ElementAt(token.Id);
                    break;
                case TokenClass.ReservedWord:
                    tokenValue = Constraints.Instance.Tokens.ReservedWords.ElementAt(token.Id);
                    break;
                case TokenClass.Identifier:
                    tokenValue = token.Value;
                    break;
                case TokenClass.Literal:
                    tokenValue = token.Value;
                    break;
                default:
                    tokenValue = "_UNDEFINED_TYPE_";
                    break;
            }
            Messages.Add(string.Format(Resources.Messages.TokenFounded, fileName, token.Class, token.Id, line,
                tokenValue));
        }
    }
}