using BashSimplified.IOLibrary;

namespace BashSimplified.Commands
{
	public class Exit : IExecutable
	{
		public string Run(string[] args, string input, IWriter errorOutput)
		{
			Environment.Exit(0);
			return string.Empty;
		}
	}
}
