using System.IO;
using NUnit.Framework;

namespace Bash.Commands.UnitTests;

public class WcCommandTests
{
	[Test]
	public void RunTest()
	{
		Directory.SetCurrentDirectory("Assets");

		string[] actual = new WcCommand().Run(new [] { "test.txt" });
		string[] expected =
		{
			"filename\tlines\twords\tsize",
			"test.txt\t4\t4\t23 bytes"
		};
		
		Directory.SetCurrentDirectory("../");
		
		Assert.AreEqual(expected, actual);
	}
}