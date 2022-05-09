using System;
using Bash.App.BashComponents;
using NUnit.Framework;

namespace Bash.UnitTests.BashComponentsTests
{
	public class SubcommandParserTests
	{
		private SubcommandParser parser;

		[SetUp]
		public void SetUp()
		{
			parser = new SubcommandParser();
		}

		[Test]
		public void SplittingNoQuatationTest()
		{
			var result = parser.SplitCommand("sample command text");
			Assert.AreEqual(new string[] { "sample", "command", "text" }, result);

			Assert.Pass();
		}

		[Test]
		public void SplittingWithQuatationTest()
		{
			var result = parser.SplitCommand("sample \"command text\"");
			Assert.AreEqual(new string[] { "sample", "command text" }, result);

			Assert.Pass();
		}
	}
}
