using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bash
{
	public class Logger : ILogger
	{
		public string Input()
		{
			return Console.ReadLine();
		}

		public void Write(string argument)
		{
			Console.Write(argument);
		}
	}
}
