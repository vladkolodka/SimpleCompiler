using System;
using System.Collections.Generic;
using Compiler.Data;
using Compiler.Module;

namespace Compiler.Core
{
    public class Kernel
    {
        private readonly ICollection<CompilationPool> _compilationPools = new List<CompilationPool>();
        private readonly ICollection<ICompilerModule> _modules = new List<ICompilerModule>();

        public Kernel(IEnumerable<string> codeFiles)
        {
            // create compilation pool for each code file
            foreach (var codeFile in codeFiles) _compilationPools.Add(new CompilationPool(codeFile));

            var lexicalAnalyzer = new LexicalAnalyzer();

            lexicalAnalyzer.TokenFounded += TokenFounded;

            // create compiler modules chain
            _modules.Add(lexicalAnalyzer);
            _modules.Add(new SyntaxAnalyzer());
            _modules.Add(new SemanticAnalyzer());
        }

        private void TokenFounded(Token token, int line)
        {
            Console.WriteLine($"Token {token.Class}:{token.Id} founded on line {line + 1}");
        }

        public void Run()
        {
            foreach (var pool in _compilationPools)
            foreach (var module in _modules)
            {
                if (module.TryBypass(pool)) continue;

                // TODO errors handling
                Console.WriteLine(string.Join("\n", module.Errors));

                // terminate compilation process if there are errors
                break;
            }
        }
    }
}