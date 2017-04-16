using System.Collections.Generic;
using Compiler.Data;

namespace Compiler.Module
{
    public interface ICompilerModule
    {
        ICollection<Error> Errors { get; }
        bool TryBypass(CompilationPool dataPool);
    }
}