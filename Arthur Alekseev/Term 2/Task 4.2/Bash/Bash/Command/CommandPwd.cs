using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bash.Command
{
	public class CommandPwd : ICommand
	{
		public string Name => "pwd";

		public string[] Execute(string[] args)
		{
			var path = Directory.GetCurrentDirectory();
			DirectoryInfo info = new DirectoryInfo(path);
			List<string> result = new List<string>();

			result.Add(path);
			foreach (var file in info.EnumerateFiles())
				result.Add(file.Name);

			return result.ToArray();
		}
	}
}
