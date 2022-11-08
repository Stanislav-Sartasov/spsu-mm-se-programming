using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using WeatherLib.ResponceReceiver;

namespace WeatherLibTests
{
	[TestClass]
	public class ResponceReceiverTests
	{
		[TestMethod]
		public async Task GetResponseTest()
		{
			ResponceReceiver receiver = new ResponceReceiver();
			await receiver.GetResponce("https://api.stormglass.io/v2/weather/point?lat=59.9386&lng=30.3141&params=airTemperature,cloudCover,humidity,precipitation,windWaveDirection,windSpeed&key=660840ca-c942-11ec-9863-0242ac130002-66084142-c942-11ec-9863-0242ac130002");
			Assert.IsTrue(receiver.IsSucceed);
			Assert.IsTrue(receiver.Responce != null);
		}

		[TestMethod]
		public async Task GetBadResponseTest()
		{
			ResponceReceiver receiver = new ResponceReceiver();
			await receiver.GetResponce("0");
			Assert.IsFalse(receiver.IsSucceed);
			Assert.IsTrue(receiver.Responce == null);
		}
	}
}
