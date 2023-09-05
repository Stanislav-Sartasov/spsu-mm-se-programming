using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bash.Command
{
	public class CommandCd : ICommand
	{
		public string Name => "cd";

		public string[] Execute(string[] args)
		{
			try
			{
				Directory.SetCurrentDirectory(args[0]);
			}
			catch (Exception ex)
			{
				return new string[] { "Could not find the path specified" };
			}
			return new string[0];
		}
	}
}
