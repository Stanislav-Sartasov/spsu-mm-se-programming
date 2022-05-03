using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace CommandResolverLib
{
    public class CommandResolver : ICommandResolver
    {
        private ICommandCreator cc;

        public CommandResolver(ICommandCreator commandCreator)
        {
            cc = commandCreator;
        }
        public string Resolve(string commandLine)
        {
            var commands = Analyser.MySplit(commandLine, '|');
            var stdout = string.Empty;
            foreach (string subCommand in commands)
            {
                string translatedCommand = Analyser.Substitution(subCommand, cc);
                var complex = Analyser.MySplit(translatedCommand, '>');
                ICommand command;
                if (complex.Length != 1)
                {
                    command = cc.CreateSTD(complex);
                }
                else
                {
                    command = cc.Create(translatedCommand);
                }

                command.SetStdIn(stdout);
                command.Run();
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
    }
}
