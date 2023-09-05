namespace BashSimplified.CommandResolver
{
	public class CommandData
	{
		public string Command { get; init; }
		public string[] Args { get; init; }

		public CommandData(string cmd, string[] args)
		{
			Command = cmd;
			Args = args;
		}
	}
}
