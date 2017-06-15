using System;
using System.Collections.Generic;
using Compiler.Modules;

namespace Compiler.Core
{
    public class Kernel
    {
        private readonly ICollection<CompilationPool> _compilationPools = new List<CompilationPool>();
        private readonly ICollection<ICompilerModule> _modules = new List<ICompilerModule>();

        public Kernel(IDictionary<string, string> codeFiles)
        {
            // create compilation pool for each code file
            foreach (var codeFile in codeFiles)
                _compilationPools.Add(new CompilationPool(codeFile.Key, codeFile.Value));

            // create compiler modules chain
            _modules.Add(new LexicalAnalyzer());
            _modules.Add(new SyntaxAnalyzer());
//            _modules.Add(new CodeGenerator());
        }

        public void Run()
        {
            foreach (var pool in _compilationPools)
            foreach (var module in _modules)
            {
                try
                {
                    if (module.TryBypass(pool))
                    {
                        Console.WriteLine(string.Join("\n", module.Messages));
                        continue;
                    }
                }
                catch (NotImplementedException e)
                {
                    Console.WriteLine($"Module {e.Message} is not implemented!");
                }


                Console.Write(string.Join("\n", module.Errors));

                // terminate compilation process if there are errors
                break;
            }
        }
    }
}