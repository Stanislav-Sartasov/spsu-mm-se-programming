using NUnit.Framework;

namespace WeatherRequesterResourceLibrary.UnitTests
{
	public class FetchWeatherLogUnitTests
	{
		private static FetchWeatherStatus Status = FetchWeatherStatus.Success;
		private static string Error = "broiler 747 terpit krushenie";

		private FetchWeatherLog Log = new FetchWeatherLog
		{
			Status = Status,
			Message = Error
		};

		[Test]
		public void ValueRetentionTest()
		{
			Assert.AreEqual(Status, Log.Status);
			Assert.AreEqual(Error, Log.Message);
		}
	}
}
