using System.Collections.Generic;
using Compiler.Core;

namespace Compiler.Modules
{
    public abstract class CompilerModuleBase : ICompilerModule
    {
        public ICollection<string> Errors { get; } = new List<string>();
        public ICollection<string> Messages { get; } = new List<string>();

        public abstract bool TryBypass(CompilationPool dataPool);
    }
}