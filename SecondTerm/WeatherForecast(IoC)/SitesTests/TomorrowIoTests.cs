using NUnit.Framework;
using Sites;

namespace SitesTests
{
	public class TomorrowIoTests
	{

		[Test]
		public void ParseTest()
		{
			TomorrowIo testSite = new();

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