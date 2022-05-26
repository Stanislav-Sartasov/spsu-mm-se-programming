using AbstractOperators;
using CommandManager;


namespace BushRealisation
{
    public class Bush // Bash
    {
        private ALogger logger;
        private Splitter splitter;
        private Executor executor;
        private VariableManager storage;

        public Bush(ALogger logger, ARunner runner, Dictionary<String, ACommand> commands)
        {
            this.logger = logger;

            splitter = new Splitter();
            storage = new VariableManager();
            executor = new Executor(commands, runner);
        }

        public void Run()
        {
			string line;
			List<String> output;

			SendDesctiption();

			while (true)
			{
				line = logger.ReadLine();
				output = new List<String>();

				var parsingResults = splitter.ParseToSingleCommands(line);
				var isPipe = parsingResults.Item1;
				var subcommands = parsingResults.Item2;

				if (!isPipe)
                {
                    try
                    {
						storage.TryToAssignmentVariable(subcommands);
					}
					catch (Exception ex)
					{
						logger.Log(ex.Message);
					}
				}
				else
                {
					foreach (var command in subcommands)
					{
						var splittedCommand = splitter.ParseSubCommand(command);
						var cmd = splittedCommand.Item1;
						var args = splittedCommand.Item2;

						try
                        {
							args = storage.ReplaceVariables(args);
						}
						catch (Exception ex)
						{
							logger.Log(ex.Message);
							args.Clear();
						}
						output = executor.Execute(cmd, args, output);
					}
					foreach (var outLine in output)
						logger.Log(outLine);
				}
					
				logger.Log(Environment.NewLine);
			}
		}

        private void SendDesctiption()
        {
            logger.Log("Task 8 Bush(Bash)\n" +
                "That program can accomplish this list of commands: " +
				"\n1) echo [arg1] [arg2] .. [argN] - return arguments to standart output" +
				"\n2) cat [file1] [file2] .. [fileN] - returns contents of file specified, if file does not exist returns \"This file didn't exist!\"" +
				"\n + supports pipelined processing. *Watch 9)" +
				"\n3) wc [file1] [file2] .. [fileN] - returns lines, words and byte length of the files specified, if file does not exist returns \"This file or directory didn't exist!\"" +
				"\n + supports pipelined processing. *Watch 9)" +
				"\n4) cd [directory] - changes the directory to absolute or relative directory specified, returns nothing if successful, returns  \"This path doesn't exist!\"" +

				"\n5) pwd - returns name of directory and all the subdirectories and files within" +
				"\n6) exit [code] - exits the application with code specified. By default - 0." +
				"\n<-------Special Opportunities------->" +
				"\n7) If the command is not supported, Bush wil be tried to start app with the same name. Input stream and arguments will be passed to the app" +
				"\n8) You can assign local variables. Example: $varname=value" +
				"\n9) | used to a pipelined processing. Example: [cmd1 arg arg] | [cmd2 arg arg] | ..., outputs of cmd1 will become inputs of cmd2.\n");
        }
    }
}