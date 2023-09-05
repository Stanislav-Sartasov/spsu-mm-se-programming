using Bash.Commands;
using Bash.Interfaces;
using System.Diagnostics;

namespace Bash.BashComponents
{
    public class Executer : IExecuter
    {
        private List<ICommand> commands;
        public Executer()
        {
            commands = new List<ICommand>()
            {
                new CatCommand(),
                new CdCommand(),
                new EchoCommand(),
                new ExitCommand(),
                new PwdCommand(),
                new WcCommand()
            };
        }

        public string? Execute(string command, string args)
        {
            var bashProcess = commands.Find(x => x.Name.Equals(command))?.Execute(args);
            if (bashProcess != null)
            {
                return bashProcess.ToString();
            }

            var systemProcess = Process.Start(command, args);
            if (systemProcess != null)
            {
                return "";
            }

            return null;
        }
    }
}