using Commands;
using NUnit.Framework;
using System.IO;
using Tools;

namespace CommandsTests
{
	public class CommandTests
	{
		[Test]
		public void EchoRunTest()
		{
			string testCmd = "echo test test";
			var testEcho = new Echo();

			Assert.AreEqual("echo test test", testEcho.Run(testCmd.Split(" "), "", new Writer()));

			Assert.Pass();
		}

		[Test]
		public void CatRunTest()
		{
			var testCat = new Cat();
			string path = "..//..//..//test.txt";

			Assert.AreEqual(File.ReadAllText(path), testCat.Run(new string[1] { path }, "", new Writer()));

			Assert.Pass();
		}

		[Test]
		public void PwdRunTest()
		{
			var testPwd = new Pwd();
			string[] empty = null;
			string directory = testPwd.Run(empty, "", new Writer()).Split("\n")[0];

			Assert.AreEqual(Directory.GetCurrentDirectory(), directory);

			Assert.Pass();
		}

		[Test]
		public void WcRunTest()
		{
			var wc = new Wc();
			string path = "..//..//..//test.txt";
			string testData = wc.Run(new string[1] { path }, "", new Writer());
			string[] testDataArray = testData.Split(" ");

			Assert.AreEqual("1", testDataArray[0]);
			Assert.AreEqual("3", testDataArray[1]);
			Assert.AreEqual("5", testDataArray[2]);

			Assert.Pass();
		}
	}
}