using NUnit.Framework;
using System.IO;
using WeatherRequesterResourceLibrary;

namespace StormglassIOAPI.UnitTests
{
	public class StromglassIOHandlerUnitTests
	{
		private static string APIKey = "stonks";

		[Test]
		public void ApiKeyTest()
		{
			StormglassIOHandler strm = new StormglassIOHandler(APIKey);
			var enumerate = strm.GetServiceRequestInfo().HeaderValues.GetEnumerator();
			Assert.True(enumerate.MoveNext());
			Assert.AreEqual(APIKey, enumerate.Current);
		}

		[Test]
		public void ParseJSONTest()
		{
			string json;
			using (Stream stream = new FileStream("..\\..\\..\\StormglassIOJSONExample.txt", FileMode.Open))
			{
				StreamReader streamReader = new StreamReader(stream);
				json = streamReader.ReadToEnd();
			}
			StormglassIOHandler tmrw = new StormglassIOHandler(APIKey);
			var data = tmrw.ParseJSONObject(json);
			Assert.IsNotNull(data);
			Assert.AreEqual(2, data.CloudCover);
			Assert.AreEqual(13, data.Humidity);
			Assert.AreEqual(0, data.PrecipitationVolumetric);
			Assert.AreEqual(PrecipitationTypes.None, data.Precipitation);
			Assert.AreEqual(31.32, data.TempC);
			Assert.AreEqual(WindDirections.S, data.WindDirection);
			Assert.AreEqual(6.87, data.WindSpeed);
		}

		[Test]
		public void ParseErrorJSONTest()
		{
			Assert.Null(new StormglassIOHandler(APIKey).ParseJSONObject("vomit on the sweater"));
		}
	}
}