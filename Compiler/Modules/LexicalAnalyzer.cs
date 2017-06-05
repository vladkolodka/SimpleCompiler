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
            new StateMachine(TokenClass.Delmer, Constraints.Instance.StateMachines.Delmers),
            new StateMachine(TokenClass.OperationSign, Constraints.Instance.StateMachines.OperationSigns),
            new StateMachine(TokenClass.ReservedWord, Constraints.Instance.StateMachines.ReservedWords)
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

            DetermineIdentifierTypes(compilationPool);

            compilationPool.Identifiers.ForEach(identifier =>
            {
                Messages.Add(string.Format(Resources.Messages.IndentifierFounded,
                    compilationPool.FileName,
                    identifier.Type,
                    identifier.Identity));
            });

            return true;
        }

        private void DetermineIdentifierTypes(CompilationPool compilationPool)
        {
            var rules = Constraints.Instance.Identifiers.CoreRules;

            var idenfitierTokens = compilationPool.Tokens
                .Where(token => token.Class == TokenClass.Identifier && compilationPool.Identifiers[token.Id].Type == 0)
                .GroupBy(token => token.Id)
                .Select(tokens => tokens.First())
                .Select(token => new {Index = compilationPool.Tokens.IndexOf(token), Token = token}).ToList();

            idenfitierTokens.ForEach(data =>
            {
                foreach (var identifierRule in rules)
                {
                    var isValid = true;
                    foreach (var rule in identifierRule.Rules)
                    {
                        var index = data.Index + rule.Item1;
                        if (index < 0 || index >= compilationPool.Tokens.Count) continue;

                        var token = compilationPool.Tokens[index];
                        if (token.Class != rule.Item2 || token.Id != rule.Item3) isValid = false;
                    }
                    if (!isValid) continue;

                    compilationPool.Identifiers[data.Token.Id].Type = identifierRule.IdentifierType;
                    break;
                }
            });
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