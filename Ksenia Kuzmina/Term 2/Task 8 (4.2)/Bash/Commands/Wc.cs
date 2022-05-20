using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bash.Commands
{
	public class Wc : ICommand
	{
		public string[] RunCommand(string[] arguments)
		{
			string[] info = new string[] { };

			for (int i = 0; i < arguments.Length; i++)
			{
				try
				{
					info = info.Concat(new string[] { arguments[i] + " " + File.ReadAllText(arguments[i]).Split("\n").Length + " " 
						+ File.ReadAllText(arguments[i]).Split(" ").Length + " " 
						+ new FileInfo(arguments[i]).Length}).ToArray();
				}
				catch
				{
					Console.WriteLine("The file " + arguments[i] + " doesn't exist.");
				}
			}

			return info;
		}
	}
}
