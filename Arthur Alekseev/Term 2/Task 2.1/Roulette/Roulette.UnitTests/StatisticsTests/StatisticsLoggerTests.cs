using System;
using System.IO;
using NUnit.Framework;

namespace Roulette.UnitTests.StatisticsTests
{
	public class StatisticsLoggerTests
	{
		private string _newLine;
		private TextWriter _originalOutput;
		private StringWriter _stringWriter;

		[SetUp]
		public void SetUp()
		{
			_stringWriter = new StringWriter();
			_originalOutput = Console.Out;
			Console.SetOut(_stringWriter);
			_newLine = _stringWriter.NewLine;
		}

		[Test]
		public void LogMessageTest()
		{
			StatisticsLogger.LogMessage("Sample text");
			Assert.AreEqual("Sample text" + _newLine, _stringWriter.ToString());

			Assert.Pass();
		}

		[Test]
		public void LogNewLine()
		{
			StatisticsLogger.LogNewLine();
			Assert.AreEqual(_newLine, _stringWriter.ToString());

			Assert.Pass();
		}

		[Test]
		public void LogMessageNoNewLineTest()
		{
			StatisticsLogger.LogMessageNoNewLine("Sample text");
			Assert.AreEqual("Sample text", _stringWriter.ToString());

			Assert.Pass();
		}

		[Test]
		public void LogHeaderTest()
		{
			StatisticsLogger.LogHeader("Sample text");
			Assert.AreEqual("\nSample text" + _newLine, _stringWriter.ToString());

			Assert.Pass();
		}

		[Test]
		public void LogBotNameTest()
		{
			StatisticsLogger.LogBotName("Sample text");
			Assert.AreEqual("Sample text", _stringWriter.ToString());

			Assert.Pass();
		}

		[Test]
		public void LogMoneyMoreTest()
		{
			StatisticsLogger.LogMoney(100, 50);
			Assert.AreEqual("100 (200%)", _stringWriter.ToString());

			Assert.Pass();
		}

		[Test]
		public void LogMoneyLessTest()
		{
			StatisticsLogger.LogMoney(100, 200);
			Assert.AreEqual("100 (50%)", _stringWriter.ToString());

			Assert.Pass();
		}
	}
}