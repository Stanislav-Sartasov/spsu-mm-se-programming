using Bash.App.BashComponents.Exceptions;
using Bash.Command;
using NUnit.Framework;

namespace Bash.UnitTests.CommandTests
{
	internal class ExitTests
	{
		private CommandExit executor;

		[SetUp]
		public void SetUp()
		{
			executor = new CommandExit();
		}

		[Test]
		public void ExitNoArgumnetsTest()
		{
			try
			{
				executor.Execute(new string[0]);
				Assert.Fail();
			}
			catch (ExitException ex)
			{
				Assert.AreEqual(ex.ExitCode, 0);
				Assert.Pass();
			}
		}

		[Test]
		public void ExitValidArgumnetTest()
		{
			try
			{
				executor.Execute(new string[] { "256" });
				Assert.Fail();
			}
			catch (ExitException ex)
			{
				Assert.AreEqual(ex.ExitCode, 256);
				Assert.Pass();
			}
		}

		[Test]
		public void ExitInvalidArgumnetTest()
		{
			try
			{
				executor.Execute(new string[] { "a" });
				Assert.Fail();
			}
			catch (ExitException ex)
			{
				Assert.AreEqual(ex.ExitCode, 0);
				Assert.Pass();
			}
		}
	}
}
