using Moq;
using NUnit.Framework;
using Weather;

namespace Weather.UnitTests
{
	public class OpenWeatherMapParserTests
	{
		[Test]
		public void OpenWeatherMapBadResponceTest()
		{
			var fakeWebParser = new Mock<IWebParser>();

			fakeWebParser.Setup(m => m.GetData(It.IsAny<string>())).Returns("{Some : super : strange : and : incorrect : data}");

			OpenWeatherMapParser parser = new OpenWeatherMapParser(fakeWebParser.Object);

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
		public void OpenWeatherMapGoodResponceTest()
		{
			var fakeWebParser = new Mock<IWebParser>();

			fakeWebParser.Setup(m => m.GetData(It.IsAny<string>())).Returns("{\"coord\":{\"lon\":30.2642,\"lat\":59.8944},\"weather\":[{\"id\":800,\"main\":\"Clear\",\"description\":\"clear sky\",\"icon\":\"01d\"}],\"base\":\"stations\",\"main\":{\"temp\":280,\"feels_like\":279,\"temp_min\":281.51,\"temp_max\":282.23,\"pressure\":1015,\"humidity\":58},\"visibility\":10000,\"wind\":{\"speed\":5,\"deg\":60},\"clouds\":{\"all\":0},\"dt\":1650707877,\"sys\":{\"type\":2,\"id\":197864,\"country\":\"RU\",\"sunrise\":1650680395,\"sunset\":1650735260},\"timezone\":10800,\"id\":498817,\"name\":\"Saint Petersburg\",\"cod\":200}");

			OpenWeatherMapParser parser = new OpenWeatherMapParser(fakeWebParser.Object);

			WeatherData data = parser.CollectData();
			var correctResponce = @"Source: openweathermap.org
Temp (F): 44,33
Temp (C): 6,85
Cloud coverage(%): 0
Humidity: 58
Precipitation: Clear
Wind Direction: 60
Wind Speed: 5";

			Assert.AreEqual(correctResponce, data.ToString());

			Assert.Pass();
		}
	}
}
