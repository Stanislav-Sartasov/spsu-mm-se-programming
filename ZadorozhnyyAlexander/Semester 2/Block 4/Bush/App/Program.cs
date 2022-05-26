using BushRealisation;
using Exceptions;
using Logger;
using Runner;
using Commands;
using AbstractOperators;

namespace App
{
    public class Program
    {
        public static void Main(string[] args)
        {
			try
			{
				var commands = LoadCommandDictionary();
				var logger = new BushLogger();
				new Bush(logger, new ProcessRunner(logger), commands).Run();
			}
			catch (ExitException ex)
			{
				Environment.Exit(ex.ExitCode);
			}
		}

		private static Dictionary<String, ACommand> LoadCommandDictionary()
		{
			var commands = new Dictionary<String, ACommand>();

			commands.Add("echo", new Echo());
			commands.Add("cat", new Cat());
			commands.Add("cd", new Cd());
			commands.Add("wc", new Wc());
			commands.Add("pwd", new Pwd());
			commands.Add("exit", new Exit());

			return commands;
		}
	}
}