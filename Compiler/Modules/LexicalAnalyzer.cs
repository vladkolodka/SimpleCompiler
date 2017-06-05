using System;
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
            new StateMachine(TokenClass.OperationSign, Constraints.Instance.StateMachines.OperationSigns),
            new StateMachine(TokenClass.ReservedWord, Constraints.Instance.StateMachines.ReservedWords),
            new StateMachine(TokenClass.Delmer, Constraints.Instance.StateMachines.Delmers)
        };

        public override bool TryBypass(CompilationPool compilationPool)
        {
            var line = 1;
            while (compilationPool.CodePosition < compilationPool.Code.Length)
            {
                if (Constraints.Instance.Tokens.SkippedSymbols.Contains(
                    compilationPool.Code[compilationPool.CodePosition]))
                {
                    if (compilationPool.Code[compilationPool.CodePosition] == '\n') line++;
                    compilationPool.CodePosition++;
                    continue;
                }

                if (_stateMachines.Any(stateMachine => stateMachine.FindToken(compilationPool)))
                {
                    OnTokenFounded(compilationPool.Tokens.Last(), compilationPool.FileName, line);
                    continue;
                }

                if (LiteralParser.IsLiteral(compilationPool))
                {
                    OnTokenFounded(compilationPool.Tokens.Last(), compilationPool.FileName, line);
                    continue;
                }

                if (IdentifierParser.IsIndentifier(compilationPool))
                {
                    OnTokenFounded(compilationPool.Tokens.Last(), compilationPool.FileName, line);
                    continue;
                }


                Errors.Add(string.Format(Resources.Messages.UnexpectedSymbol, compilationPool.FileName,
                    GetNextPartOfLexem(compilationPool), line));
                return false;
            }

            compilationPool.Identifiers.ForEach(identifier =>
            {
                if (!identifier.Type.HasValue) identifier.Type = 1;
            });

            var identifiers = compilationPool.Identifiers.Where(identifier => char.IsLetter(identifier.Identity[0]))
                .ToList();

            compilationPool.Identifiers.Clear();
            compilationPool.Identifiers.AddRange(identifiers);

            compilationPool.Identifiers.ForEach(identifier =>
            {
                Messages.Add(string.Format(Resources.Messages.IndentifierFounded,
                    compilationPool.FileName,
                    identifier.Type,
                    identifier.Identity));
            });

            return true;
        }

        public static string GetNextPartOfLexem(CompilationPool compilationPool)
        {
            var tokenClass =
                StateMachine.DetermineTokenClass(compilationPool.Code[compilationPool.CodePosition].ToString());

            if (!tokenClass.HasValue) throw new Exception("Token class not determined.");

            var codePositionBackup = compilationPool.CodePosition;
            var count = 1;

            while (StateMachine.HasNextSymbol(compilationPool, tokenClass.Value))
            {
                count++;
                compilationPool.CodePosition++;
            }

            var str = compilationPool.Code.Substring(codePositionBackup, count);

            compilationPool.CodePosition = codePositionBackup;
            return str;
        }

        private void OnTokenFounded(Token token, string fileName, int line)
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
                case TokenClass.Delmer:
                    tokenValue = Constraints.Instance.Tokens.Delmers.ElementAt(token.Id);
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