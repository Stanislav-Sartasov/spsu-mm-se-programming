using Bash.Command;
using NUnit.Framework;
using System;
using System.IO;

namespace Bash.UnitTests.CommandTests
{
	public class PwdTests
	{
		CommandPwd executor;

		[SetUp]
		public void SetUp()
		{
			executor = new CommandPwd();
		}

		[Test]
		public void FolderWithFilesTest()
		{
			Directory.SetCurrentDirectory("TestCases/Case One");
			var results = executor.Execute(new string[0]);
			Directory.SetCurrentDirectory("../../");

			Assert.AreEqual(5, results.Length);
			Assert.IsTrue(results[0].EndsWith("TestCases\\Case One") || results[0].EndsWith("TestCases/Case One"));
		}
	}
}
