using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bash.App.BashComponents
{
	public class Variable
	{
		public readonly string Name;
		public string Value;

		public Variable(string name, string value)
		{
			Name = name;
			Value = value;
		}
	}
}
