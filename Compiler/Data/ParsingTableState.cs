using Compiler.Core;

namespace Compiler.Data
{
    public class ParsingTableState
    {
        // Id
        public int TransitionState { get; set; }
        public TokenClass Class { get; set; }
        public int NumberInClass { get; set; }
        public int TransisionStateNumber { get; set; }
        public bool IsAcceptRequired { get; set; }
        public int? PushToStack { get; set; }
        public bool IsPopFromStackRequired { get; set; }
        public bool IsErrorOccured { get; set; }
        public bool IsNullable { get; set; }
    }
}