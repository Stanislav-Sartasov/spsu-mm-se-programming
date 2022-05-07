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
        public IResponse Resolve(string commandLine)
        {
            Response response = new Response();
            var commands = Analyser.MySplit(commandLine, '|');
            var stdout = string.Empty;
            foreach (string subCommand in commands)
            {
                string translatedCommand = Analyser.Substitution(subCommand, cc.GetLocalVariables());
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

                response.IsInterrupting = command.IsExit();
                command.SetStdIn(stdout);
                command.Run();
                stdout = command.GetStdOut();
                if (command.GetErrorCode() != 0)
                {
                    response.Message += command.GetErrorMessage();
                }
            }
            response.Message += stdout;
            return response;
        }
    }
}
