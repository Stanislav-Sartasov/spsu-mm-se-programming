using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bash.Commands
{
	public class Pwd : ICommand
	{
		public string[] RunCommand(string[] arguments)
		{
			string directory = Directory.GetCurrentDirectory();
			string[] fileNames = new DirectoryInfo(directory).GetFiles().Select(o => o.Name).ToArray();

			return (new string[] { directory }).Concat(fileNames).ToArray();
		}
	}
}
