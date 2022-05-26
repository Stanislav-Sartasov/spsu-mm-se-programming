using AbstractOperators;
using CommandManager;


namespace BushRealisation
{
    public class Bush
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

			// TODO SendDesctiptionMessage();

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
    }
}