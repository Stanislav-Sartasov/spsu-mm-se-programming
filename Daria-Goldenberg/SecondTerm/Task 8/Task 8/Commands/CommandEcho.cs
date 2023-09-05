namespace Task_8.Commands
{
	public class CommandEcho : Command
	{
		public override CommandResult Run(List<string> args)
		{
			foreach (string arg in args)
				Console.Write(arg + " ");
			Console.WriteLine();
			return new CommandResult(new List<string> { }, new List<string> { } );
		}
	}
}