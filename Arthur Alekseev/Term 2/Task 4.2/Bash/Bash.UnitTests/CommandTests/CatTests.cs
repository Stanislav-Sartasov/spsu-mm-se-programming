﻿using NUnit.Framework;
using Bash.Command;
using System.IO;
using System;

namespace Bash.UnitTests
{
	public class CatTests
	{
		CommandCat executor;
		string path = "TestCases/Case One/";

		[OneTimeSetUp]
		public void SetUp()
		{
			executor = new CommandCat();
		}

		[Test]
		public void NameGetTest()
		{
			Assert.AreEqual("cat", executor.Name);
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

			Assert.AreEqual(3, result.Length);
			var trueRes = @"first
file
contents";
			for (int i = 0; i < 3; i++)
				Assert.AreEqual(trueRes.Split(Environment.NewLine)[i], result[i]);
			Assert.Pass();
		}

		[Test]
		public void FourArgumentTest()
		{
			string[] result = executor.Execute(new string[] { path + "first.txt", path + "second.txt", path + "third", path + "file number four.txt" });

			Assert.AreEqual(6, result.Length);
			var trueResults = new string[] { @"first", @"file", @"contents", @"second file contents", @"thirdFileContents", @"FOUR" };

			for (int i = 0; i < 4; i++)
				Assert.AreEqual(trueResults[i], result[i]);
			Assert.Pass();
		}

		[Test]
		public void FileNotFoundArgumentTest()
		{
			string[] result = executor.Execute(new string[] { path + "first.txt", path + "second.txt", path + "third", path + "file number four.txt", path + "nonexistant file.wav" });

			Assert.AreEqual(7, result.Length);
			var trueResults = new string[] { @"first", @"file", @"contents", @"second file contents", @"thirdFileContents", @"FOUR", "No such file or directory" };

			for (int i = 0; i < 5; i++)
				Assert.AreEqual(trueResults[i], result[i]);
			Assert.Pass();
		}
	}
}
