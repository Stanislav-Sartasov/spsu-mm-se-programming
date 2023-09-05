using BashSimplified.IOLibrary;

namespace BashSimplified.Commands
{
	public interface IExecutable
	{
		public string Run(string[] args, string input, IWriter errorOutput);
	}
}
