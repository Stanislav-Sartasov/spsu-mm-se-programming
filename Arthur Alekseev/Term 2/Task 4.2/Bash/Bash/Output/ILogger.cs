using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bash.App.Output
{
	public interface ILogger
	{
		public void Log(string arg);

		public string Read();
	}
}
