using NUnit.Framework;
using Weather;
using Moq;
using Weather.Parsers;
using System.Threading.Tasks;
using System.Net.Http;
using System;

namespace Weather.UnitTests
{
	public class Tests
	{
		[Test]
		public void GettingDataTest()
		{
			MyHttpClient myHttpClient = new MyHttpClient();
			var response = myHttpClient.GetData("https://api.openweathermap.org/data/2.5/weather?lon=2340.2642&lat=59.8944&units=metric&appid=");
			Assert.IsNotNull(response);
		}

		[Test]
		public async Task ConsoleOutputTest()
		{
			Weather weather = new Weather();
			var mock = new Mock<IHttpClient>();

			ConsoleOutput.OutputWeather(weather);

			mock.Setup(x => x.GetData(It.IsAny<string>())).Returns(getOpenWeatherMockData());
			var parserOpenWeather = new ParserOpenWeather(mock.Object);
			weather = await parserOpenWeather.GetWeatherInfoAsync();

			try
			{
				ConsoleOutput.OutputWeather(weather);
				ConsoleOutput.OutputExitMessage();
			}
			catch
			{
				Assert.Fail();
			}

			Assert.Pass();
		}

		[Test]
		public async Task OpenWeatherMapSuccess()
		{
			var weather = new Weather();
			var mock = new Mock<IHttpClient>();

			mock.Setup(x => x.GetData(It.IsAny<string>())).Returns(getOpenWeatherMockData());
			var parserOpenWeather = new ParserOpenWeather(mock.Object);
			weather = await parserOpenWeather.GetWeatherInfoAsync();

			Assert.IsNotNull(weather);
			Assert.AreEqual((int)weather.CelsiusTemperature, 283);
			Assert.AreEqual((int)weather.FahrenheitTemperature, 315);
			Assert.AreEqual(weather.Precipitation, "Clear");
			Assert.AreEqual(weather.Humidity, 47);
			Assert.AreEqual(weather.CloudCover, "0");
			Assert.AreEqual(weather.WindSpeed, 6);
			Assert.AreEqual(weather.WindDirection, "NE");

			mock.Setup(x => x.GetData(It.IsAny<string>())).Returns(getOpenWeatherMockSEData());
			weather = await parserOpenWeather.GetWeatherInfoAsync();

			Assert.AreEqual(weather.WindDirection, "SE");

			mock.Setup(x => x.GetData(It.IsAny<string>())).Returns(getOpenWeatherMockSWData());
			weather = await parserOpenWeather.GetWeatherInfoAsync();

			Assert.AreEqual(weather.WindDirection, "SW");

			mock.Setup(x => x.GetData(It.IsAny<string>())).Returns(getOpenWeatherMockNWData());
			weather = await parserOpenWeather.GetWeatherInfoAsync();

			Assert.AreEqual(weather.WindDirection, "NW");

			Assert.Pass();
		}

		[Test]
		public async Task OpenWeatherMapFail()
		{
			var weather = new Weather();
			var mock = new Mock<IHttpClient>();

			try
			{
				mock.Setup(x => x.GetData(It.IsAny<string>())).Returns(getFailedMockData());
				var parserOpenWeather = new ParserOpenWeather(mock.Object);
				weather = await parserOpenWeather.GetWeatherInfoAsync();
			}
			catch
			{
				Assert.Pass();
			}

			Assert.Fail();
		}

		[Test]
		public async Task TomorrowIoTest()
		{
			var weather = new Weather();
			var mock = new Mock<IHttpClient>();

			mock.Setup(x => x.GetData(It.IsAny<string>())).Returns(getTomorrowIoData());
			var parserTomorrowIo = new ParserTomorrowIo(mock.Object);
			weather = await parserTomorrowIo.GetWeatherInfoAsync();

			mock.Setup(x => x.GetData(It.IsAny<string>())).Returns(getFailedMockDataTomorrowIo());

			try
			{
				weather = await parserTomorrowIo.GetWeatherInfoAsync();
			}
			catch
			{
				Assert.Pass();
			}

			Assert.Fail();
		}

		[Test]
		public async Task TomorrowIoPrecipitation()
		{
			var weather = new Weather();
			var mock = new Mock<IHttpClient>();

			mock.Setup(x => x.GetData(It.IsAny<string>())).Returns(getTomorrowIoRainData());
			var parserTomorrowIo = new ParserTomorrowIo(mock.Object);
			weather = await parserTomorrowIo.GetWeatherInfoAsync();

			Assert.IsNotNull(weather);
			Assert.AreEqual((int)weather.CelsiusTemperature, 9);
			Assert.AreEqual((int)weather.FahrenheitTemperature, 41);
			Assert.AreEqual(weather.Precipitation, "Rain");
			Assert.AreEqual(weather.Humidity, 55);
			Assert.AreEqual(weather.CloudCover, "98");
			Assert.AreEqual((int)weather.WindSpeed, 5);
			Assert.AreEqual(weather.WindDirection, "NE");

			mock.Setup(x => x.GetData(It.IsAny<string>())).Returns(getTomorrowIoSnowData());
			weather = await parserTomorrowIo.GetWeatherInfoAsync();

			Assert.AreEqual(weather.Precipitation, "Snow");

			mock.Setup(x => x.GetData(It.IsAny<string>())).Returns(getTomorrowIoNoData());
			weather = await parserTomorrowIo.GetWeatherInfoAsync();

			Assert.AreEqual(weather.Precipitation, "Undefined");

			Assert.Pass();
		}

		private async Task<string> getFailedMockData()
		{
			return "{ \"cod\":\"400\",\"message\":\"wrong longitude\"}";
		}
		private async Task<string> getFailedMockDataTomorrowIo()
		{
			return "{\"code\":400002,\"type\":\"Invalid Query Parameters\",\"message\":\"The entries provided as query parameters were not valid for the request. Fix parameters and try again: .query.location ValidationError: Location should be a location id, or \\\"lat,lon\\\" value =  \\\"value\\\" failed custom validation because \\\"[0]\\\" must be less than or equal to 90\"}";
		}

		private async Task<string> getOpenWeatherMockData()
		{
			return "{\"coord\":{\"lon\":30.2642,\"lat\":59.8944},\"weather\":[{\"id\":800,\"main\":\"Clear\",\"description\":\"clear sky\",\"icon\":\"01d\"}],\"base\":\"stations\",\"main\":{\"temp\":283.84,\"feels_like\":282.19,\"temp_min\":283.74,\"temp_max\":284.88,\"pressure\":1021,\"humidity\":47},\"visibility\":10000,\"wind\":{\"speed\":6,\"deg\":70},\"clouds\":{\"all\":0},\"dt\":1650373810,\"sys\":{\"type\":2,\"id\":197864,\"country\":\"RU\",\"sunrise\":1650335482,\"sunset\":1650389066},\"timezone\":10800,\"id\":498817,\"name\":\"Saint Petersburg\",\"cod\":200}";
		}

		private async Task<string> getOpenWeatherMockSEData()
		{
			return "{\"coord\":{\"lon\":30.2642,\"lat\":59.8944},\"weather\":[{\"id\":800,\"main\":\"Clear\",\"description\":\"clear sky\",\"icon\":\"01d\"}],\"base\":\"stations\",\"main\":{\"temp\":283.84,\"feels_like\":282.19,\"temp_min\":283.74,\"temp_max\":284.88,\"pressure\":1021,\"humidity\":47},\"visibility\":10000,\"wind\":{\"speed\":6,\"deg\": 120},\"clouds\":{\"all\":0},\"dt\":1650373810,\"sys\":{\"type\":2,\"id\":197864,\"country\":\"RU\",\"sunrise\":1650335482,\"sunset\":1650389066},\"timezone\":10800,\"id\":498817,\"name\":\"Saint Petersburg\",\"cod\":200}";
		}

		private async Task<string> getOpenWeatherMockSWData()
		{
			return "{\"coord\":{\"lon\":30.2642,\"lat\":59.8944},\"weather\":[{\"id\":800,\"main\":\"Clear\",\"description\":\"clear sky\",\"icon\":\"01d\"}],\"base\":\"stations\",\"main\":{\"temp\":283.84,\"feels_like\":282.19,\"temp_min\":283.74,\"temp_max\":284.88,\"pressure\":1021,\"humidity\":47},\"visibility\":10000,\"wind\":{\"speed\":6,\"deg\": 190},\"clouds\":{\"all\":0},\"dt\":1650373810,\"sys\":{\"type\":2,\"id\":197864,\"country\":\"RU\",\"sunrise\":1650335482,\"sunset\":1650389066},\"timezone\":10800,\"id\":498817,\"name\":\"Saint Petersburg\",\"cod\":200}";
		}

		private async Task<string> getOpenWeatherMockNWData()
		{
			return "{\"coord\":{\"lon\":30.2642,\"lat\":59.8944},\"weather\":[{\"id\":800,\"main\":\"Clear\",\"description\":\"clear sky\",\"icon\":\"01d\"}],\"base\":\"stations\",\"main\":{\"temp\":283.84,\"feels_like\":282.19,\"temp_min\":283.74,\"temp_max\":284.88,\"pressure\":1021,\"humidity\":47},\"visibility\":10000,\"wind\":{\"speed\":6,\"deg\": 280},\"clouds\":{\"all\":0},\"dt\":1650373810,\"sys\":{\"type\":2,\"id\":197864,\"country\":\"RU\",\"sunrise\":1650335482,\"sunset\":1650389066},\"timezone\":10800,\"id\":498817,\"name\":\"Saint Petersburg\",\"cod\":200}";
		}

		private async Task<string> getTomorrowIoData()
		{
			return "{ \"data\":{ \"timelines\":[{ \"timestep\":\"current\",\"endTime\":\"2022-04-24T15:00:00Z\",\"startTime\":\"2022-04-24T15:00:00Z\",\"intervals\":[{ \"startTime\":\"2022-04-24T15:00:00Z\",\"values\":{ \"cloudCover\":98,\"humidity\":55,\"precipitationType\":0,\"temperature\":9.13,\"windDirection\":48.69,\"windSpeed\":5.69} }]}]} }";
		}

		private async Task<string> getTomorrowIoRainData()
		{
			return "{ \"data\":{ \"timelines\":[{ \"timestep\":\"current\",\"endTime\":\"2022-04-24T15:00:00Z\",\"startTime\":\"2022-04-24T15:00:00Z\",\"intervals\":[{ \"startTime\":\"2022-04-24T15:00:00Z\",\"values\":{ \"cloudCover\":98,\"humidity\":55,\"precipitationType\":1,\"temperature\":9.13,\"windDirection\":48.69,\"windSpeed\":5.69} }]}]} }";
		}

		private async Task<string> getTomorrowIoSnowData()
		{
			return "{ \"data\":{ \"timelines\":[{ \"timestep\":\"current\",\"endTime\":\"2022-04-24T15:00:00Z\",\"startTime\":\"2022-04-24T15:00:00Z\",\"intervals\":[{ \"startTime\":\"2022-04-24T15:00:00Z\",\"values\":{ \"cloudCover\":98,\"humidity\":55,\"precipitationType\":2,\"temperature\":9.13,\"windDirection\":48.69,\"windSpeed\":5.69} }]}]} }";
		}

		private async Task<string> getTomorrowIoNoData()
		{
			return "{ \"data\":{ \"timelines\":[{ \"timestep\":\"current\",\"endTime\":\"2022-04-24T15:00:00Z\",\"startTime\":\"2022-04-24T15:00:00Z\",\"intervals\":[{ \"startTime\":\"2022-04-24T15:00:00Z\",\"values\":{ \"cloudCover\":98,\"humidity\":55,\"precipitationType\":3,\"temperature\":9.13,\"windDirection\":48.69,\"windSpeed\":5.69} }]}]} }";
		}
	}
}