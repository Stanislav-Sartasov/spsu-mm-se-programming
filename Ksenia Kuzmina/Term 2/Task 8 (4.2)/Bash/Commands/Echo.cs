using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bash.Commands
{
	public class Echo : ICommand
	{
		private ILogger logger;

		public Echo(ILogger logger)
		{
			this.logger = logger;
		}

		public string[] RunCommand(string[] arguments)
		{
			for (int i = 0; i < arguments.Length; i++)
			{
				logger.Write(arguments[i] + " ");
			}

			logger.Write(Environment.NewLine);

			return new string[] { };
		}
	}
}
