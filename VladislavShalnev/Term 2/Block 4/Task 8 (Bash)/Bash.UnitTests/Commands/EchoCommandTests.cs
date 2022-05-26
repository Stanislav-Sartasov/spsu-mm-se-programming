using Bash.Utils;
using Moq;
using NUnit.Framework;

namespace Bash.Commands.UnitTests;

public class EchoCommandTests
{
	[Test]
	public void RunTest()
	{
		string expected = "123 test test";
		
		var mock = new Mock<IIOManager>();
		mock
			.Setup(io =>
				io.Write(It.Is<string>(actual => actual == expected))
			)
			.Callback(Assert.Pass);

		new EchoCommand(mock.Object).Run(new[] { "123", "\"test test\"" });
		
		Assert.Fail();
	}
}