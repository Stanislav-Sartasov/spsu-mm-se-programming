using NUnit.Framework;
using Moq;
using BashSimplified.IOLibrary;
using System.IO;
using System.Linq;

namespace BashSimplified.Commands.UnitTests
{
	public class CommandsTests
	{
		private string[] variousArgs = { "a-bra", "a-bra", "cada", "bra" };
		private string someInput = "we will we will ROCK YOU!";
		private string someTextPath = "..\\..\\..\\someText.txt";

		[Test]
		public void EchoTest()
		{
			var mock = new Mock<IWriter>();

			Assert.AreEqual(string.Join(" ", variousArgs), new Echo().Run(variousArgs, string.Empty, mock.Object));
			mock.Verify(mock => mock.WriteLine(It.IsAny<string>()), Times.Never);
		}

		[Test]
		public void PwdTest()
		{
			var mock = new Mock<IWriter>();

			Assert.AreEqual(Directory.GetCurrentDirectory(), new Pwd().Run(new string[0], string.Empty, mock.Object).Split("\n")[0]);
			mock.Verify(mock => mock.WriteLine(It.IsAny<string>()), Times.Never);
		}

		[Test]
		public void WcZeroArgumentsTest()
		{
			var mock = new Mock<IWriter>();

			var returned = new Wc().Run(new string[0], someInput, mock.Object).Split(" ").Where(x => x.Length > 0).ToArray();

			Assert.AreEqual($"{someInput.Count(x => x == '\n') + 1}", returned[0]);
			Assert.AreEqual($"{someInput.Count(x => x == ' ' || x == '\n') + 1}", returned[1]);
			Assert.AreEqual($"{someInput.Length}", returned[2]);
			mock.Verify(mock => mock.WriteLine(It.IsAny<string>()), Times.Never);
		}

		[Test]
		public void WcCorrectArgumentTest()
		{
			var mock = new Mock<IWriter>();

			var returned = new Wc().Run(new string[1] { someTextPath }, string.Empty, mock.Object).Split(" ").Where(x => x.Length > 0).ToArray();

			Assert.AreEqual("4", returned[0]);
			Assert.AreEqual("8", returned[1]);
			Assert.AreEqual("43", returned[2]);
			mock.Verify(mock => mock.WriteLine(It.IsAny<string>()), Times.Never);
		}

		[Test]
		public void WcIncorrectArgumentTest()
		{
			var mock = new Mock<IWriter>();

			var returned = new Wc().Run(new string[2] { "idonotexist", someTextPath }, string.Empty, mock.Object).Split(" ").Where(x => x.Length > 0).ToArray();

			Assert.AreEqual(0, returned.Length);
			mock.Verify(mock => mock.WriteLine(It.IsAny<string>()), Times.Once);
		}

		[Test]
		public void CatZeroArgumentsTest()
		{
			var mock = new Mock<IWriter>();

			Assert.AreEqual(someInput, new Cat().Run(new string[0], someInput, mock.Object));
			mock.Verify(mock => mock.WriteLine(It.IsAny<string>()), Times.Never);
		}

		[Test]
		public void CatCorrectArgumentTest()
		{
			var mock = new Mock<IWriter>();

			Assert.AreEqual(File.ReadAllText(someTextPath), new Cat().Run(new string[1] { someTextPath }, string.Empty, mock.Object));
			mock.Verify(mock => mock.WriteLine(It.IsAny<string>()), Times.Never);
		}

		[Test]
		public void CatIncorrectArgumentTest()
		{
			var mock = new Mock<IWriter>();

			Assert.AreEqual(string.Empty, new Cat().Run(new string[2] { "idonotexist", someTextPath }, string.Empty, mock.Object));
			mock.Verify(mock => mock.WriteLine(It.IsAny<string>()), Times.Once);
		}
	}
}