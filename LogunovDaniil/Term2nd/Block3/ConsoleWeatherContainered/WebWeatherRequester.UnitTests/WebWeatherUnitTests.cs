using NUnit.Framework;
using Moq;
using WeatherRequesterResourceLibrary;

namespace WebWeatherRequester.UnitTests
{
	public class WebWeatherUnitTests
	{
		private RequestDataContainer NormalRequest = new RequestDataContainer
		{
			ServiceURL = "https://google.com"
		};

		private RequestDataContainer UnauthorizedRequest = new RequestDataContainer
		{
			ServiceURL = "https://api.tomorrow.io/v4/timelines?units=metric&timesteps=1h&apikey=hahagetrektlmao"
		};

		private WeatherDataContainer HumidityData = new WeatherDataContainer
		{
			Humidity = 100500
		};

		[Test]
		public void RequestsDelayTest()
		{
			var mock = new Mock<IWebServiceHandler>();

			mock.Setup(x => x.GetServiceRequestInfo()).Returns(NormalRequest);
			mock.Setup(x => x.ParseJSONObject(It.IsAny<string>())).Returns(HumidityData);

			WebWeather weather = new WebWeather(mock.Object);
			weather.FetchWeatherData();
			System.Threading.Thread.Sleep(100);
			weather.FetchWeatherData();

			mock.Verify(mock => mock.ParseJSONObject(It.IsAny<string>()), Times.Once());

			Assert.Pass();
		}

		[Test]
		public void ErrorHandlingTest()
		{
			var mock = new Mock<IWebServiceHandler>();

			mock.Setup(x => x.GetServiceRequestInfo()).Returns(UnauthorizedRequest);

			WebWeather weather = new WebWeather(mock.Object);
			weather.FetchWeatherData();

			var status = weather.GetLastLog();
			Assert.NotNull(status);
			Assert.AreEqual(FetchWeatherStatus.Error, status.Status);
		}

		[Test]
		public void LastLogBehaviourTest()
		{
			var mock = new Mock<IWebServiceHandler>();

			mock.Setup(x => x.GetServiceRequestInfo()).Returns(NormalRequest);
			mock.Setup(x => x.ParseJSONObject(It.IsAny<string>())).Returns(HumidityData);

			WebWeather weather = new WebWeather(mock.Object);

			Assert.Null(weather.GetLastLog());
			weather.FetchWeatherData();

			var status = weather.GetLastLog();
			Assert.NotNull(status);
			Assert.AreEqual(FetchWeatherStatus.Success, status.Status);
		}

		[Test]
		public void FetchWeatherDataTest()
		{
			var mock = new Mock<IWebServiceHandler>();

			mock.Setup(x => x.GetServiceRequestInfo()).Returns(NormalRequest);
			mock.Setup(x => x.ParseJSONObject(It.IsAny<string>())).Returns(HumidityData);

			WebWeather weather = new WebWeather(mock.Object);

			var data = weather.FetchWeatherData();

			Assert.NotNull(data);
			Assert.AreEqual(HumidityData.Humidity, data.Humidity);
		}

		[Test]
		public void ParseErrorDataTest()
		{
			var mock = new Mock<IWebServiceHandler>();

			mock.Setup(x => x.GetServiceRequestInfo()).Returns(NormalRequest);
			mock.Setup(x => x.ParseJSONObject(It.IsAny<string>())).Returns((WeatherDataContainer?)null);

			WebWeather weather = new WebWeather(mock.Object);

			var data = weather.FetchWeatherData();
			Assert.Null(data);

			var log = weather.GetLastLog();
			Assert.NotNull(log);
			Assert.AreEqual(FetchWeatherStatus.Error, log.Status);
		}
	}
}