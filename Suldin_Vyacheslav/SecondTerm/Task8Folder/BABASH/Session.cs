using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace BABASH
{
    public class Session 
    {
        private Dictionary<string, string> localVar = new Dictionary<string, string>();

        public IReader Reader = new ConsoleReader();

        private string curentDirectory;

        public Session()
        {
            curentDirectory = Directory.GetCurrentDirectory();
            localVar.Add("$HOME", curentDirectory);
        }
        public int Start()
        {
            while (true)
            {
                var line = Reader.GetLine();
                
                var responce = Resolve(line);
                if (responce == null)
                    break;
                Reader.Show(responce);
            }
            return 1;
        }

        public string Resolve(string line)
        {
            var commands = Analyser.MySplit(line, '|');
            var stdout = string.Empty;
            foreach (string subCommand in commands)
            {
                string translatedCommand = Analyser.Substitution(subCommand, localVar);
                var complex = Analyser.MySplit(translatedCommand, '>');
                Command command;
                if (complex.Length != 1)
                {
                    command = new STDCommand(complex, this);
                }
                else
                {
                    command = CommandCreator.Create(translatedCommand, this);
                }

                command.SetStdIn(stdout);
                command.Execute();
                if (command.GetErrorCode() == 0)
                {
                    stdout = command.GetStdOut();
                }
                else
                {
                    return command.GetErrorMessage();
                }
            }
            return stdout;
        }

        public string GetCurrentDirectory()
        {
            return curentDirectory;
        }
        public void SetCurrentDirectory(string path)
        {
            curentDirectory = path;
        }
        public string GetLocalVar(string key)
        {
            return localVar[key];
        }
        public void SetLocalVar(string key, string value)
        {
            try
            {
                localVar.Add("$" + key, value);
            }
            catch (ArgumentException)
            {
                localVar["$" + key] = value;
            }
        }
    }
}
