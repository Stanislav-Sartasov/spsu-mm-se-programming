using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using Task_8.Commands;

namespace Task_8.UnitTests
{
	public class CommandTest
	{
		private StringWriter stringWriter;

		[SetUp]
		public void SetUp()
		{
			stringWriter = new StringWriter();
			Console.SetOut(stringWriter);
		}

		[Test]
		public void CatRunTest()
		{
			Directory.SetCurrentDirectory("Files");
			List<string> args = new List<string> { "first test file.txt", "third test file.txt" };
			CommandResult result = new Cat().Run(args);
			Directory.SetCurrentDirectory("..");
			Assert.AreEqual(result.Errors, new List<string> { "Did not find the file third test file.txt." });
			Assert.AreEqual(result.Results, new List<string> { "https://petrathecat.github.io/smth/" });

			Assert.Pass();
		}

		[Test]
		public void EchoRunTest()
		{
			List<string> args = new List<string> { "123", "456", "789" };
			CommandResult result = new Echo().Run(args);
			string correctOutput = "123 456 789 \r\n";
			Assert.AreEqual(stringWriter.ToString(), correctOutput);
			Assert.AreEqual(result.Errors, new List<string> { });
			Assert.AreEqual(result.Results, new List<string> { });

			Assert.Pass();
		}

		[Test]
		public void PWDRunTest()
		{
			Directory.SetCurrentDirectory("Files");
			List<string> args = new List<string> { "", "" };
			CommandResult result = new PWD().Run(args);
			Directory.SetCurrentDirectory("..");
			Assert.AreEqual(result.Results.Count, 3);
			Assert.AreEqual(result.Results[1], "first test file.txt");
			Assert.AreEqual(result.Results[2], "second test file.txt");

			Assert.Pass();
		}

		[Test]
		public void StartAppRunTest()
		{
			List<string> args = new List<string> { "0123" };
			CommandResult result = new StartApp().Run(args);
			Assert.AreEqual(result.Errors[0], "Process 0123 could not be started.");

			Assert.Pass();
		}

		[Test]
		public void WCRunTest()
		{
			Directory.SetCurrentDirectory("Files");
			List<string> args = new List<string> { "first test file.txt", "third test file.txt" };
			CommandResult result = new WC().Run(args);
			Directory.SetCurrentDirectory("..");
			Assert.AreEqual(result.Results[0], "1 1 35");
			Assert.AreEqual(result.Errors[0], "Did not find the file third test file.txt.");

			Assert.Pass();
		}

		[Test]
		public void ExitRunTest()
		{
			CommandResult result = new Exit().Run(new List<string> { "" });
			Assert.AreEqual(result.Errors[0], "exit");

			Assert.Pass();
		}
	}
}