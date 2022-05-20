using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bash.Commands
{
	public class Exit : ICommand
	{
		private IExiter exiter;

		public Exit(IExiter exiter)
		{
			this.exiter = exiter;
		}

		public string[] RunCommand(string[] arguments)
		{
			exiter.Exit();
			return new string[] { };
		}
	}
}
