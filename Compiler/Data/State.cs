using System.Collections.Generic;

namespace Compiler.Data
{
    public class State
    {
        public State(int tokenNumber)
        {
            TokenNumber = tokenNumber;
        }

        public int TokenNumber { get; }
        public bool IsFinal => TokenNumber != 0;
        public IDictionary<char, int> Transitions { get; } = new Dictionary<char, int>();
    }
}