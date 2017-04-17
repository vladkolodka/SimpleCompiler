using System;
using System.Collections.Generic;
using System.Linq;
using Compiler.Data;

namespace Compiler.Util
{
    public static class Parser
    {
        public static ICollection<State> ParseStateMachine(string text)
        {
            var stateCollection = new List<State>();

            foreach (var line in text.Replace("\r", "").Split('\n'))
            {
                var blocks = line.Split(' ');

                // transition declaration
                if (blocks[0].Equals(string.Empty))
                    stateCollection.Last().Transitions.Add(blocks[1][0], Convert.ToInt32(blocks[2]));
                // state declaration
                else
                    stateCollection.Add(new State(blocks.Length == 3 ? Convert.ToInt32(blocks[2]) - 1 : -1));
            }

            return stateCollection;
        }

        public static ICollection<string> ParseTokens(string text)
        {
            return text.Replace("\r", "").Split('\n');
        }
    }
}