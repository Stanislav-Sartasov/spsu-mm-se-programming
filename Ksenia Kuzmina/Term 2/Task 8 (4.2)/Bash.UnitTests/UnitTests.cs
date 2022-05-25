using NUnit.Framework;
using Bash.Commands;
using Moq;
using System.IO;
using System;

namespace Bash.UnitTests
{
	public class UnitTests
	{
		[Test]
		public void CatTest()
		{
			string[] args = { "test files/test.txt", "test files/thisfiledoesntexist.txt" };
			string[] lines = new Cat().RunCommand(args);
			Assert.AreEqual(1, lines.Length);
			Assert.Pass();
		}

		[Test]
		public void WcTest()
		{
			Directory.SetCurrentDirectory("test files");
			string[] args = { "test.txt", "thisfiledoesntexist.txt" };
			string[] lines = new Wc().RunCommand(args);
			Directory.SetCurrentDirectory("..");

			Assert.AreEqual(lines[0], "test.txt 1 1 0");
			Assert.Pass();
		}

		[Test]
		public void ExitTest()
		{
			string[] args = { "test files/test.txt", "test files/thiscatdoesntexist.txt" };
			bool didExit = false;

			Mock<IExiter> mock = new Mock<IExiter>();
			mock.Setup(e => e.Exit()).Callback(() => { didExit = true; });
			Exit exit = new Exit(mock.Object);
			exit.RunCommand(args);

			Assert.IsTrue(didExit);
			Assert.Pass();
		}

		[Test]
		public void EchoTest()
		{
			string result = "";
			Mock<ILogger> mock = new Mock<ILogger>();
			mock.Setup(e => e.Write(It.IsAny<string>())).Callback((string x) => { result += x; });

			string[] args = { "123" };
			string[] lines = new Echo(mock.Object).RunCommand(args);

			Assert.AreEqual("123 " + Environment.NewLine, result);
			Assert.AreEqual(lines.Length, 0);
			Assert.Pass();
		}

		[Test]
		public void PwdTest()
		{
			Directory.SetCurrentDirectory("test files");
			var lines = new Pwd().RunCommand(new string[0]);
			Directory.SetCurrentDirectory("..");
			Assert.AreEqual(lines[1], "test.txt");
			Assert.Pass();
		}

		[Test]
		public void ManagerSetValueTest()
		{
			VariableManager manager = new VariableManager();
			string result = manager.ProcessCommand("$var = 123");

			Assert.AreEqual(result, "$var = 123");
			Assert.IsTrue(DoesVariableExist("$var", "123", manager));

			result = manager.ProcessCommand("$var = 124");
			Assert.AreEqual(result, "$var = 124");
			Assert.IsTrue(DoesVariableExist("$var/", "124/", manager));

			Assert.Pass();
		}

		[Test]
		public void BashTest()
		{
			string result = "";

			Mock<ILogger> mockLogger = new Mock<ILogger>();
			mockLogger.Setup(e => e.Write(It.IsAny<string>())).Callback((string x) => { result += x; });
			mockLogger.SetupSequence(e => e.Input())
				.Returns("$var = test files")
				.Returns("cat \"$var/test2.txt\"")
				.Returns("wc \"$var/test2.txt\"")
				.Returns("echo $var")
				.Returns("123")
				.Returns("echo \"hello\"")
				.Returns("exit");

			Mock<IExiter> mockExiter = new Mock<IExiter>();
			mockExiter.Setup(e => e.Exit()).Throws(new Exception());

			Bash bash = new Bash(mockLogger.Object, mockExiter.Object);

			string rightOutput = "a b c d e f g h" + Environment.NewLine
				+ "test files/test2.txt 1 8 15" + Environment.NewLine
				+ "test files " + Environment.NewLine
				+ "Error starting the app" + Environment.NewLine
				+ "hello " + Environment.NewLine;

			try
			{
				bash.Run();
			}
			catch
			{
				Assert.AreEqual(rightOutput, result);
			}
		}

		private bool DoesVariableExist(string variableName, string correctValue, VariableManager manager)
		{
			string input = "_ " + variableName;
			input = manager.ProcessCommand(input);
			return input.Split(" ")[1].Trim() == correctValue.Trim();
		}
	}
}