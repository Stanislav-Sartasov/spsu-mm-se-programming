using NUnit.Framework;
using Bash.App;
using Bash.ProcessStarter;
using Moq;
using Bash.App.BashComponents.Exceptions;
using Bash.App.Output;
using System;

namespace Bash.UnitTests
{
	public class BashTests
	{
		private App.Bash bash;
		private Mock<ILogger> mockLogger;
		private ILogger fakeLogger;
		private IProcessStarter processStarter;
		private string lastLoggerData;
		private string lastProcessStarterData;
		private string bashIntroduction = "Task 8 (Bash)\nThe program has these commands:" +
				"\n1) exit [code] - exits the application with code specified" +
				"\n2) cat [file1] [file2] .. [fileN] - returns contents of file specified, if file does not exist returns \"No such file or directory\"" +
				"\n3) wc [file1] [file2] .. [fileN] - returns lines, words and byte length of the files specified, if file does not exist returns \"No such file or directory\"" +
				"\n4) cd [directory] - changes the directory to absolute or relative directory specified, returns nothing if successful, returns  \"Could not find the path specified\"" +
				"\n5) echo [arg1] [arg2] .. [argN] - return nothing, prints arguments to standart output" +
				"\n6) pwd - returns name of directory and all the files within" +
				"\n7) if no command has been recognised, app with the name wil be tried to start. Later arguments will be passed to the app" +
				"\nThe application supports assigning local variables like this:$varname = value" +
				"\n| is used to do a command pipeline, used like this: [cmd1] | [cmd2] | ..., outputs of cmd1 will become inputs of cmd2.\n";
		[SetUp]
		public void SetUp()
		{
			mockLogger = new Mock<ILogger>();
			mockLogger.Setup(m => m.Log(It.IsAny<string>())).Callback<string>(x => { lastLoggerData += x; });
			fakeLogger = mockLogger.Object;
			lastLoggerData = "";

			var mockProcessStarter = new Mock<IProcessStarter>();
			mockProcessStarter.Setup(m => m.StartProcess(It.IsAny<string>(), It.IsAny<string>())).Callback((string x, string y) => { lastProcessStarterData += x + "|" + y; });
			processStarter = mockProcessStarter.Object;
			lastProcessStarterData = "";

			bash = new App.Bash(fakeLogger, processStarter);
		}

		[Test]
		public void CorrectIntroductionTest()
		{
			mockLogger.Setup(m => m.Read()).Returns("exit 0");

			try
			{
				bash.Run();
			}
			catch (ExitException) { }

			var lastLoggerDataCopy = lastLoggerData;
			var lastProcessStarterDataCopy = lastProcessStarterData;

			lastLoggerData = lastProcessStarterData = "";
			Assert.AreEqual(bashIntroduction, lastLoggerDataCopy);

			Assert.Pass();
		}

		[Test]
		public void VariableAssignmentRewritesOutputTest()
		{
			mockLogger.SetupSequence(m => m.Read()).Returns("$a = 123").Returns("echo $a").Returns("exit 0");

			try
			{
				bash.Run();
			}
			catch (ExitException) { }

			var lastLoggerDataCopy = lastLoggerData;
			var lastProcessStarterDataCopy = lastProcessStarterData;

			lastLoggerData = lastProcessStarterData = "";
			Assert.AreEqual(bashIntroduction + Environment.NewLine + "123  " + Environment.NewLine, lastLoggerDataCopy);

			Assert.Pass();
		}

		[Test]
		public void BadVariableAssignmentExceptionTest()
		{
			// To stop bash from iteration infinitely
			mockLogger.SetupSequence(m => m.Read()).Returns("$a = bad assignment").Returns("exit 0");

			try
			{
				bash.Run();
			}
			catch (ExitException) { }

			var lastLoggerDataCopy = lastLoggerData;
			var lastProcessStarterDataCopy = lastProcessStarterData;

			lastLoggerData = lastProcessStarterData = "";
			Assert.AreEqual(bashIntroduction + new VariableAssignmentException().Message + Environment.NewLine, lastLoggerDataCopy);

			Assert.Pass();
		}
	}
}
