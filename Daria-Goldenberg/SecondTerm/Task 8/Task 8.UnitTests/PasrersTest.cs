using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using Task_8.Commands;

namespace Task_8.UnitTests
{
	public class PasrersTest
	{
		private StringWriter stringWriter;

		[SetUp]
		public void SetUp()
		{
			stringWriter = new StringWriter();
			Console.SetOut(stringWriter);
		}

		[Test]
		public void ParseArgumentsTest()
		{
			string arg = "$cat \"123\" 456";
			List<string> result = ArgumentParser.ParseArguments(arg);
			Assert.AreEqual(3, result.Count);
			Assert.AreEqual(result[0], "$cat");
			Assert.AreEqual(result[1], "123");
			Assert.AreEqual(result[2], "456");

			Assert.Pass();
		}

		[Test]
		public void ParseCommandsTest()
		{
			string arg = "never | gonna | give you up";
			List<string> result = CommandParser.ParseCommands(arg);
			Assert.AreEqual(result.Count, 3);
			Assert.AreEqual(result[0], "never ");
			Assert.AreEqual(result[1], " gonna ");
			Assert.AreEqual(result[2], " give you up");

			Assert.Pass();
		}

		[Test]
		public void SetVariableTest()
		{
			var variables = new Dictionary<string, string>()
			{
				{ "$var", "never" },
				{ "$v", "gonna" },
				{ "$variable", "give" }
			};

			string command = "echo $var $v $variable you up";
			VariableParser.SetVariable(ref command, variables);
			Assert.AreEqual(command, "echo never gonna give you up ");

			Assert.Pass();
		}

		[Test]
		public void SetVariableExceptionTest()
		{
			var variables = new Dictionary<string, string>()
			{
				{ "$var", "never" },
			};

			string command = "echo $v";
			try
			{
				VariableParser.SetVariable(ref command, variables);
			}
			catch (Exception ex)
			{
				Assert.AreEqual("Variable $v does not exist.", ex.Message);
			}

			Assert.Pass();
		}

		[TestCase("cat", "task.txt", "Did not find the file task.txt.")]
		[TestCase("wc", "task.txt", "Did not find the file task.txt.")]
		public void ExecuteTest(string command, string name, string output)
		{
			List<string> args = new List<string>() { command, name };
			CommandResult result = CommandExecutor.Execute(args);
			Assert.AreEqual(result.Errors[0], output);

			Assert.Pass();
		}

		[Test]
		public void ExecuteTest()
		{
			List<string> args = new List<string>() { "pwd" };
			Directory.SetCurrentDirectory("Files");
			CommandResult result = CommandExecutor.Execute(args);
			Directory.SetCurrentDirectory("..");
			Assert.AreEqual(result.Results.Count, 3);

			args = new List<string>() { "exit" };
			result = CommandExecutor.Execute(args);
			Assert.AreEqual(result.Errors[0], "exit");

			args = new List<string>() { "echo", "123", "456" };
			string correctOutput = "123 456 \r\n";
			result = CommandExecutor.Execute(args);
			Assert.AreEqual(stringWriter.ToString(), correctOutput);

			Assert.Pass();
		}
	}
}
