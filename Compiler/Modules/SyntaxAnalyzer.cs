using System;
using System.Collections.Generic;
using System.Linq;
using Compiler.Core;
using Compiler.Data;

namespace Compiler.Modules
{
    internal class SyntaxAnalyzer : CompilerModuleBase
    {
        private CompilationPool _pool;

        public override bool TryBypass(CompilationPool compilationPool)
        {
            _pool = compilationPool;

            var table = Constraints.Instance.Tables.MainTable;
            var currentStateIndex = 0;
            var currentTokenIndex = 0;
            var stack = new Stack<int>();

            // 25
            do
            {
                var state = table.ElementAt(currentStateIndex);

                if (IsTokenExpected(table.ElementAt(currentStateIndex), currentTokenIndex))
                {
                    if(state.PushToStack.HasValue) stack.Push(state.PushToStack.Value);
                    if (state.IsAcceptRequired) currentTokenIndex++;

                    try
                    {
                        currentStateIndex = state.IsPopFromStackRequired ? stack.Pop() : state.TransisionStateNumber;

                    }
                    catch (InvalidOperationException e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }
                else if (state.IsErrorOccured)
                {
                    // TODO throw error
                    Errors.Add(
                        $"Syntax error : {_pool.Tokens[currentTokenIndex].Class}, {_pool.Tokens[currentTokenIndex].Id}, {_pool.Tokens[currentTokenIndex].Value}");
                    return false;
                }
                else currentStateIndex++;
            } while (currentStateIndex != 0);

            Messages.Add("Syntax analyzer: success.");
            /*            if (!compilationPool.Tokens.Any())
                            return false;

                        var parsingTable = Parser.ParseTransitionsTable(ParsingTables.MainParsnigTable);

                        var currentToken = 0;
                        var currentState = parsingTable.ElementAt(0);
                        var stack = new Stack<int>();
                        var tokens = compilationPool.Tokens;


                        while (currentState.Id != -1)
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

        private bool IsTokenExpected(ParsingTableState state, int tokenIndex)
        {
            if (state.IsNullable/* && state.ExpectedTokens.Any()*/) return true;

            var type = _pool.Tokens[tokenIndex].Class == TokenClass.Identifier
                ? _pool.Identifiers[_pool.Tokens[tokenIndex].Id].Type
                : _pool.Tokens[tokenIndex].Id;

            return state.ExpectedTokens.Any(pair => pair.Key == _pool.Tokens[tokenIndex].Class &&
                                                    pair.Value.Equals(type));
        }
    }
}