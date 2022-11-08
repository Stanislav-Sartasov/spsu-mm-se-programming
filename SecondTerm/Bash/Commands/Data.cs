namespace Commands
{
	public class Data
	{
		public string Command { get; set; }
		public string[] Args { get; set; }

		public Data(string command, string[] args)
		{
			Command = command;
			Args = args;
		}
	}
}
