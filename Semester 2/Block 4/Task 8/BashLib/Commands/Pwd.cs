using BashLib.IO;
using System.IO;
using System.Linq;

namespace BashLib.Commands
{
	public class Pwd : ICommand
	{
		public string Run(string[] arguments)
		{
			string directory = Directory.GetCurrentDirectory();
			string[] files = new DirectoryInfo(directory).GetFiles().Select(f => f.Name).ToArray();

			return directory + "\n" + string.Join("\n", files);
		}
	}
}