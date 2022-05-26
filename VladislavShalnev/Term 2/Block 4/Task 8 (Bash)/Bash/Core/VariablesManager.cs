using System.Text.RegularExpressions;

namespace Bash.Core;

internal class VariablesManager
{
	internal Dictionary<string, string> Variables = new();
	
	private const string VariablePattern = @"\$([^\W\d]\w*)";
	private const string VariableAssignmentPattern = @"(\$([^\W\d]\w*)="
	                                                 + "((\"[^\"]*\")|([^ \"]*)))(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)";

	internal void ReadVariables(string commandString)
	{
		var matches = Regex.Matches(commandString, VariableAssignmentPattern);
		foreach (Match match in matches)
		{
			string[] values = match.ToString()[1..].Split('=');

			string key = values[0];
			string value = values[1];

			Variables[key] = ReplaceVariables(value);
		}
	}

	internal string ReplaceVariablesAssignments(string commandString)
	{
		string newCommandString = commandString;
		
		var matches = Regex.Matches(commandString, VariableAssignmentPattern);
		foreach (Match match in matches)
			newCommandString = newCommandString.Replace(match.ToString(), "");

		return newCommandString;
	}

	internal string ReplaceVariables(string commandString)
	{
		string newCommandString = commandString;
		bool isQuoted = Regex.IsMatch(newCommandString, "\"[^\"]*\"");
		
		var matches = Regex.Matches(commandString, VariablePattern);
		foreach (Match match in matches)
		{
			string name = match.ToString();
			string key = name[1..];

			newCommandString = newCommandString.Replace(name, 
				Variables.ContainsKey(key)
				? isQuoted ? Variables[key].Trim('"') : Variables[key] // $a="1 2", "3 $a 4" => "3 1 2 4" (not "3 "1 2" 4")
				: ""
			);
		}
		
		return newCommandString;
	}
}