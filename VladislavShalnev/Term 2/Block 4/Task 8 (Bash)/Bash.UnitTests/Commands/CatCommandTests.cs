using System;
using System.IO;
using NUnit.Framework;

namespace Bash.Commands.UnitTests;

public class CatCommandTests
{
	[Test]
	public void RunTest()
	{
		Directory.SetCurrentDirectory("Assets");

		string actual = new CatCommand().Run(new [] { "test.txt" })[0];
		string br = Environment.NewLine;
		
		string expected = $"test 123{br}42{br}{br}text";

		Directory.SetCurrentDirectory("../");
		
		Assert.AreEqual(expected, actual);
	}
}