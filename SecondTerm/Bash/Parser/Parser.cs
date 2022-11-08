using System;
using System.Collections.Generic;
using System.Linq;
using Commands;

namespace Parser
{
	public class Parser
	{
		private Dictionary<string, string> variables = new();
		private string forbiddenSymbols = "\\|$?<>:;=";

		private void VariableInit(string name, string value)
		{
			if (variables.ContainsKey(name))
				variables[name] = value;
			else
				variables.Add(name, value);
		}

		private string Parse(string str)
		{
			if (str.Length == 0 || str[0] != '$')
				return str;

			str = str[1..];

			if (str.Contains('='))
			{
				var assign = str.Split('=', 2);
				for (int i = 0; i < assign.Length; i++)
				{
					for (int j = 0; j < assign[i].Length; j++)
					{
						if (forbiddenSymbols.Contains(assign[i][j]))
							return "";
					}
				}
				if (assign[0].Length == 0)
					return "";
				VariableInit(assign[0], assign[1]);
				return assign[1];
			}

			if (!variables.ContainsKey(str))
			{
				var env = Environment.GetEnvironmentVariable(str);
				if (env != "")
					return env;
				return "";
			}
			return variables[str];
		}

		public Data ParseCommands(string cmdLine)
		{
			var InvCommas = cmdLine.Split('"');
			bool inCommas = false;
			var args = new List<string>();
			foreach (var commas in InvCommas)
			{
				if (commas.Length == 0)
					continue;

				var fragments = commas.Split(' ').Select(s => Parse(s));

				if (inCommas)
					args.Add(string.Join(" ", fragments));
				else
					foreach (var fragment in fragments.Where(s => !string.IsNullOrWhiteSpace(s)))
						args.Add(fragment);

				inCommas = !inCommas;
			}
			if (args.Count == 0)
				return null;
			return new Data(args[0], args.Skip(1).ToArray());
		}
	}
}
