using Bash.App.BashComponents;
using NUnit.Framework;

namespace Bash.UnitTests.BashComponentsTests
{
	public class CommandSplitterTests
	{
		private CommandSplitter commandSplitter;

		[SetUp]
		public void SetUp()
		{
			commandSplitter = new CommandSplitter();
		}

		[Test]
		public void SpliteSingleCommandTest()
		{
			var result = commandSplitter.SplitCommands("sample meaningless command");

			Assert.AreEqual(new string[] { "sample meaningless command" }, result);

			Assert.Pass();
		}

		[Test]
		public void SpliteMultipleCommandsTest()
		{
			var result = commandSplitter.SplitCommands("sample meaningless command | sample meaningless command 2");

			Assert.AreEqual(new string[] { "sample meaningless command ", " sample meaningless command 2" }, result);

			Assert.Pass();
		}

		[Test]
		public void SpliteMultipleCommandsWithinQuatationTest()
		{
			var result = commandSplitter.SplitCommands("sample meaningless command \"| sample meaningless command 2\"");

			Assert.AreEqual(new string[] { "sample meaningless command \"| sample meaningless command 2\"" }, result);

			Assert.Pass();
		}
	}
}
