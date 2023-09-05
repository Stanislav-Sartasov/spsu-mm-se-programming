using NUnit.Framework;
using WeatherRequesterResourceLibrary;

namespace WeatherRequesterResourceLibrary.UnitTests
{
	public class WeatherDataContainerUnitTests
	{
		private static string Name = "6e3HorNM";
		private static float Temp = 4.20F;
		private static int Humidity = 39;
		private static float WindSpeed = 5.552F;
		private static float WindDirection = 5.552F;
		private static PrecipitationTypes Type = PrecipitationTypes.None;
		private static float Probability = 0.006F;


		private WeatherDataContainer Data = new WeatherDataContainer
		{
			SourceName = Name,
			TempC = Temp,
			Humidity = Humidity,
			WindSpeed = WindSpeed,
			WindDirection = WindDirection,
			Precipitation = Type,
			PrecipitationProbability = Probability
		};

		[Test]
		public void ValueRetentionTest()
		{
			Assert.AreEqual(Name, Data.SourceName);
			Assert.AreEqual(Temp, Data.TempC);
			Assert.AreEqual(Humidity, Data.Humidity);
			Assert.AreEqual(WindSpeed, Data.WindSpeed);
			Assert.AreEqual(WindDirection, Data.WindDirection);
			Assert.AreEqual(Type, Data.Precipitation);
			Assert.AreEqual(Probability, Data.PrecipitationProbability);
		}
	}
}