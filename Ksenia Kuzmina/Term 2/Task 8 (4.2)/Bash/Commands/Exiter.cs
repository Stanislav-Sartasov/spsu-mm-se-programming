using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bash.Commands
{
	public class Exiter : IExiter
	{
		public void Exit()
		{
			Environment.Exit(0);
		}
	}
}
