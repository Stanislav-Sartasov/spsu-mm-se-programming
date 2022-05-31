using System.Collections.Generic;
using Bash.Core;
using NUnit.Framework;

namespace Bash.Utils.UnitTests;

public class CommandsParserTests
{
	[Test]
	public void ParseTest()
	{
		string commandsString = "a 1 2 | b";
		
		var commands = new List<CommandObject>()
		{
			new() { Name = "a", Args = new List<string> {"1", "2"}},
			new() { Name = "b", Args = new List<string>() }
		};
		
		List<CommandObject> actual = CommandsParser.Parse(commandsString);

		for (int i = 0; i < actual.Count; i++)
		{
			Assert.AreEqual(commands[i].Name, actual[i].Name);
			Assert.That(commands[i].Args, Is.EquivalentTo(actual[i].Args));
		}
	}
}