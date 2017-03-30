using System.Collections.Generic;

namespace Compiler.Data
{
    public class State
    {
        public IDictionary<string, int> Transitions { get; } = new Dictionary<string, int>();
    }
}