using BashLib.IO;
using BashLib.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Moq;

namespace BashTests
{
	[TestClass]
	public class CommandsTests
	{
		private string textFile = Path.GetFullPath("..\\..\\..\\textFile.txt");

		[TestMethod]
		public void CatTest()
		{
			var writer = new Mock<IWriter>();

			var args = new string[] { textFile, "fff" };
			var targetOutput = "this is sample text file\n";

			var output = new Cat(writer.Object).Run(args);

			Assert.AreEqual(targetOutput, output);
			writer.Verify(w => w.WriteLine(It.IsAny<string>()), Times.Once);
		}

		[TestMethod]
		public void EchoTest()
		{
			var writer = new Mock<IWriter>();

			var args = new string[] { "echo", "can everything" };
			var targetOutput = "echo can everything";

			var output = new Echo().Run(args);

			Assert.AreEqual(targetOutput, output);
			writer.Verify(w => w.WriteLine(It.IsAny<string>()), Times.Never);
		}

		[TestMethod]
		public void PwdTest()
		{
			var writer = new Mock<IWriter>();

			var args = new string[] { };
			var targetOutput = Directory.GetCurrentDirectory();

			var output = new Pwd().Run(args);

			Assert.AreEqual(targetOutput, output.Split('\n')[0]);
			writer.Verify(w => w.WriteLine(It.IsAny<string>()), Times.Never);
		}

		[TestMethod]
		public void WcTest()
		{
			var writer = new Mock<IWriter>();

			var args = new string[] { textFile, "fff" };
			var targetOutput = $"{textFile}:\nlines - 1\nwords - 5\nbytes - 24";

			var output = new Wc(writer.Object).Run(args);

			Assert.AreEqual(targetOutput, output);
			writer.Verify(w => w.WriteLine(It.IsAny<string>()), Times.Once);
		}
	}
}