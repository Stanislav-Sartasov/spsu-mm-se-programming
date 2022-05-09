using Bash.App.BashComponents.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bash.Command
{
	public class CommandExit : ICommand
	{
		public string Name => "exit";

		public string[] Execute(string[] args)
		{
			if (args.Length == 0)
				throw new ExitException(0);
			else
				if (int.TryParse(args[0], out int exitCode))
				throw new ExitException(exitCode);
			else
				throw new ExitException(0);
		}
	}
}
