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
//            try
//            {
                Start(args);
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e);
//            }

            Console.ReadKey();
        }

        private static void Start(IReadOnlyCollection<string> fileNames)
        {
            if (fileNames.Count == 0)
            {
                Console.WriteLine(Messages.InvalidFileName);
                return;
            }

            var codeFiles = new Dictionary<string, string>();
            foreach (var fileName in fileNames)
                try
                {
                    var text = File.ReadAllText(fileName);

                    text = Regex.Replace(text, @"(;.*?)\n", "\r\n");
                    text = Regex.Replace(text, @"[ ]{2,}", " ");

                    codeFiles.Add(fileName, text);
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