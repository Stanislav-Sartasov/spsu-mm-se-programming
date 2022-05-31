using BashSimplified.IOLibrary;

namespace BashSimplified.Commands
{
	public class Echo : IExecutable
	{
		public string Run(string[] args, string input, IWriter errorOutput)
		{
			return string.Join(" ", args);
		}
	}
}
