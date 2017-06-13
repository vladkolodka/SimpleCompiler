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
                Console.WriteLine(currentStateIndex);
                var state = table.ElementAt(currentStateIndex);

                if (IsTokenExpected(table.ElementAt(currentStateIndex), currentTokenIndex))
                {
                    if (state.PushToStack.HasValue) stack.Push(state.PushToStack.Value);

                    if (state.TransisionStateNumber == -1 && state.IsPopFromStackRequired == false)
                    {
                        Messages.Add(string.Format(Resources.Messages.ParsingSuccessful, _pool.FileName));

                        return true;
                    }

                    if (state.IsAcceptRequired) currentTokenIndex++;

                    try
                    {
                        currentStateIndex = state.IsPopFromStackRequired ? stack.Pop() : state.TransisionStateNumber;
                    }
                    catch (InvalidOperationException e)
                    {
                        // TODO
                        Errors.Add("Pop from stack");
                        return false;
                    }
                }
                else if (state.IsErrorOccured)
                {
                    // TODO throw error
                            Errors.Add(
                        $"Syntax error : {_pool.Tokens[currentTokenIndex].Class}, {_pool.Tokens[currentTokenIndex].Id}, {_pool.Tokens[currentTokenIndex].Value} : {currentTokenIndex}. Expected: {string.Join("\n", state.ExpectedTokens.Select(pair => $"{pair.Key} : {pair.Value}"))}");
                    return false;
                }
                else currentStateIndex++;
            } while (currentStateIndex != -1);

            Messages.Add("Syntax analyzer: success.");

            return true;
        }

        private bool IsTokenExpected(ParsingTableState state, int tokenIndex)
        {
            if (state.IsNullable) return true;

            if (tokenIndex >= _pool.Tokens.Count) return true;

            var type = _pool.Tokens[tokenIndex].Class == TokenClass.Identifier
                ? _pool.Identifiers[_pool.Tokens[tokenIndex].Id].Type
                : _pool.Tokens[tokenIndex].Id;

            return state.ExpectedTokens.Any(pair => pair.Key == _pool.Tokens[tokenIndex].Class &&
                                                    pair.Value.Equals(type));
        }
    }
}