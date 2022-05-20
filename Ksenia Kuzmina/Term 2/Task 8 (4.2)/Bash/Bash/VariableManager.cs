using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bash
{
	public class VariableManager
	{
		private const string forbiddenSymbols = "\\/:*?\"<>| ";
		private Dictionary<string, string> variables = new Dictionary<string, string>();

		public string ProcessCommand(string command)
		{
			command = ReplaceVariable(command);

			if (command.Trim().StartsWith("$") && command.Contains("="))
			{
				SetVariableValue(command.Split("=")[0].Trim(), command.Split("=")[1].Trim());
			}

			return command;
		}

		private void SetVariableValue(string name, string value)
		{
			if (variables.ContainsKey(name))
			{
				variables[name] = value;
			}
			else
			{
				variables.Add(name, value);
			}
		}
		
		private string ReplaceVariable(string command)
		{
			int startIndex = command.IndexOf(" ") + 1;
			string foundName = "";
			bool isWriting = false;
			List<string> lines = new List<string>();
			string commandWithoutFirstArg = string.Join(" ", command.Split(" ")[1..]);

			for (int i = startIndex; i < command.Length; i++)
			{
				if (forbiddenSymbols.Contains(command[i]) && foundName != "")
				{
					isWriting = false;
					lines.Add(foundName);
					foundName = "";
				}

				if (command[i] == '$')
				{
					isWriting = true; 
				}

				if (isWriting)
				{
					foundName += command[i];
				}
			}

			if (foundName != "")
			{
				lines.Add(foundName);
			}

			for (int i = 0; i < lines.Count; i++)
			{
				var foundValue = variables[lines[i]];
				var regex = new Regex(Regex.Escape(lines[i]));
				commandWithoutFirstArg = regex.Replace(commandWithoutFirstArg, foundValue, 1);
			}

			return command.Split(" ")[0] + " " + commandWithoutFirstArg;
		}
	}
}
