using NUnit.Framework;
using Sites;

namespace SitesTests
{
	public class StormglassIoTests
	{
		[Test]
		public void ParseTest()
		{
			StormglassIo testSite = new("https://api.stormglass.io/v2/weather/point?" +
				"lat=59.87595" +
				"&lng=29.82961" +
				"&params=windDirection,windSpeed,cloudCover,humidity,airTemperature,precipitation" +
				"&source=sg" +
				$"&start=2022-11-01" +
				$"&end=2022-11-01" +
				"&key=582a4100-601a-11ed-bc36-0242ac130002-582a416e-601a-11ed-bc36-0242ac130002");

			var testWeather = testSite.Parse();
			Assert.AreEqual(4.47, testWeather.TempC);
			Assert.AreEqual("100", testWeather.Cloudcover);
			Assert.AreEqual("87.22", testWeather.Humidity);
			Assert.AreEqual("0.1", testWeather.PrecipitationIntensity);
			Assert.AreEqual("170.91", testWeather.WindDirection);
			Assert.AreEqual("2.53", testWeather.WindSpeed);

			Assert.Pass();
		}
	}
}