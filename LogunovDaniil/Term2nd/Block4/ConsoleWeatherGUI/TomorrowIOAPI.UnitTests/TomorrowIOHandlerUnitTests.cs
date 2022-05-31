using NUnit.Framework;
using System.IO;
using WeatherRequesterResourceLibrary;

namespace TomorrowIOAPI.UnitTests
{
	public class TomorrorIOHandlerUnitTests
	{
		private static string APIKey = "thisishorosho";

		[Test]
		public void ApiKeyTest()
		{
			TomorrowIOHandler tmrw = new TomorrowIOHandler(APIKey);
			string url = tmrw.GetServiceRequestInfo().ServiceURL;
			Assert.AreEqual(APIKey, url.Remove(0, url.Length - APIKey.Length));
		}

		[Test]
		public void ParseJSONTest()
		{
			string json;
			using (Stream stream = new FileStream("..\\..\\..\\TomorrowIOJSONExample.txt", FileMode.Open))
			{
				StreamReader streamReader = new StreamReader(stream);
				json = streamReader.ReadToEnd();
			}
			TomorrowIOHandler tmrw = new TomorrowIOHandler(APIKey);
			var data = tmrw.ParseJSONObject(json);
			Assert.IsNotNull(data);
			Assert.AreEqual(68, data.CloudCover);
			Assert.AreEqual(35, data.Humidity);
			Assert.AreEqual(0, data.PrecipitationProbability);
			Assert.AreEqual(PrecipitationTypes.None, data.Precipitation);
			Assert.AreEqual(6.5, data.TempC);
			Assert.AreEqual(WindDirections.W, data.WindDirection);
			Assert.AreEqual(5.38, data.WindSpeed);
		}

		[Test]
		public void ParseErrorJSONTest()
		{
			Assert.Null(new TomorrowIOHandler(APIKey).ParseJSONObject("mom's spaghetti"));
		}
	}
}