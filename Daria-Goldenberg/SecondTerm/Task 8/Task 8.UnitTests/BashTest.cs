using NUnit.Framework;
using Moq;
using Task_8.Logger;
using System.IO;
using System;

namespace Task_8.UnitTests
{
	public class BashTest
	{
		[Test]
		public void BashRunTest()
		{
			string output = "";
			Mock<ILogger> mock = new Mock<ILogger>();
			mock.Setup(x => x.Write(It.IsAny<string>())).Callback((string x) => { output += x; });
			mock.SetupSequence(x => x.Read()).Returns("$a = \"first test file\"").Returns("cat a.txt").Returns("cat $a.txt").Returns("exit");

			Directory.SetCurrentDirectory("Files");
			Bash bash = new Bash(mock.Object);
			bash.Run();
			Directory.SetCurrentDirectory("..");

			Assert.AreEqual("Did not find the file a.txt." + Environment.NewLine + "https://petrathecat.github.io/smth/" + Environment.NewLine, output);
		}
	}
}
