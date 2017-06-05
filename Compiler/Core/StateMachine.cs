using System.Collections.Generic;
using System.Linq;
using Compiler.Data;

namespace Compiler.Core
{
    public class StateMachine
    {
        private readonly ICollection<State> _states;
        private readonly TokenClass _tokenClass;

        public StateMachine(TokenClass tokenClass, ICollection<State> states)
        {
            _tokenClass = tokenClass;
            _states = states;
        }

        private bool TryFindToken(CompilationPool pool)
        {
            for (var stateNumber = 0;; pool.CodePosition++)
            {
                var state = _states.ElementAt(stateNumber);
                var symbol = pool.Code[pool.CodePosition];

                if (!HasNextSymbol(pool, _tokenClass))
                {
                    if (state.Transitions.ContainsKey(symbol)) state = _states.ElementAt(state.Transitions[symbol]);
                    else return false;

                    // last chanсe to determine token
                    if (!state.IsFinal) return false;

                    pool.Tokens.Add(new Token(_tokenClass, state.TokenNumber));

                    pool.CodePosition++;
                    return true;
                }

                var subStateNumber = state.Transitions.Where(pair => pair.Key.Equals(symbol)).Select(pair => pair.Value)
                    .FirstOrDefault();

                if (subStateNumber != 0)
                {
                    var subState = _states.ElementAt(subStateNumber);

                    if (!subState.Transitions.Any() && subState.IsFinal)
                    {
                        pool.Tokens.Add(new Token(_tokenClass, subState.TokenNumber));
                        pool.CodePosition++;
                        return true;
                    }
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

        public static bool HasNextSymbol(CompilationPool pool, TokenClass tokenClass)
        {
            if (pool.CodePosition + 1 == pool.Code.Length) return false;

            var symbol = pool.Code[pool.CodePosition + 1].ToString();

            if (Constraints.Instance.Tokens.SkippedSymbols.Contains(symbol[0])) return false;

            if (Constraints.Instance.Borders.ForSpecialChars.Contains(symbol)) return false;

            var result = Constraints.Instance.Borders.ForTokenClasses[tokenClass].Contains(symbol);

            return result;
        }

        public static TokenClass? DetermineTokenClass(string symbol)
        {
            foreach (var tokenBorders in Constraints.Instance.Borders.ForTokenClasses)
                if (tokenBorders.Value.Contains(symbol)) return tokenBorders.Key;

            return null;
        }
    }
}