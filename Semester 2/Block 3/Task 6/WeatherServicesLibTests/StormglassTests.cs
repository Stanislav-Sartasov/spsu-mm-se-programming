using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherServicesLib;
using ResponceReceiverLib;
using Moq;

namespace WeatherServicesLibTests
{
	[TestClass]
	public class StormglassTests
	{
		[TestMethod]
		public void GetWeatherForecastTest()
		{
			var receiver = new Mock<IResponceReceiver>();
			string json = "{\"hours\":[{\"airTemperature\":{\"dwd\":15.18,\"noaa\":14.71,\"sg\":15.18},\"cloudCover\":{\"dwd\":86.92,\"noaa\":90.1,\"sg\":86.92},\"humidity\":{\"dwd\":73.84,\"noaa\":59.07,\"sg\":73.84},\"precipitation\":{\"dwd\":0.0,\"noaa\":0.01,\"sg\":0.0},\"time\":\"2022-06-05T16:00:00+00:00\",\"windSpeed\":{\"icon\":5.67,\"noaa\":4.36,\"sg\":5.67},\"windWaveDirection\":{\"dwd\":265.71,\"icon\":271.41,\"meteo\":321.45,\"noaa\":272.27,\"sg\":321.45}}],\"meta\":{\"cost\":1,\"dailyQuota\":10,\"end\":\"2022-06-05 16:24\",\"lat\":59.9386,\"lng\":30.3141,\"params\":[\"airTemperature\",\"cloudCover\",\"humidity\",\"precipitation\",\"windWaveDirection\",\"windSpeed\"],\"requestCount\":1,\"start\":\"2022-06-05 16:00\"}}\n";

			receiver.Setup(x => x.Responce).Returns(json);
			receiver.Setup(x => x.IsSucceed).Returns(true);

			IWeatherService stormglass = Container.CreateWeatherService(WeatherServices.Stormglass);
			WeatherForecast forecast = stormglass.GetWeatherForecast(receiver.Object);

			Assert.IsTrue(forecast.IsForecastReceived == true);
			Assert.IsTrue(forecast.Temperature == "15,18");
			Assert.IsTrue(forecast.CloudCover == "86,92");
			Assert.IsTrue(forecast.Humidity == "73,84");
			Assert.IsTrue(forecast.Precipitation == "0");
			Assert.IsTrue(forecast.WindSpeed == "5,67");
			Assert.IsTrue(forecast.WindDirection == "321,45");
		}

		[TestMethod]
		public void GetUnreceivedForecastTest()
		{
			var receiver = new Mock<IResponceReceiver>();
			string json = null;
			receiver.Setup(x => x.Responce).Returns(json);
			receiver.Setup(x => x.IsSucceed).Returns(false);

			IWeatherService stormglass = new Stormglass();
			WeatherForecast forecast = stormglass.GetWeatherForecast(receiver.Object);

			Assert.IsTrue(forecast.IsForecastReceived == false);
		}
	}
}