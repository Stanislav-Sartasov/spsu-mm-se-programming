using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bash.App.BashComponents
{
	public class SubcommandParser
	{
		public string[] SplitCommand(string command)
		{
			var result = new List<string>() { "" };
			bool inBounds = false;

			foreach (var c in command)
				if (c == '"')
					inBounds = !inBounds;
				else
					if (c == ' ' && !inBounds && result[^1] != "")
					result.Add("");
				else
					result[^1] += c;

			for(int i = 0; i < result.Count; i++)
				result[i] = result[i].Trim();

			return result.ToArray();
		}
	}
}
