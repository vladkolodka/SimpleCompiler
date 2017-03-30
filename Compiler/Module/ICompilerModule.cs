using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Compiler.Data;

namespace Compiler.Module
{
    public interface ICompilerModule
    {
        bool TryBypass(CompilationPool dataPool);
        ICollection<Error> Errors { get; }
    }
}