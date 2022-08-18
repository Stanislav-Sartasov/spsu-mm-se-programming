using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherLib.Parsers;
using WeatherLib.ResponceReceiver;
using WeatherLib.Weather;
using Moq;

namespace WeatherLibTests
{
	[TestClass]
	public class OpenweatherTests
	{
		[TestMethod]
		public void GetWeatherForecastTest()
		{
			var receiver = new Mock<IResponceReceiver>();
			string json = "{\"coord\":{\"lon\":30.3141,\"lat\":59.9386},\"weather\":[{\"id\":800,\"main\":\"Clear\",\"description\":\"clear sky\",\"icon\":\"01d\"}],\"base\":\"stations\",\"main\":{\"temp\":17.22,\"feels_like\":16.72,\"temp_min\":17.22,\"temp_max\":18.05,\"pressure\":1013,\"humidity\":66},\"visibility\":10000,\"wind\":{\"speed\":4,\"deg\":280},\"clouds\":{\"all\":0},\"dt\":1654444431,\"sys\":{\"type\":2,\"id\":197864,\"country\":\"RU\",\"sunrise\":1654389819,\"sunset\":1654456263},\"timezone\":10800,\"id\":519690,\"name\":\"Novaya Gollandiya\",\"cod\":200}";
			receiver.Setup(x => x.Responce).Returns(json);
			receiver.Setup(x => x.IsSucceed).Returns(true);

			IWeatherService openweather = new Openweather();
			WeatherForecast forecast = openweather.GetWeatherForecast(receiver.Object);

			Assert.IsTrue(forecast.IsForecastReceived == true);
			Assert.IsTrue(forecast.Temperature == "17,22");
			Assert.IsTrue(forecast.CloudCover == "0");
			Assert.IsTrue(forecast.Humidity == "66");
			Assert.IsTrue(forecast.Precipitation == null);
			Assert.IsTrue(forecast.WindSpeed == "4");
			Assert.IsTrue(forecast.WindDirection == "280");
		}

		[TestMethod]
		public void GetUnreceivedForecastTest()
		{
			var receiver = new Mock<IResponceReceiver>();
			string json = null;
			receiver.Setup(x => x.Responce).Returns(json);
			receiver.Setup(x => x.IsSucceed).Returns(false);

			IWeatherService openweather = new Openweather();
			WeatherForecast forecast = openweather.GetWeatherForecast(receiver.Object);

			Assert.IsTrue(forecast.IsForecastReceived == false);
		}
	}
}