using NUnit.Framework;
using Moq;

namespace WeatherUI.Weather.UnitTests
{
	internal class TomorrowIoParserTests
	{
		[Test]
		public void TomorrowIoBadResponceTest()
		{
			var fakeWebParser = new Mock<IWebParser>();

			fakeWebParser.Setup(m => m.GetData(It.IsAny<string>())).Returns("{Some : super : strange : and : incorrect : data}");

			TomorrowIoParser parser = new TomorrowIoParser(fakeWebParser.Object);

			try
			{
				parser.CollectData();
			}
			catch (EmptyWeatherDataException)
			{
				Assert.Pass();
			}

			Assert.Fail();
		}

		[Test]
		public void TomorrowIoGoodResponceTest()
		{
			var fakeWebParser = new Mock<IWebParser>();

			fakeWebParser.Setup(m => m.GetData(It.IsAny<string>())).Returns("{\"data\":{\"timelines\":[{\"timestep\":\"current\",\"endTime\":\"2022-04-19T19:46:00Z\",\"startTime\":\"2022-04-19T19:46:00Z\",\"intervals\":[{\"startTime\":\"2022-04-19T19:46:00Z\",\"values\":{\"cloudCover\":10,\"humidity\":20,\"precipitationType\":1,\"temperature\":30,\"windDirection\":40,\"windSpeed\":50}}]}]}}");

			TomorrowIoParser parser = new TomorrowIoParser(fakeWebParser.Object);

			WeatherData data = parser.CollectData();
			var correctResponce = @"Source: tomorrow.io
Temp (F): 86,00
Temp (C): 30,00
Cloud coverage(%): 10
Humidity: 20
Precipitation: Rain
Wind Direction: 40
Wind Speed: 50";

			Assert.AreEqual(correctResponce, data.ToString());

			Assert.Pass();
		}

	}
}
