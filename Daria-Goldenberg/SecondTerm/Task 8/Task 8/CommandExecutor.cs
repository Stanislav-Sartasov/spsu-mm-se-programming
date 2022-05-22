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
				return new Cat().Run(arguments);
			if (commandHeader == "wc")
				return new WC().Run(arguments);
			if (commandHeader == "pwd")
				return new PWD().Run(arguments);
			if (commandHeader == "exit")
				return new Exit().Run(arguments);
			if (commandHeader == "echo")
				return new Echo().Run(arguments);
			return new StartApp().Run(args);
		}
	}
}