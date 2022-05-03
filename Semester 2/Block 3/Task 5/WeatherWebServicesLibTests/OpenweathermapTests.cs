using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebServicesLib;

namespace WeatherWebServicesLibTests
{
	[TestClass]
	public class OpenweathermapTests
	{
		[TestMethod]
		public void WrongKeyTest()
		{
			IWebService openweathermap = new Openweathermap("59.9386", "30.3141", "0");
			Assert.IsFalse(openweathermap.PrintResponce());
		}

		[TestMethod]
		public void RightKeyTest()
		{
			IWebService openweathermap = new Openweathermap("59.9386", "30.3141", "612ff6587b1aa1997d794833bb3c37ee");
			Assert.IsTrue(openweathermap.PrintResponce());
		}
	}
}
