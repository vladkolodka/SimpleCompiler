using System.Collections.Generic;

namespace Compiler.Core
{
    public interface ICompilerModule
    {
        ICollection<string> Errors { get; }
        ICollection<string> Messages { get; }
        bool TryBypass(CompilationPool dataPool);
    }
}