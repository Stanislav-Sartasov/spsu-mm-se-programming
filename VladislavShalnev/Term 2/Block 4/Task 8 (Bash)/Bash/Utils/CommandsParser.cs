using System.Text.RegularExpressions;
using Bash.Core;

namespace Bash.Utils;

internal static class CommandsParser
{
	internal static List<CommandObject> Parse(string commandsString)
	{
		var commands = new List<CommandObject>();

		foreach (var commandString in commandsString.Split("|"))
		{
			// "123 "test test" " => ["123", "test test"]
			string[] args = Regex
				.Split(commandString.Trim(), " (?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");

			commands.Add(new CommandObject
			{
				Name = args[0],
				Args = args[1..].ToList()
			});
		}
		
		return commands;
	}
}