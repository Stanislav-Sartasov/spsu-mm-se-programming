using NUnit.Framework;
using Sites;

namespace SitesTests
{
	public class TomorrowIoTests
	{

		[Test]
		public void ParseTest()
		{
			TomorrowIo testSite = new("https://api.tomorrow.io/v4/timelines?" +
			"location=59.875957,29.829619&" +
			"fields=temperature&" +
			"fields=windSpeed&" +
			"fields=cloudCover&" +
			"fields=windDirection&" +
			"fields=precipitationIntensity&" +
			"fields=humidity&" +
			"timesteps=current&" +
			"apikey=SBpvOKHq9xdJPZ6G896fIEMzxFgASjoG");

			var testWeather = testSite.Parse();
			Assert.IsNotNull(testWeather.TempC);
			Assert.IsNotNull(testWeather.TempF);
			Assert.IsNotNull(testWeather.WindDirection);
			Assert.IsNotNull(testWeather.PrecipitationIntensity);
			Assert.IsNotNull(testWeather.Humidity);
			Assert.IsNotNull(testWeather.WindSpeed);
			Assert.IsNotNull(testWeather.Cloudcover);
			Assert.Pass();
		}
	}
}