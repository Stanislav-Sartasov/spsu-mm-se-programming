using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bash.Command
{
	public class CommandCat : ICommand
	{
		public string Name => "cat";

		public string[] Execute(string[] args)
		{
			List<string> result = new List<string>();
			for (int i = 0; i < args.Length; i++)
				try
				{
					result.Add(File.ReadAllText(args[i]));
				}
				catch
				{
					result.Add("No such file or directory");
				}

			return result.ToArray();
		}
	}
}
