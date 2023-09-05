using Bash.App;
using Bash.App.Output;
using Bash.Command;
using Moq;
using NUnit.Framework;
using System;

namespace Bash.UnitTests.CommandTests
{
	public class EchoTests
	{
		CommandEcho executor;
		string fakeLoggerLastMessage;

		[SetUp]
		public void SetUp()
		{
			fakeLoggerLastMessage = "";
			var mock = new Mock<ILogger>();
			mock.Setup(m => m.Log(It.IsAny<string>())).Callback<string>(x => { fakeLoggerLastMessage += x; });

			executor = new CommandEcho(mock.Object);
		}

		[Test]
		public void LogEmptyArgumentsTest()
		{
			var result = executor.Execute(new string[0]);
			var output = fakeLoggerLastMessage;
			fakeLoggerLastMessage = "";

			Assert.AreEqual(fakeLoggerLastMessage, "");
			Assert.AreEqual(0, result.Length);

			Assert.Pass();
		}

		[Test]
		public void LogSingleArgumentTest()
		{
			var result = executor.Execute(new string[] { "Hello world!" });
			var output = fakeLoggerLastMessage;
			fakeLoggerLastMessage = "";

			Assert.AreEqual(output, "Hello world! ");
			Assert.AreEqual(0, result.Length);

			Assert.Pass();
		}

		[Test]
		public void LogSeveralArgumentsTest()
		{
			var result = executor.Execute(new string[] { "Hello world!", "I love cheese", "", "I have no ideas" });
			var output = fakeLoggerLastMessage;
			fakeLoggerLastMessage = "";

			Assert.AreEqual(output, "Hello world! " + "I love cheese  " + "I have no ideas ");
			Assert.AreEqual(0, result.Length);

			Assert.Pass();
		}
	}
}
