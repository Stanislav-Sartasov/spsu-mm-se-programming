using Bash.App.BashComponents;
using Bash.App.BashComponents.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bash.App.BashComponents
{
	public class VariableManager
	{
		private List<Variable> variables = new List<Variable>();
		private string allowedCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_1234567890";

		// Returns true if variable assignment is detected, or false if no variable assignment is detected
		public bool ReplaceVariables(string[] commandParts)
		{
			// No need to replace the first argument, it is either a command name or a variable assignment
			for (int i = 1; i < commandParts.Count(); i++)
				commandParts[i] = ReplaceVariableInString(commandParts[i]);

			if (CheckForAssignment(commandParts))
				return true;

			return false;
		}

		private bool CheckForAssignment(string[] commandParts)
		{
			if(!commandParts.Contains("="))
				return false;

			int count = 0;
			foreach (var element in commandParts)
				if (element != "")
					count++;

			if (count != 3)
				throw new VariableAssignmentException();

			// If command starts with $ and has = it means variable assignment
			if (commandParts[0].StartsWith("$") && commandParts.Contains("="))
			{
				SetVariableValue(commandParts[0], commandParts[^1]);
				return true;
			}

			return false;
		}

		private void SetVariableValue(string varName, string varValue)
		{
			foreach (var variable in variables)
				if (variable.Name == varName)
				{
					variable.Value = varValue;
					return;
				}
			variables.Add(new Variable(varName, varValue));
		}

		private string ReplaceVariableInString(string argument)
		{
			string foundVariable = "$";
			for (int i = 0; i < argument.Length; i++)
			{
				if (argument[i] == '$')
					for (int j = i + 1; j < argument.Length + 1; j++)
						if (j != argument.Length && allowedCharacters.ToCharArray().Contains(argument[j]))
							foundVariable = foundVariable + argument[j];
						else
						{
							argument = TryReplacingVariable(argument, foundVariable);
							foundVariable = "$";
							break;
						}
			}

			return argument;
		}

		private string TryReplacingVariable(string argument, string foundVariable)
		{
			var regex = new Regex(Regex.Escape(foundVariable));
			foreach (var variable in variables)
			{
				if (foundVariable == variable.Name)
					return regex.Replace(argument, variable.Value, 1);
			}
			throw new VariableNotFoundException(foundVariable);
		}
	}
}
