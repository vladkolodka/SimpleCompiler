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
            foreach (var codeFile in codeFiles) _compilationPools.Add(new CompilationPool(codeFile));

            _modules.Add(new LexicalAnalyzer());
            _modules.Add(new SyntaxAnalyzer());
            _modules.Add(new SemanticAnalyzer());
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