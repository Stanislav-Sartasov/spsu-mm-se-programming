using NUnit.Framework;
using Moq;
using System;

namespace Task_6.UnitTests
{
	public class SiteTest
	{
		[Test]
		public void GetDataFromOpenWeatherTest()
		{
			var fakeSite = new Mock<IRequest>();
			fakeSite.Setup(r => r.Connected).Returns(true);
			fakeSite.Setup(r => r.Response).Returns("{\"coord\":{\"lon\":30.2642,\"lat\":59.8944},\"weather\":[{\"id\":800,\"main\":\"Clear\",\"description\":\"clear sky\",\"icon\":\"01n\"}],\"base\":\"stations\",\"main\":{\"temp\":275.93,\"feels_like\":273.93,\"temp_min\":273.8,\"temp_max\":275.96,\"pressure\":1019,\"humidity\":45},\"visibility\":10000,\"wind\":{\"speed\":2,\"deg\":200},\"clouds\":{\"all\":0},\"dt\":1651174866,\"sys\":{\"type\":2,\"id\":197864,\"country\":\"RU\",\"sunrise\":1651111556,\"sunset\":1651168004},\"timezone\":10800,\"id\":498817,\"name\":\"Saint Petersburg\",\"cod\":200}");
			OpenWeather openWeather = new OpenWeather(fakeSite.Object);
			Weather data = openWeather.GetData();
			var correct = new Weather(2.78, 37, 0, 45, 2, "S", "Clear");

			Assert.AreEqual(correct.TemperatureCelsius, data.TemperatureCelsius);
			Assert.AreEqual(correct.Humidity, data.Humidity);
			Assert.AreEqual(correct.TemperatureFahrenheit, data.TemperatureFahrenheit);
			Assert.AreEqual(correct.CloudCover, data.CloudCover);
			Assert.AreEqual(correct.WindSpeed, data.WindSpeed);
			Assert.AreEqual(correct.WindDirection, data.WindDirection);
			Assert.AreEqual(correct.Precipitation, data.Precipitation);

			fakeSite.Setup(r => r.Connected).Returns(false);
			try
			{
				openWeather.GetData();
			}
			catch
			{
				Assert.Pass();
			}
			Assert.Fail();
		}

		[Test]
		public void GetDataFromTomorrowIoTest()
		{
			var fakeSite = new Mock<IRequest>();
			fakeSite.Setup(r => r.Connected).Returns(true);
			fakeSite.Setup(r => r.Response).Returns("{\"data\":{\"timelines\":[{\"timestep\":\"current\",\"endTime\":\"2022 - 04 - 28T20: 33:00Z\",\"startTime\":\"2022 - 04 - 28T20: 33:00Z\",\"intervals\":[{\"startTime\":\"2022 - 04 - 28T20: 33:00Z\",\"values\":{\"cloudCover\":100,\"humidity\":50,\"precipitationType\":2,\"temperature\":2.31,\"windDirection\":184.5,\"windSpeed\":2.63}}]}]}}");
			TomorrowIo tomorrowIo = new TomorrowIo(fakeSite.Object);
			Weather data = tomorrowIo.GetData();
			var correct = new Weather(2.31, 36.16, 100, 50, 2.63, "S", "Snow");

			Assert.AreEqual(correct.TemperatureCelsius, data.TemperatureCelsius);
			Assert.AreEqual(correct.Humidity, data.Humidity);
			Assert.AreEqual(correct.TemperatureFahrenheit, data.TemperatureFahrenheit);
			Assert.AreEqual(correct.CloudCover, data.CloudCover);
			Assert.AreEqual(correct.WindSpeed, data.WindSpeed);
			Assert.AreEqual(correct.WindDirection, data.WindDirection);
			Assert.AreEqual(correct.Precipitation, data.Precipitation);

			fakeSite.Setup(r => r.Connected).Returns(false);
			try
			{
				tomorrowIo.GetData();
			}
			catch
			{
				Assert.Pass();
			}
			Assert.Fail();
		}

		[TestCase(0, "Clear")]
		[TestCase(1, "Rain")]
		[TestCase(2, "Snow")]
		[TestCase(3, "Freezing Rain")]
		[TestCase(4, "Ice Pellets")]
		[TestCase(5, "Undefined")]
		public void CheckPrecipitationTypeTest(int type, string precipitation)
		{
			var fakeSite = new Mock<IRequest>();
			fakeSite.Setup(r => r.Connected).Returns(true);
			fakeSite.Setup(r => r.Response).Returns("{\"data\":{\"timelines\":[{\"timestep\":\"current\",\"endTime\":\"2022 - 04 - 28T20: 33:00Z\",\"startTime\":\"2022 - 04 - 28T20: 33:00Z\",\"intervals\":[{\"startTime\":\"2022 - 04 - 28T20: 33:00Z\",\"values\":{\"cloudCover\":100,\"humidity\":50,\"precipitationType\":" + Convert.ToString(type) + ",\"temperature\":2.31,\"windDirection\":184.5,\"windSpeed\":2.63}}]}]}}");
			TomorrowIo tomorrowIo = new TomorrowIo(fakeSite.Object);
			Weather data = tomorrowIo.GetData();

			var correct = new Weather(2.31, 36.16, 100, 50, 2.63, "S", precipitation);
			Assert.AreEqual(correct.Precipitation, data.Precipitation);

			Assert.Pass();
		}

		[TestCase(45, "NE")]
		[TestCase(90, "E")]
		[TestCase(135, "SE")]
		[TestCase(180, "S")]
		[TestCase(225, "SW")]
		[TestCase(270, "W")]
		[TestCase(315, "NW")]
		[TestCase(345, "N")]
		public void CheckDirectionTest(int deg, string direction)
		{
			var fakeSite = new Mock<IRequest>();
			fakeSite.Setup(r => r.Connected).Returns(true);
			fakeSite.Setup(r => r.Response).Returns("{\"data\":{\"timelines\":[{\"timestep\":\"current\",\"endTime\":\"2022 - 04 - 28T20: 33:00Z\",\"startTime\":\"2022 - 04 - 28T20: 33:00Z\",\"intervals\":[{\"startTime\":\"2022 - 04 - 28T20: 33:00Z\",\"values\":{\"cloudCover\":100,\"humidity\":50,\"precipitationType\":2,\"temperature\":2.31,\"windDirection\":" + deg + ",\"windSpeed\":2.63}}]}]}}");
			TomorrowIo tomorrowIo = new TomorrowIo(fakeSite.Object);
			Weather data = tomorrowIo.GetData();

			var correct = new Weather(2.31, 36.16, 100, 50, 2.63, direction, "Snow");
			Assert.AreEqual(correct.WindDirection, data.WindDirection);

			Assert.Pass();
		}
	}
}