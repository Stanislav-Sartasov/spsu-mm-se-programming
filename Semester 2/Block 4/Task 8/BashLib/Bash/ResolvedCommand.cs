namespace BashLib.Bash
{
	public class ResolvedCommand
	{
		public string Name { get; private set; }
		public string[] Args { get; private set; }

		public ResolvedCommand(string name, string[] args)
		{
			Name = name;
			Args = args;
		}
	}
}