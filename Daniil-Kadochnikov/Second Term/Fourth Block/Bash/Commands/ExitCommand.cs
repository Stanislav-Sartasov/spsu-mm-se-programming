namespace Commands
{
	public class ExitCommand : ICommand
	{
		public string Name { get; } = "exit";

		public string[]? Execute(string[] arguments)
		{
			if (arguments.Length == 0)
				Environment.Exit(0);
			else if (arguments.Length == 1 && int.TryParse(arguments[0], out int argument))
				Environment.Exit(argument);
			else
				Environment.Exit(-1);
			return null;
		}
	}
}