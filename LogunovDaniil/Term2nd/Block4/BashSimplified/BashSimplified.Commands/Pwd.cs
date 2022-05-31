using BashSimplified.IOLibrary;

namespace BashSimplified.Commands
{
	public class Pwd : IExecutable
	{
		public string Run(string[] args, string input, IWriter errorOutput)
		{
			string curDir = Directory.GetCurrentDirectory();
			return curDir + "\n"
				+ string.Join("\n", Enumerable.Concat(Directory.GetDirectories(curDir), Directory.GetFiles(curDir)));
		}
	}
}
