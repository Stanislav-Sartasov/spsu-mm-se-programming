using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bash.Commands
{
	public class Cat : ICommand
	{
		public string[] RunCommand(string[] arguments)
		{
			string[] lines = new string[] { };

			for (int i = 0; i < arguments.Length; i++)
			{
				try
				{
					lines = lines.Concat(File.ReadAllText(arguments[i]).Split(Environment.NewLine)).ToArray();
				}
				catch
				{
					Console.WriteLine("The file " + arguments[i] +  " doesn't exist.");
				}
			}

			return lines;
		}
	}
}
