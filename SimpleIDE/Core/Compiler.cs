using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using SimpleIDE.Data;

namespace SimpleIDE.Core
{
    public class Compiler
    {
        private readonly string[] _fileNames;

        public Compiler(string[] fileNames)
        {
            _fileNames = fileNames;
        }

        public List<KeyValuePair<int, string>> Identifiers { get; } = new List<KeyValuePair<int, string>>();
        public List<Error> Errors { get; } = new List<Error>();
        public Dictionary<int, List<string>> Tokens { get; } = new Dictionary<int, List<string>>();

        public void Compile()
        {
            var process = new Process
            {
                StartInfo =
                {
                    FileName = "compiler.exe",
                    CreateNoWindow = true,
                    Arguments = string.Join(" ", _fileNames),
                    RedirectStandardOutput = true,
                    UseShellExecute = false
                }
            };
            process.Start();

            string line;
            while ((line = process.StandardOutput.ReadLine()) != null) Parse(line);
        }

        private void Parse(string line)
        {
            /*
            0 : file name
            1 : code
            2 : parameters list divided by space
            3 : message
            */
            var blocks = line.Split('|');

            var code = Convert.ToInt32(blocks[1]);
            var parameters = blocks[2].Split(' ');

            if (code > 0)
                switch (code)
                {
                    case 1:
                        var lineNumber = Convert.ToInt32(parameters[2]);

                        if (!Tokens.ContainsKey(lineNumber)) Tokens.Add(lineNumber, new List<string>());

                        Tokens[lineNumber].Add($"[{parameters[0]}:{parameters[1]} ({parameters[3]})]");
                        break;
                    case 2:
                        Identifiers.Add(new KeyValuePair<int, string>(Convert.ToInt32(parameters[0]), parameters[1]));
                        break;
                    case 3:
                        MessageBox.Show(blocks[3]);
                        break;
                }
            if (code >= 0) return;

            code = Math.Abs(code);

            // error message
            switch (code)
            {
                case 3:
                    Errors.Add(new Error
                    {
                        FileName = blocks[0],
                        Description = $"Symbol {parameters[0]} is not expected.",
                        Message = blocks[3],
                        Code = code,
                        Line = Convert.ToInt32(parameters[1])
                    });
                    break;
                case 4:
                    Errors.Add(new Error
                    {
                        FileName = blocks[0],
                        Description = $"Identifier type {parameters[0]} is not defined.",
                        Message = blocks[3],
                        Code = code
                    });
                    break;
                case 5:
                    Errors.Add(new Error
                    {
                        FileName = blocks[0],
                        Description = $"Expected: {parameters[0]}, found: {parameters[1]}",
                        Message = blocks[3],
                        Code = code
                    });
                    break;
                default:
                    Errors.Add(new Error {FileName = blocks[0], Message = blocks[3]});
                    break;
            }
        }
    }
}