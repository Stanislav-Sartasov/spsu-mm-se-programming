using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Text.RegularExpressions;

namespace BABASH
{
    public class CommandCreator
    {
        static public Command Create(string commandLine, Session comandSession)
        {
            string[] splitted = Analyser.MySplit(commandLine, ' ');
            string commandName = splitted[0];
            string[] args = splitted.Skip(1).ToArray();
            for (int i = 0; i < args.Length; i++)
                args[i] = args[i].Replace("\"", string.Empty);
            switch (commandName)
            {
                case "rmdir": return new RMDIRCommand(args, comandSession);
                case "rm": return new RMCommand(args,comandSession);
                case "touch": return new TOUCHCommand(args, comandSession);
                case "mkdir" : return new MKDIRCommand(args, comandSession);
                case "cd": return new CDCommand(args, comandSession);
                case "export": return new EXPORTCommand(args, comandSession);
                case "pwd": return new PWDCommand(null, comandSession);
                case "ls": return new LSCommand(args, comandSession);
                case "cat": return new CATCommand(args, comandSession);
                case "wc": return new WCCommand(args, comandSession);
                case "echo": return new ECHOCommand(args, comandSession);
                case "exit": return new EXITCommand(null, comandSession);
                default: return new UNKNOWCommand(commandName, comandSession);
            }            
        }
    }
}
