using System;
using System.Collections.Generic;
using System.IO;
using Bash.Utils;
using Moq;
using NUnit.Framework;

namespace Bash.Core.UnitTests;

public class BashTests
{
	[Test]
	public void RunTest()
	{
		Directory.SetCurrentDirectory("Assets");
		
		var mock = new Mock<IIOManager>();
		
		mock
			.SetupSequence(io => io.Read())
			.Returns("$a=123 $b=$a")
			.Returns("echo \"$b=test\"")
			.Returns("cat test.txt")
			.Returns("wc test.txt")
			.Returns("test 123 456")
			.Returns("cat test.txt | echo")
			.Returns("exit 0");

		var actual = new List<string>();

		mock
			.Setup(io => io.Write(It.IsAny<string>()))
			.Callback((string message) => actual.Add(message));

		string br = Environment.NewLine;
		List<string> expected = new List<string>
		{
			"123=test",
			$"test 123{br}42{br}{br}text",
			"filename\tlines\twords\tsize",
			"test.txt\t4\t4\t23 bytes",
			"test 123 456",
			$"test 123{br}42{br}{br}text",
			"exit 0"
		};

		Func<string, IEnumerable<string>, object?> startProcess = (name, args) =>
		{
			actual.Add($"{name} {string.Join(' ', args)}");
			return null;
		};
		
		var exit = (int code) =>
			actual.Add($"exit {code}");

		new Bash(mock.Object, exit, startProcess).Run();
		
		Directory.SetCurrentDirectory("../");
		
		Assert.AreEqual(expected, actual);
	}
}