using System.Collections.Generic;
using System.Linq;
using CommandResolverLib;

namespace CommandLib
{
    public class CommandCreator : ICommandCreator
    {
        public ICommand Create(string commandLine)
        {
            string[] splitted = Analyser.MySplit(commandLine, ' ');
            string commandName = splitted[0];
            string[] args = splitted.Skip(1).ToArray();
            for (int i = 0; i < args.Length; i++)
                args[i] = args[i].Replace("\"", string.Empty);
            switch (commandName)
            {
                case "rmdir": return new RMDIRCommand(args);
                case "rm": return new RMCommand(args);
                case "touch": return new TOUCHCommand(args);
                case "mkdir": return new MKDIRCommand(args);
                case "cd": return new CDCommand(args);
                case "export": return new EXPORTCommand(args);
                case "pwd": return new PWDCommand(null);
                case "ls": return new LSCommand(args);
                case "cat": return new CATCommand(args);
                case "wc": return new WCCommand(args);
                case "echo": return new ECHOCommand(args);
                case "help": return new HELPCommand(args);
                case "exit": return new EXITCommand(null);
                default: return new UNKNOWNCommand(commandName);
            }
        }

        public ICommand CreateSTD(string[] complex)
        {
            return new STDCommand(complex, this);
        }
        public IReadOnlyDictionary<string, string> GetLocalVariables()
        {
            return Environ.GetVars();
        }
    }
}
