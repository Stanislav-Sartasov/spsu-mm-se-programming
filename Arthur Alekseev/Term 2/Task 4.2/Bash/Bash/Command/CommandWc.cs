using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bash.Command
{
	public class CommandWc : ICommand
	{
		public string Name => "wc";

		public string[] Execute(string[] args)
		{
			List<string> result = new List<string>();
			for (int i = 0; i < args.Length; i++)
				try
				{
					var data = File.ReadAllText(args[i]);
					var byteLength = new System.IO.FileInfo(args[i]).Length;

					result.Add(
						args[i] + "\t" + data.Split(Environment.NewLine).Length.ToString() +
						"\t" + data.Split(" ").Length.ToString() +
						"\t" + byteLength.ToString()
					);
				}
				catch
				{
					result.Add("No such file or directory");
				}

			return result.ToArray();
		}
	}
}
