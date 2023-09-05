using Bash.Command;
using NUnit.Framework;
using System.IO;

namespace Bash.UnitTests.CommandTests
{
	public class CdTests
	{
		CommandCd executor;

		[SetUp]
		public void SetUp()
		{
			executor = new CommandCd();
		}

		[Test]
		public void ChangeDirectoryTest()
		{
			var result = executor.Execute(new string[] { "TestCases" });
			string directory = Directory.GetCurrentDirectory();
			Directory.SetCurrentDirectory("..");
			Assert.IsTrue(directory.EndsWith("TestCases"));

			Assert.AreEqual(0, result.Length);
		}


		[Test]
		public void ChangeToIncorrectDirectoryTest()
		{
			var result = executor.Execute(new string[] { "NonExistantFolder" });
			string directory = Directory.GetCurrentDirectory();
			Assert.IsTrue(!directory.EndsWith("TestCases"));
			Assert.AreEqual(new string[] { "Could not find the path specified" }, result);
		}
	}
}
