using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using Compiler.Core;
using Compiler.Util;

namespace Compiler.Data
{
    public class Constraints
    {
        private static Constraints _instance;
        public StateMachineConstraint StateMachines { get; } = new StateMachineConstraint();
        public TokenConstraint Tokens { get; } = new TokenConstraint();
        public IdentifierConstraints Identifiers { get; } = new IdentifierConstraints();
        public TokenBorders Borders { get; }


        public Constraints()
        {
            Borders = new TokenBorders(new Dictionary<TokenClass, ICollection<string>>
            {
                {TokenClass.ReservedWord, Tokens.ReservedWords},
                {TokenClass.OperationSign, Tokens.OperationSigns},
                {TokenClass.Delmer, Tokens.Delmers}
            });
        }

        public static Constraints Instance => _instance ?? (_instance = new Constraints());

        public class StateMachineConstraint
        {
            public ICollection<State> OperationSigns { get; } =
                Parser.ParseStateMachine(Resources.StateMachines.OperationSigns);

            public ICollection<State> ReservedWords { get; } =
                Parser.ParseStateMachine(Resources.StateMachines.ReservedWords);

            public ICollection<State> Delmers { get; } =
                Parser.ParseStateMachine(Resources.StateMachines.Delmers);
        }

        public class TokenConstraint
        {
            public ICollection<string> OperationSigns { get; } = Parser.ParseTokens(Resources.Tokens.OperationSigns);
            public ICollection<string> ReservedWords { get; } = Parser.ParseTokens(Resources.Tokens.ReservedWords);
            public ICollection<string> Delmers { get; } = Parser.ParseTokens(Resources.Tokens.Delmers);
            public ICollection<char> SkippedSymbols { get; } = new List<char> {' ', '\r', '\n'};
        }

        public class IdentifierConstraints
        {
            public ICollection<Identifier> Core { get; } = Parser.ParseIdentifiers(Resources.Identifiers.CoreLib);
        }

        public class TokenBorders
        {
            public IDictionary<TokenClass, List<string>> ForTokenClasses { get; }

            public List<string> ForSpecialChars { get; } = new List<string>
            {
                "\"",
                "0",
                "1",
                "2",
                "3",
                "4",
                "5",
                "6",
                "7",
                "8",
                "9"
            };

            public TokenBorders(IDictionary<TokenClass, ICollection<string>> symbols)
            {
                ForTokenClasses = symbols.ToDictionary(pair => pair.Key,
                    pair => pair.Value.SelectMany(s => s.ToCharArray()).Select(c => c.ToString()).Distinct().ToList());
            }
        }
    }
}