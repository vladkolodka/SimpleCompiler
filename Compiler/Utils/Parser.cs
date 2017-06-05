using System;
using System.Collections.Generic;
using System.Linq;
using Compiler.Core;
using Compiler.Data;

namespace Compiler.Utils
{
    public static class Parser
    {
        public static ICollection<State> ParseStateMachine(string text)
        {
            var stateCollection = new List<State>();

            foreach (var line in text.Replace("\r", "").Split('\n'))
            {
                var blocks = line.Split(' ');

                if (blocks[0].Equals("--")) continue;

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

        public static ICollection<Identifier> ParseIdentifiers(string text)
        {
            return
                text.Replace("\r", "")
                    .Split('\n')
                    .Select(line => line.Split(':'))
                    .Select(blocks => new Identifier(blocks[0], Convert.ToInt32(blocks[1])))
                    .ToList();
        }

        public static ICollection<ParsingTableState> ParseTransitionsTable(string text)
        {
            var stateList = new List<ParsingTableState>();
            var blockLines = text.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries));

            foreach (var items in blockLines)
            {
                if (items.Length == 3)
                {
                    stateList.Last().ExpectedTokens
                        .Add(new KeyValuePair<TokenClass, int>((TokenClass) int.Parse(items[1]), int.Parse(items[2])));
                    continue;
                }

                var state = new ParsingTableState
                {
                    TransitionState = int.Parse(items[0]),
                    TransisionStateNumber = int.Parse(items[3]),
                    IsAcceptRequired = items[4].Equals("+"),
                    PushToStack = items[5].Equals("-") ? null : new int?(int.Parse(items[5])),
                    IsPopFromStackRequired = items[6].Equals("+"),
                    IsErrorOccured = items[7].Equals("+"),
                    IsNullable = items.Length >= 9 && items[8].Equals("!")
                };

                if (!items[1].Equals("?"))
                    state.ExpectedTokens.Add(new KeyValuePair<TokenClass, int>((TokenClass) int.Parse(items[1]),
                        int.Parse(items[2])));

                stateList.Add(state);
            }

            return stateList;
        }

        public static ICollection<IdentifierRule> ParseIdentifierRules(string text)
        {
            var rules = new List<IdentifierRule>();

            foreach (var items in text.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries)))
            {
                if (items.Length == 1)
                {
                    rules.Add(new IdentifierRule {IdentifierType = int.Parse(items[0])});
                    continue;
                }

                rules.Last().Rules.Add(new Tuple<int, TokenClass, int>(int.Parse(items[1]),
                    (TokenClass) int.Parse(items[2]), int.Parse(items[3])));
            }

            return rules;
        }
    }
}