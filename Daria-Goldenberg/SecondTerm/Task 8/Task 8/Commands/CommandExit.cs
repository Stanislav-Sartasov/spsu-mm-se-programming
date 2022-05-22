namespace Task_8.Commands
{
	public class CommandExit : Command
	{
		public override CommandResult Run(List<string> args)
		{
			return new CommandResult(new List<string> { "exit" }, new List<string> { });
		}
	}
}