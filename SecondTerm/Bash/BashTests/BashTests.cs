using Commands;
using NUnit.Framework;
using System.Collections.Generic;
using Tools;
using Moq;

namespace BashTests
{
	public class BashTests
	{
		[Test]
		public void AddCommandTest()
		{
			Bash.Bash testBash = new(new Reader(), new Writer());
			var testEcho = new Echo();
			var dictionary = new Dictionary<string, Echo>()
			{
				{ "echo", testEcho }
			};
			testBash.AddCommand("echo", testEcho);

			Assert.AreEqual(dictionary, testBash.commands);

			testBash.AddCommand("echo", testEcho);

			Assert.AreEqual(dictionary, testBash.commands);


			Assert.Pass();
		}

		[Test]
		public void StartTest()
		{
			var mockWriter = new Mock<IWriter>();
			var mockReader = new Mock<IReader>();
			var mockCommand = new Mock<ICommand>();

			mockCommand.Setup(m => m.Run(It.IsAny<string[]>(), It.IsAny<string>(), It.IsAny<IWriter>())).Returns("testCommand");
			mockReader.SetupSequence(m => m.Read()).Returns("testCommand").Returns("exit");

			Bash.Bash bash = new(mockReader.Object, mockWriter.Object);
			bash.AddCommand("testCommand", mockCommand.Object);
			bash.Start();

			mockCommand.Verify(m => m.Run(It.IsAny<string[]>(), It.IsAny<string>(), It.IsAny<IWriter>()), Times.Once());
			mockWriter.Verify(m => m.Write(It.IsAny<string>()), Times.Once());

			Assert.Pass();
		}

		[Test]
		public void BashTest()
		{
			var bash = Bash.Bash.BashInit();

			Assert.IsNotNull(bash.commands);

			Assert.Pass();
		}
	}
}