using NUnit.Framework;

namespace WeatherRequesterResourceLibrary.UnitTests
{
	public class GeneralConversionsUnitTests
	{
		[Test]
		public void TemperatureConversionTest()
		{
			Assert.AreEqual(77, (int)GeneralConversions.ConvertTempFromCToF(25));
		}

		[Test]
		public void WindDirectionTest()
		{
			Assert.AreEqual(WindDirections.N, GeneralConversions.GetDirectionFromAngle(20));
			Assert.AreEqual(WindDirections.SE, GeneralConversions.GetDirectionFromAngle(130));
		}
	}
}
