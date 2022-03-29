using System;
using System.IO;
using NUnit.Framework;

namespace Roulette.UnitTests.StatisticsTests
{
	public class StatisticCollectorTests
	{
		private string _newLine;
		private StringWriter _stringWriter;

		[SetUp]
		public void SetUp()
		{
			_stringWriter = new StringWriter();
			Console.SetOut(_stringWriter);
			_newLine = _stringWriter.NewLine;
		}

		[Test]
		public void CreateTest()
		{
			var statisticCollector = new StatisticCollector();
			Assert.IsNotNull(statisticCollector);

			Assert.Pass();
		}

		[Test]
		public void CollectStatistics()
		{
			var statisticCollector = new StatisticCollector();
			statisticCollector.CollectStatistics();

			Assert.Pass();
		}

		[Test]
		public void LogStatisticsTest()
		{
			var statisticCollector = new StatisticCollector();

			statisticCollector.LogStatistics();

			Assert.IsNotEmpty(_stringWriter.ToString());

			Assert.Pass();
		}
	}
}