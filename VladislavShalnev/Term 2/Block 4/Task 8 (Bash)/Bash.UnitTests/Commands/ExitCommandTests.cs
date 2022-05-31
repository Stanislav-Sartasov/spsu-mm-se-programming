using Bash.Exceptions;
using NUnit.Framework;

namespace Bash.Commands.UnitTests;

public class ExitCommandTests
{
	[Test]
	public void RunTest()
	{
		var exit = (int actual) => Assert.AreEqual(123, actual);
		
		Assert.Throws<ExitException>(() => new ExitCommand(exit).Run(new[] {"123"}));
	}
}