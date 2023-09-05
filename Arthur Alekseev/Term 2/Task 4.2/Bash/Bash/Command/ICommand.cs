using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bash.Command
{
	public interface ICommand
	{
		public string Name { get; }

		public string[] Execute(string[] args);
	}
}
