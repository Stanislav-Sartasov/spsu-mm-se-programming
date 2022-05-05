using NUnit.Framework;
using Weather;

namespace Weather.UnitTests
{
	public class WeatherDataUnitTests
	{
		[SetUp]
		public void Setup()
		{

		}

		[Test]
		public void WeatherDataCreationTest()
		{
			WeatherData weatherData = new WeatherData("tf", "tc", "cc", "hm", "pc", "wd", "ws", "testing");

			Assert.IsNotNull(weatherData);

			Assert.Pass();
		}

		[Test]
		public void WeatherDataIsNotEmptyTrueTest()
		{
			WeatherData weatherData = new WeatherData("tf", "tc", "cc", "hm", "pc", "wd", "ws", "testing");

			Assert.IsTrue(weatherData.IsNotEmpty());

			Assert.Pass();
		}

		[Test]
		public void WeatherDataIsNotEmptyFalseTest()
		{
			WeatherData weatherData = new WeatherData(null, null, null, null, null, null, null, "testing");

			Assert.IsFalse(weatherData.IsNotEmpty());

			Assert.Pass();
		}

		[Test]
		public void WeatherDataToStringTest()
		{
			WeatherData weatherData = new WeatherData("tf", "tc", "cc", "hm", "pc", "wd", "ws", "testing");

			string correctString = @"Source: testing
Temp (F): tf
Temp (C): tc
Cloud coverage(%): cc
Humidity: hm
Precipitation: pc
Wind Direction: wd
Wind Speed: ws";

			Assert.AreEqual(correctString, weatherData.ToString());

			Assert.Pass();
		}

		[Test]
		public void EmptyWeatherDataToStringTest()
		{
			WeatherData weatherData = new WeatherData(null, null, null, null, null, null, null, "testing");

			string correctString = @"Source: testing
Temp (F): No Data
Temp (C): No Data
Cloud coverage(%): No Data
Humidity: No Data
Precipitation: No Data
Wind Direction: No Data
Wind Speed: No Data";

			Assert.AreEqual(correctString, weatherData.ToString());

			Assert.Pass();
		}
	}
}