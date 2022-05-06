using NUnit.Framework;
using System.Collections.Generic;

namespace Task_6.UnitTests
{
	public class IoCContainerTest
	{
		[Test]
		public void GetRequestTest()
		{
			string[] names = new string[] { "OpenWeather", "tomorrowio" };
			List<ISite> requests = IoCContainer.GetRequest(names);

			Assert.AreEqual(2, requests.Count);
			Assert.AreEqual(requests[0].Name, "OpenWeather");
			Assert.AreEqual(requests[1].Name, "TomorrowIo");

			names = new string[] { "Something", "Something x2" };
			requests = IoCContainer.GetRequest(names);
			Assert.AreEqual(0, requests.Count);

			Assert.Pass();
		}
	}
}
