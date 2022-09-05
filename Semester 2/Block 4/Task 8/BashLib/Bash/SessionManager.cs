using System;
using System.Collections.Generic;
using System.Linq;

namespace BashLib.Bash
{
	public class SessionManager
	{
		private Dictionary<string, string> variables = new Dictionary<string, string>();
		private string forbiddenSymbols = "\\|$?<>:;=";

		public ResolvedCommand ResolveCommand(string command)
		{
			var parts = command.Split(" ").Select(p => p.Trim()).ToArray();
			parts = ResolveParts(parts);

			var args = GetArguments(parts);

			if (args.Length != 0)
			{
				return new ResolvedCommand(args[0], args[1..]);
			}
			else
			{
				return null;
			}
		}

		private string[] ResolveParts(string[] parts)
		{
			for (int index = 0; index < parts.Length; index++)
			{
				if (parts[index].Length == 0 || parts[index][0] != '$')
					continue;

				parts[index] = parts[index][1..];

				if (parts[index].Contains('='))
				{
					var assigment = parts[index].Split('=');

					if (IsContainForbiddenSymbols(assigment))
						parts[index] = string.Empty;

					if (assigment[1][0] == '"' && assigment[1][assigment.Length - 1] != '"')
					{
						for (int i = index + 1; i < parts.Length; i++)
						{
							assigment[1] += ' ' + parts[i];
							if (parts[i].Contains('"'))
							{
								parts[i] = string.Empty;
								break;
							}
							parts[i] = string.Empty;
						}
					}

					SetVariable(assigment[0], assigment[1]);
					parts[index] = assigment[1];
				}
				else if (variables.ContainsKey(parts[index]))
				{
					parts[index] = variables[parts[index]];
				}
				else
				{
					var env = Environment.GetEnvironmentVariable(parts[index]);
					parts[index] = env != null ? env : string.Empty;
				}
			}

			return parts.Where(p => !string.IsNullOrWhiteSpace(p)).ToArray();
		}

		private bool IsContainForbiddenSymbols(string[] assigment)
		{
			foreach (var partOfAssigment in assigment)
				foreach (var symbol in partOfAssigment)
					if (forbiddenSymbols.Contains(symbol))
						return true;

			return false;
		}

		private void SetVariable(string name, string value)
		{
			if (variables.ContainsKey(name))
				variables[name] = value;
			else
				variables.Add(name, value);
		}

		private string[] GetArguments(string[] argsParts)
		{
			var argsString = string.Join(" ", argsParts.Where(p => !string.IsNullOrWhiteSpace(p)));
			var args = new List<string>();

			bool isInQuotes = false;
			string word = string.Empty;
			int count = 0;
			foreach (char symbol in argsString)
			{
				count++;

				if (symbol == '"')
				{
					isInQuotes = !isInQuotes;
					if (count != argsString.Length)
						continue;
				}

				if (symbol == ' ' && !isInQuotes)
				{
					args.Add(word);
					word = string.Empty;
					continue;
				}

				if (count == argsString.Length)
				{
					if (symbol != '"')
						word += symbol;
					args.Add(word);
					break;
				}

				word += symbol;
			}

			return args.ToArray();
		}
	}
}