using System;
using System.IO;
using NUnit.Framework;

namespace Task_5.UnitTests
{
	public class LoggerTest
	{
		private StringWriter stringWriter;

		[SetUp]
		public void SetUp()
		{
			stringWriter = new StringWriter();
			Console.SetOut(stringWriter);
		}

		[Test]
		public void LogWeatherTest()
		{
			string correctOutput =
@"---OpenWeather---
Temperature: 5°C (41°F)
Wind: 2 m/s (S)
Cloud coverage: 100%
Precipitation: No Data
Humidity: 0%
";
			string name = "OpenWeather";
			var weather = new Weather(5, 41, 100, 0, 2, "S", null);
			Logger.LogWeather(name, weather);
			Assert.AreEqual(correctOutput + Environment.NewLine, stringWriter.ToString());
			
			Assert.Pass();
		}

		[Test]
		public void LogErrorTest()
		{
			Exception ex = new Exception("Sample text");
			Logger.LogError(ex);
			Assert.AreEqual("Sample text" + Environment.NewLine, stringWriter.ToString());
			
			Assert.Pass();
		}
	}
}
