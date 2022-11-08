using NUnit.Framework;
using Sites;

namespace SitesTests
{
	public class StormglassIoTests
	{

		[Test]
		public void ParseTest()
		{
			StormglassIo testSite = new();

			var testWeather = testSite.Parse();
			Assert.IsNotNull(testWeather.TempC);
			Assert.IsNotNull(testWeather.Cloudcover);
			Assert.IsNotNull(testWeather.Humidity);
			Assert.IsNotNull(testWeather.PrecipitationIntensity);
			Assert.IsNotNull(testWeather.WindDirection);
			Assert.IsNotNull(testWeather.WindSpeed);

			Assert.Pass();
		}
	}
}