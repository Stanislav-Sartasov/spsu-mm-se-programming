using NUnit.Framework;
using Bash.Command;
using System.IO;

namespace Bash.UnitTests
{
	public class WcTests
	{
		CommandWc executor;
		string path = "TestCases/Case One/";

		[SetUp]
		public void SetUp()
		{
			executor = new CommandWc();
		}

		[Test]
		public void NameGetTest()
		{
			Assert.AreEqual("wc", executor.Name);
			Assert.Pass();
		}

		[Test]
		public void NoArgumentsTest()
		{
			string[] result = executor.Execute(new string[0]);

			Assert.AreEqual(0, result.Length);
			Assert.Pass();
		}

		[Test]
		public void OneArgumentTest()
		{
			string[] result = executor.Execute(new string[] { path + "first.txt" });

			Assert.AreEqual(1, result.Length);
			var trueRes = path + "first.txt\t3\t1\t21";
			Assert.AreEqual(trueRes, result[0]);
			Assert.Pass();
		}

		[Test]
		public void FourArgumentTest()
		{
			string[] result = executor.Execute(new string[] { path + "first.txt", path + "second.txt", path + "third", path + "file number four.txt" });

			Assert.AreEqual(4, result.Length);
			var trueResults = new string[] { path + "first.txt\t3\t1\t21", path + "second.txt\t1\t3\t20", path + "third\t1\t1\t17", path + "file number four.txt\t1\t1\t4" };

			for (int i = 0; i < 4; i++)
				Assert.AreEqual(trueResults[i], result[i]);
			Assert.Pass();
		}

		[Test]
		public void FileNotFoundArgumentTest()
		{
			string[] result = executor.Execute(new string[] { path + "first.txt", path + "second.txt", path + "third", path + "file number four.txt", path + "nonexistant file.wav" });

			Assert.AreEqual(5, result.Length);
			var trueResults = new string[] { path + "first.txt\t3\t1\t21", path + "second.txt\t1\t3\t20", path + "third\t1\t1\t17", path + "file number four.txt\t1\t1\t4", "No such file or directory" };

			for (int i = 0; i < 5; i++)
				Assert.AreEqual(trueResults[i], result[i]);
			Assert.Pass();
		}
	}
}
