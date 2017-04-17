using System.Collections.Generic;
using Compiler.Util;

namespace Compiler.Data
{
    public class Constraints
    {
        private static Constraints _instance;
        public StateMachineConstraint StateMachines { get; } = new StateMachineConstraint();
        public TokenConstraint Tokens { get; } = new TokenConstraint();


        public static Constraints Instance => _instance ?? (_instance = new Constraints());

        public class StateMachineConstraint
        {
            public ICollection<State> OperationSigns { get; } =
                Parser.ParseStateMachine(Resources.StateMachines.OperationSigns);

            public ICollection<State> ReservedWords { get; } =
                Parser.ParseStateMachine(Resources.StateMachines.ReservedWords);
        }

        public class TokenConstraint
        {
            public ICollection<string> OperationSigns { get; } = Parser.ParseTokens(Resources.Tokens.OperationSigns);
            public ICollection<string> ReservedWords { get; } = Parser.ParseTokens(Resources.Tokens.ReservedWords);
        }
    }
}