using System.Collections.Generic;
using System.Linq;
using Compiler.Data;

namespace Compiler.Core
{
    public class StateMachine
    {
        private readonly bool _searchSymbol;
        private readonly ICollection<State> _states;
        private readonly TokenClass _tokenClass;

        public StateMachine(TokenClass tokenClass, ICollection<State> states,
            bool searchSymbol = false)
        {
            _tokenClass = tokenClass;
            _states = states;
            _searchSymbol = searchSymbol;
        }

        private bool TryFindToken(CompilationPool pool)
        {
            for (var stateNumber = 0;; pool.CodePosition++)
            {
                var state = _states.ElementAt(stateNumber);
                var symbol = pool.Code[pool.CodePosition];

                if (!HasNextSymbol(pool, _searchSymbol))
                {
                    if (state.Transitions.ContainsKey(symbol)) state = _states.ElementAt(state.Transitions[symbol]);
                    else return false;

                    // last chanse to determine token
                    if (!state.IsFinal) return false;

                    pool.Tokens.Add(new Token(_tokenClass, state.TokenNumber));
                    if (pool.Tokens.Count >= 3 && pool.Idnetifiers.Count != 0)
                    {
                        if (pool.Tokens.ElementAt(pool.Tokens.Count - 2).Class == TokenClass.ReservedWord &&
                            pool.Tokens.ElementAt(pool.Tokens.Count - 2).Id == 2)
                        {
                            pool.Idnetifiers.Last().Type = state.TokenNumber;                            
                        }
                    }
                    pool.CodePosition++;
                    return true;
                }
                if (!state.Transitions.ContainsKey(symbol)) return false;
                // go to next state
                stateNumber = state.Transitions[symbol];
            }
        }

        public bool FindToken(CompilationPool pool)
        {
            var currentPosition = pool.CodePosition;

            if (TryFindToken(pool)) return true;

            // if token not found, restore code-cursor position
            pool.CodePosition = currentPosition;
            return false;
        }

        public static bool HasNextSymbol(CompilationPool pool, bool searchSymbol)
        {
            if (pool.CodePosition + 1 == pool.Code.Length) return false;

            var next = pool.Code[pool.CodePosition + 1];
            if (next == 32 || next == '\r' || next == '\n') return false;

//            if (pool.Code[pool.CodePosition + 1] == 32) return false;

            // is current symbol belongs to token-class alphabet
            return searchSymbol
                ? Constraints.Instance.Tokens.OperationSigns.Contains(pool.Code[pool.CodePosition + 1].ToString())
                : !Constraints.Instance.Tokens.OperationSigns.Contains(pool.Code[pool.CodePosition + 1].ToString());
        }
    }
}