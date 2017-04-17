using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Compiler.Core;
using Compiler.Resources;

namespace Compiler
{
    internal class Programs
    {
        private static void Main(string[] args)
        {
            Start(args);
//            Console.ReadKey();
        }

        private static void Start(string[] fileNames)
        {
            if (fileNames.Length == 0)
            {
                Console.WriteLine(Messages.InvalidFileName);
                return;
            }

            var codeFiles = new List<string>();
            foreach (var fileName in fileNames)
                try
                {
                    var text = File.ReadAllText(fileName);

                    text = Regex.Replace(text, @"(;.*?)\n", "\r\n");
                    text = Regex.Replace(text, @"[ ]{2,}", " ");

                    codeFiles.Add(text);
                }
                catch (Exception)
                {
                    Console.WriteLine(Messages.FileNotFound, fileName);
                    return;
                }

            var compiler = new Kernel(codeFiles);
            compiler.Run();
        }
    }
}