using Bash.App;
using Bash.App.BashComponents;
using Bash.App.Output;
using Bash.ProcessStarter;
using Moq;
using NUnit.Framework;

namespace Bash.UnitTests.BashComponentsTests
{
	public class CommandExecutorTests
	{
		private CommandExecutor executor;
		private ILogger fakeLogger;
		private IProcessStarter processStarter;
		private string lastLoggerData;
		private string lastProcessStarterData;

		[SetUp]
		public void SetUp()
		{
			var mockLogger = new Mock<ILogger>();
			mockLogger.Setup(m => m.Log(It.IsAny<string>())).Callback<string>(x => { lastLoggerData += x; });
			fakeLogger = mockLogger.Object;
			lastLoggerData = "";

			var mockProcessStarter = new Mock<IProcessStarter>();
			mockProcessStarter.Setup(m => m.StartProcess(It.IsAny<string>(), It.IsAny<string>())).Callback((string x, string y) => { lastProcessStarterData += x + "|" + y; });
			processStarter = mockProcessStarter.Object;
			lastProcessStarterData = "";

			executor = new CommandExecutor(fakeLogger, processStarter);
		}

		[Test]
		public void EmptyCommandTest()
		{
			var result = executor.Execute(new string[0]);

			var lastLoggerDataCopy = lastLoggerData;
			var lastProcessStarterDataCopy = lastProcessStarterData;

			lastLoggerData = lastProcessStarterData = "";

			Assert.AreEqual("", lastLoggerDataCopy);
			Assert.AreEqual("", lastProcessStarterDataCopy);

			Assert.AreEqual(0, result.Length);
		}

		[Test]
		public void EchoCommandTest()
		{
			var result = executor.Execute(new string[] { "echo", "arg1", "arg2" });

			var lastLoggerDataCopy = lastLoggerData;
			var lastProcessStarterDataCopy = lastProcessStarterData;

			lastLoggerData = lastProcessStarterData = "";

			Assert.AreEqual("arg1 arg2 ", lastLoggerDataCopy);
			Assert.AreEqual("", lastProcessStarterDataCopy);

			Assert.AreEqual(0, result.Length);
		}

		[Test]
		public void AppStartTest()
		{
			var result = executor.Execute(new string[] { "unknowncommand", "arg1", "arg2" });

			var lastLoggerDataCopy = lastLoggerData;
			var lastProcessStarterDataCopy = lastProcessStarterData;

			lastLoggerData = lastProcessStarterData = "";

			Assert.AreEqual("", lastLoggerDataCopy);
			Assert.AreEqual("unknowncommand|arg1 arg2", lastProcessStarterDataCopy);

			Assert.AreEqual(0, result.Length);
		}
	}
}
