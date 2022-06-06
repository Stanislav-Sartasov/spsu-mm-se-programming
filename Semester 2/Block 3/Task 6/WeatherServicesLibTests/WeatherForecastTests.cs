using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherServicesLib;
using ResponceReceiverLib;
using System.IO;
using System;
using Moq;

namespace WeatherServicesLibTests
{
	[TestClass]
	public class WeatherForecastTests
	{
		[TestMethod]
		public void WeatherForecastTest()
		{
			var receiver = new Mock<IResponceReceiver>();
			string json = "{\"coord\":{\"lon\":30.3141,\"lat\":59.9386},\"weather\":[{\"id\":800,\"main\":\"Clear\",\"description\":\"clear sky\",\"icon\":\"01d\"}],\"base\":\"stations\",\"main\":{\"temp\":17.22,\"feels_like\":16.72,\"temp_min\":17.22,\"temp_max\":18.05,\"pressure\":1013,\"humidity\":66},\"visibility\":10000,\"wind\":{\"speed\":4,\"deg\":280},\"clouds\":{\"all\":0},\"dt\":1654444431,\"sys\":{\"type\":2,\"id\":197864,\"country\":\"RU\",\"sunrise\":1654389819,\"sunset\":1654456263},\"timezone\":10800,\"id\":519690,\"name\":\"Novaya Gollandiya\",\"cod\":200}";
			receiver.Setup(x => x.Responce).Returns(json);
			receiver.Setup(x => x.IsSucceed).Returns(true);

			IWeatherService openweather = Container.CreateWeatherService(WeatherServices.Openweather);
			WeatherForecast forecast = openweather.GetWeatherForecast(receiver.Object);

			StringWriter sw = new StringWriter();
			Console.SetOut(sw);

			forecast.Print();

			Assert.IsTrue(sw.ToString() == "Air temperature: 17,22°С, 62,995999999999995°F\r\nCloud cover: 0%\r\nhumidity: 66%\r\nprecipitation: no data on this service\r\nwindSpeed: 4m/s\r\nwindDirection: 280°\r\n");
		}
	}
}
