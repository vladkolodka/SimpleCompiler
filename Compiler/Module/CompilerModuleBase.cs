using System.Collections.Generic;
using Compiler.Data;

namespace Compiler.Module
{
    public abstract class CompilerModuleBase : ICompilerModule
    {
        public ICollection<Error> Errors { get; } = new List<Error>();

        public abstract bool TryBypass(CompilationPool dataPool);
    }
}