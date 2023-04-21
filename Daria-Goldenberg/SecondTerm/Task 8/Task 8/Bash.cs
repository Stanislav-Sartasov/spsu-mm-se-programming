using Task_8.Commands;
using Task_8.Logger;

namespace Task_8
{
	public class Bash
	{
		private Dictionary<string, string> variables;
		private ILogger logger;

		public Bash(ILogger logger)
		{
			variables = new Dictionary<string, string>();
			this.logger = logger;
		}

		public void Run()
		{
			while (true)
			{
				string input = logger.Read();
				List<string> commands = CommandParser.ParseCommands(input);
				CommandResult lastResult = new CommandResult(new List<string>(), new List<string>());

				foreach (string command in commands)
				{
					string commandCpy = command;

					if (commandCpy.Contains("="))
						variables[commandCpy.Split("=")[0].Trim()] = commandCpy.Split("=")[1].Trim();

					try
					{
						VariableParser.SetVariable(ref commandCpy, variables);
					}
					catch (Exception ex)
					{
						logger.Write(ex.Message + Environment.NewLine);
						break;
					}

					List<string> args = ArgumentParser.ParseArguments(commandCpy);

					lastResult = CommandExecutor.Execute(args.Concat(lastResult.Results).ToList());

					foreach (var error in lastResult.Errors)
					{
						if (error == "exit")
							return;
						logger.Write(error + Environment.NewLine);
					}
				}

				foreach (var result in lastResult.Results)
					logger.Write(result + Environment.NewLine);
			}
		}
	}
}
