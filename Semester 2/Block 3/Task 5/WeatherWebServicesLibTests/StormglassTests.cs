using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebServicesLib;

namespace WeatherWebServicesLibTests
{
	[TestClass]
	public class StormglassTests
	{
		[TestMethod]
		public void WrongKeyTest()
		{
			IWebService stormglass = new Stormglass("59.9386", "30.3141", "0");
			Assert.IsFalse(stormglass.PrintResponce());
		}

		[TestMethod]
		public void RightKeyTest()
		{
			IWebService stormglass = new Stormglass("59.9386", "30.3141", "660840ca-c942-11ec-9863-0242ac130002-66084142-c942-11ec-9863-0242ac130002");
			Assert.IsTrue(stormglass.PrintResponce());
		}
	}
}
