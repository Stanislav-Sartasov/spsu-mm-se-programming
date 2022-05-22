using Moq;
using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherAPI;
using WeatherForecastModel;
using Weather;

namespace Weather.UnitTests
{
	public class WeatherTests
	{
		[Test]
		public void InitializeWeatherAPITest()
		{
			AWeatherAPI tomorrowAPI = new TomorrowAPI();
			AWeatherAPI stormGlass = new StormGlassAPI();
			HttpClient client = StormGlassAPI.Client;

			Assert.AreEqual(tomorrowAPI.URL, "https://api.tomorrow.io/v4/timelines?location=59.93863%2C%2030.31413&fields=temperature&fields=humidity&fields=windSpeed&fields=windDirection&fields=precipitationProbability&fields=cloudCover&units=metric&timesteps=current&timezone=Europe%2FMoscow&apikey=");
			Assert.AreEqual(tomorrowAPI.flag, true);
			Assert.AreEqual(stormGlass.URL, "https://api.stormglass.io/v2/weather/point?lat=59.93863&lng=30.31413&params=airTemperature,cloudCover,humidity,precipitation,windDirection,windSpeed");
			Assert.AreEqual(stormGlass.flag, true);

			Assert.AreEqual(TomorrowAPI.Client, client);
			Assert.AreEqual(StormGlassAPI.Client, client);
			Assert.AreEqual(AWeatherAPI.Client, client);
			Assert.AreEqual(IWeatherAPI.Client, null);
		}

		[Test]
		public void GetKeysTest()
		{
			string? tomorrowKey = Keys.TomorrowIoAPIkey;
			string? stormGlassKey = Keys.StormGlassIoAPIkey;
			Assert.Pass();
		}

		[Test]
		public async Task GetTomorrowWeatherModelTest()
		{
			IWeatherAPI tomorrowAPI = new TomorrowAPI();
			var weatherAPI = new Mock<IWeatherAPI>();
			weatherAPI
				.Setup(x => x.GetDataAsync())
				.ReturnsAsync("{\"data\":{\"timelines\":[{\"timestep\":\"current\",\"endTime\":\"2022 - 05 - 20T23: 02:00 + 03:00\",\"startTime\":\"2022 - 05 - 20T23: 02:00 + 03:00\",\"intervals\":[{\"startTime\":\"2022 - 05 - 20T23: 02:00 + 03:00\",\"values\":{\"cloudCover\":36,\"humidity\":62,\"precipitationProbability\":0,\"temperature\":6.19,\"windDirection\":62.69,\"windSpeed\":3.13}}]}]}}");

			WeatherModel gotModel = tomorrowAPI.GetWeatherModelAsync(await weatherAPI.Object.GetDataAsync());
			Assert.AreEqual(gotModel.Temperature, "6.19");
			Assert.AreEqual(gotModel.CloudCover, "36");
			Assert.AreEqual(gotModel.Humidity, "62");
			Assert.AreEqual(gotModel.PrecipitationProbability, "0");
			Assert.AreEqual(gotModel.WindDirection, "62.69");
			Assert.AreEqual(gotModel.WindSpeed, "3.13");
		}

		[Test]
		public async Task GetStormGlassWeatherModelTest()
		{
			IWeatherAPI stormGlassAPI = new StormGlassAPI();
			var weatherAPI = new Mock<IWeatherAPI>();
			weatherAPI
				.Setup(x => x.GetDataAsync())
				.ReturnsAsync("{\"hours\":[{\"airTemperature\":{\"sg\":11.73},\"cloudCover\":{\"sg\":0.0},\"humidity\":{\"sg\":33.98},\"precipitation\":{\"sg\":0.0},\"time\":\"2022 - 05 - 22T16: 00:00 + 00:00\",\"windDirection\":{\"sg\":35.22},\"windSpeed\":{\"sg\":3.71}}],\"meta\":{\"cost\":1,\"dailyQuota\":10,\"end\":\"2022 - 05 - 22 16:11\",\"lat\":59.93863,\"lng\":30.31413,\"params\":[\"airTemperature\",\"cloudCover\",\"humidity\",\"precipitation\",\"windDirection\",\"windSpeed\"],\"requestCount\":3,\"source\":[\"sg\"],\"start\":\"2022 - 05 - 22 16:00\"}}");

			WeatherModel gotModel = stormGlassAPI.GetWeatherModelAsync(await weatherAPI.Object.GetDataAsync());
			Assert.AreEqual(gotModel.Temperature, "11.73");
			Assert.AreEqual(gotModel.CloudCover, "0");
			Assert.AreEqual(gotModel.Humidity, "33.98");
			Assert.AreEqual(gotModel.PrecipitationProbability, "0");
			Assert.AreEqual(gotModel.WindDirection, "35.22");
			Assert.AreEqual(gotModel.WindSpeed, "3.71");
		}

		[Test]
		public void WriterTest()
		{
			ConsoleWriter.WriteException("test");
			ConsoleWriter.WriteGreeting();
			ConsoleWriter.WriteErrorTomorrow();
			ConsoleWriter.WriteErrorStormGlass();
			WeatherModel model = new WeatherModel()
			{
				Temperature = "1",
				CloudCover = "1",
				Humidity = "1",
				PrecipitationProbability = "1",
				WindDirection = "1",
				WindSpeed = "1"
			};
			ConsoleWriter.WriteWeatherForecast(model, "test");
			ConsoleWriter.WriteAboutKeybord();
			Assert.Pass();
		}
	}
}