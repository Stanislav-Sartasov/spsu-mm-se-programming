using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bash
{
	public interface ILogger
	{
		public void Write(string argument);
		public string Input();
	}
}
