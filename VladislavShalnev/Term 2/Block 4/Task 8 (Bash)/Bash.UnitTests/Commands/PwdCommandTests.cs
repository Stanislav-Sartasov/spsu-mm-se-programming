using System.IO;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace Bash.Commands.UnitTests;

public class PwdCommandTests
{
	[Test]
	public void RunTest()
	{
		Directory.SetCurrentDirectory("Assets");

		string[] actual = new PwdCommand().Run();
		string[] expected = { "test.txt" };
		
		Directory.SetCurrentDirectory("../");
		
		Assert.IsTrue(Regex.IsMatch(actual[0], ".+Assets"));
		Assert.AreEqual(expected, actual[1..]);
	}
}