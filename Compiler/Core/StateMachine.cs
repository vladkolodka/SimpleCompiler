using System.Collections.Generic;
using Compiler.Data;

// TODO states parser

namespace Compiler.Core
{
    public class StateMachine
    {
        private readonly ICollection<State> _states;

        public StateMachine(ICollection<State> states)
        {
            _states = states;
        }
    }
}