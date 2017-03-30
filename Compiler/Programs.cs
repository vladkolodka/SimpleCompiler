using System;
using System.Collections.Generic;
using System.IO;
using Compiler.Core;
using Compiler.Resources;

namespace Compiler
{
    internal class Programs
    {
        static void Main(string[] args)
        {
            Start(args);
            Console.ReadKey();
        }

        private static void Start(string[] fileNames)
        {
            if (fileNames.Length == 0)
            {
                Console.WriteLine("You should enter file name!");
                return;
            }

            List<string> codeFiles = new List<string>();
            foreach (var fileName in fileNames)
            {
                try
                {
                    codeFiles.Add(File.ReadAllText(fileName));
                }
                catch (Exception)
                {
                    Console.WriteLine($"File \"{fileName}\" not found!\nProcess terminated...");
                    return;
                }
            }

            var compiler = new Kernel(codeFiles);
            compiler.Run();
        }
    }
}