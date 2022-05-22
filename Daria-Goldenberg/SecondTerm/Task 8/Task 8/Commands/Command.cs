namespace Task_8.Commands
{
	public abstract class Command
	{
		public abstract CommandResult Run(List<string> args);
	}
}
