using System;
using Tools;

namespace Commands
{
	public class Exit : ICommand
	{
		public string Run(string[] args, string input, IWriter errorOutput)
		{
			Environment.Exit(0);
			return "";
		}
	}
}
