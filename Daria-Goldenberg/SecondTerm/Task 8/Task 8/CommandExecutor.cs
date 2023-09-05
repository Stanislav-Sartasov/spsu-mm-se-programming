using Task_8.Commands;

namespace Task_8
{
	public static class CommandExecutor
	{
		public static CommandResult Execute(List<string> args)
		{
			string commandHeader = args[0].Trim();
			List<string> arguments = args.ToArray()[1..].ToList();
			if (commandHeader == "cat")
				return new CommandCat().Run(arguments);
			if (commandHeader == "wc")
				return new CommandWC().Run(arguments);
			if (commandHeader == "pwd")
				return new CommandPWD().Run(arguments);
			if (commandHeader == "exit")
				return new CommandExit().Run(arguments);
			if (commandHeader == "echo")
				return new CommandEcho().Run(arguments);
			return new CommandStartApp().Run(args);
		}
	}
}