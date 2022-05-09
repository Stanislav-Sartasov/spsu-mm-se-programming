using Bash.App.BashComponents;
using Bash.App.Output;
using Bash.Command;
using Bash.ProcessStarter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bash.App
{
	public class Bash
	{
		private ILogger logger;

		private SubcommandParser commandParser;
		private CommandSplitter commandSplitter;
		private VariableManager variableManager;
		private CommandExecutor commandExecutor;

		public Bash(ILogger logger, IProcessStarter processStarter)
		{
			this.logger = logger;

			commandSplitter = new CommandSplitter();
			variableManager = new VariableManager();
			commandParser = new SubcommandParser();
			commandExecutor = new CommandExecutor(logger, processStarter);
		}

		public void Run()
		{
			string nextLine;
			string[] previousResults;

			SendDesctiptionMessage();

			while (true)
			{
				nextLine = logger.Read();
				previousResults = new string[0];
				bool cancelNextOutput = false;

				var CommandStrings = commandSplitter.SplitCommands(nextLine.Replace("=", " = "));

				foreach (var command in CommandStrings)
				{
					var splittedCommand = commandParser.SplitCommand(command.Trim()).Concat(previousResults).ToArray();
					try
					{
						cancelNextOutput = variableManager.ReplaceVariables(splittedCommand);
					}
					catch (Exception ex)
					{
						cancelNextOutput = true;
						logger.Log(ex.Message);
					}

					if (cancelNextOutput)
						previousResults = new string[0];
					else
						previousResults = commandExecutor.Execute(splittedCommand);

					foreach (var argument in previousResults)
						logger.Log(argument);
				}
			}
		}

		private void SendDesctiptionMessage()
		{
			logger.Log("Task 8 (Bash)\nThe program has these commands:" +
				"\n1) exit [code] - exits the application with code specified" +
				"\n2) cat [file1] [file2] .. [fileN] - returns contents of file specified, if file does not exist returns \"No such file or directory\"" +
				"\n3) wc [file1] [file2] .. [fileN] - returns lines, words and byte length of the files specified, if file does not exist returns \"No such file or directory\"" +
				"\n4) cd [directory] - changes the directory to absolute or relative directory specified, returns nothing if successful, returns  \"Could not find the path specified\"" +
				"\n5) echo [arg1] [arg2] .. [argN] - return nothing, prints arguments to standart output" +
				"\n6) pwd - returns name of directory and all the files within" +
				"\n7) if no command has been recognised, app with the name wil be tried to start. Later arguments will be passed to the app" +
				"\nThe application supports assigning local variables like this:$varname = value" +
				"\n| is used to do a command pipeline, used like this: [cmd1] | [cmd2] | ..., outputs of cmd1 will become inputs of cmd2.\n");
		}
	}
}
