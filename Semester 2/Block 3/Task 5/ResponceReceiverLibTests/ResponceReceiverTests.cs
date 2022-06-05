using Microsoft.VisualStudio.TestTools.UnitTesting;
using ResponceReceiverLib;

namespace ResponceReceiverLibTests
{
	[TestClass]
	public class ResponceReceiverTests
	{
		[TestMethod]
		public void GetResponseTest()
		{
			ResponceReceiver receiver = new ResponceReceiver("https://api.openweathermap.org/data/2.5/weather?lat=59.9386&lon=30.3141&units=metric&appid=612ff6587b1aa1997d794833bb3c37ee");
			Assert.IsTrue(receiver.IsSucceed);
			Assert.IsTrue(receiver.Responce != null);
		}

		[TestMethod]
		public void GetBadResponseTest()
		{
			ResponceReceiver receiver = new ResponceReceiver("0");
			Assert.IsFalse(receiver.IsSucceed);
			Assert.IsTrue(receiver.Responce == null);
		}
	}
}
