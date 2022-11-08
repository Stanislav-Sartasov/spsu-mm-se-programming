using System.IO;
using System.Linq;
using Tools;

namespace Commands
{
	public class Pwd : ICommand
	{
		public string Run(string[] args, string input, Writer errorOutput)
		{
			string directory = Directory.GetCurrentDirectory();
			return directory + "\n"
				+ string.Join("\n", Enumerable.Concat(Directory.GetDirectories(directory), Directory.GetFiles(directory)));
		}
	}
}
