using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bash.App.BashComponents
{
	public class CommandSplitter
	{
		public string[] SplitCommands(string command)
		{
			var result = new List<string>() { "" };
			bool inBounds = false;

			foreach (var c in command)
				if (c == '"')
				{
					inBounds = !inBounds;
					result[^1] += c;
				}
				else
					if (c == '|' && !inBounds)
					result.Add("");
				else
					result[^1] += c;

			return result.ToArray();
		}
	}
}
