using System.Collections.Generic;
using Compiler.Core;

namespace Compiler.Data
{
    public class ParsingTableState
    {
        // Id
        public int TransitionState { get; set; }

        public ICollection<KeyValuePair<TokenClass, int>> ExpectedTokens { get; set; } =
            new List<KeyValuePair<TokenClass, int>>();

        public int TransisionStateNumber { get; set; }
        public bool IsAcceptRequired { get; set; }
        public int? PushToStack { get; set; }
        public bool IsPopFromStackRequired { get; set; }
        public bool IsErrorOccured { get; set; }
        public bool IsNullable { get; set; }
    }
}