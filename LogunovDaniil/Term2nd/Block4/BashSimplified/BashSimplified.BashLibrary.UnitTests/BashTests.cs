using NUnit.Framework;
using BashSimplified.Commands;
using BashSimplified.IOLibrary;
using Moq;

namespace BashSimplified.BashLibrary.UnitTests
{
	public class BashTests
	{
		private string someOutput = "around the world";
		private string someAlias = "echo";
		private string exit = "exit";

		[Test]
		public void BashSimpleCommandTest()
		{
			var mExecutable = new Mock<IExecutable>();
			mExecutable.Setup(x => x.Run(It.IsAny<string[]>(), It.IsAny<string>(), It.IsAny<IWriter>()))
				.Returns(someOutput);

			var mWriter = new Mock<IWriter>();

			var mReader = new Mock<IReader>();
			mReader.SetupSequence(x => x.GetLine())
				.Returns(someAlias)
				.Returns(exit);

			var bash = new Bash(mReader.Object, mWriter.Object);
			bash.AddCommand(someAlias, mExecutable.Object);
			bash.StartMainLoop();

			mExecutable.Verify(x => x.Run(It.IsAny<string[]>(), It.IsAny<string>(), It.IsAny<IWriter>()), Times.Once());
			mWriter.Verify(x => x.WriteLine(It.IsAny<string>()), Times.Once());
		}

		[Test]
		public void BashUnknownCommandTest()
		{
			var mWriter = new Mock<IWriter>();

			var mReader = new Mock<IReader>();
			mReader.SetupSequence(x => x.GetLine())
				.Returns("somenonexistentcommandforsureyes")
				.Returns(exit);

			var bash = new Bash(mReader.Object, mWriter.Object);
			bash.StartMainLoop();

			mWriter.Verify(x => x.WriteLine(It.IsAny<string>()), Times.Exactly(2));
		}
	}
}