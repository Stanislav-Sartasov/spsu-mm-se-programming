using Bash.App.Output;
using Bash.Command;
using Bash.ProcessStarter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bash.App.BashComponents
{
	public class CommandExecutor
	{
		private List<ICommand> commandExecutors;
		private IProcessStarter processStarter;

		public CommandExecutor(ILogger logger, IProcessStarter processStarter)
		{
			commandExecutors = new List<ICommand>() {
				new CommandExit(),
				new CommandEcho(logger),
				new CommandPwd(),
				new CommandCat(),
				new CommandWc(),
				new CommandCd() 
			};

			this.processStarter = processStarter;
		}

		public string[] Execute(string[] commandParts)
		{
			if (commandParts.Length == 0 || commandParts.Length == 1 && commandParts[0] == "")
				return new string[0];

			var commandExecutor = GetCommand(commandParts[0]);

			if (commandExecutor is null)
				StartApp(commandParts);

			else
				return commandExecutor.Execute(commandParts[1..]);

			return new string[0];
		}

		private ICommand? GetCommand(string name)
		{
			foreach (var executor in commandExecutors)
			{
				if (executor.Name == name)
					return executor;
			}
			return null;
		}

		private void StartApp(string[] command)
		{
			processStarter.StartProcess(command[0], string.Join(" ", command[1..]));
		}
	}
}
