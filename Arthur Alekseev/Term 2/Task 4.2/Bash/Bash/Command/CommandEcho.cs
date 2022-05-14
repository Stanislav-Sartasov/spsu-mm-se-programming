using Bash.App;
using Bash.App.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bash.Command
{
	public class CommandEcho : ICommand
	{
		ILogger logger;
		public string Name => "echo";

		public CommandEcho(ILogger output)
		{
			logger = output;
		}

		public string[] Execute(string[] args)
		{
			foreach (var arg in args)
				logger.Log(arg + " ");
			return new string[0];
		}
	}
}
