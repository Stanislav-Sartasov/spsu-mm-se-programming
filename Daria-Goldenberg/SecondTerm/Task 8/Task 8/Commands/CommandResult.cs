namespace Task_8.Commands
{
	public class CommandResult
	{
		public readonly List<string> Errors;
		public readonly List<string> Results;

		public CommandResult(List<string> errors, List<string> results)
		{
			Errors = errors;
			Results = results;
		}
	}
}
