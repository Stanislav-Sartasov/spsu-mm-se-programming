using NUnit.Framework;
using WeatherContainer;
using Sites;

namespace WeatherContainerTests
{
	public class IoCContainerTests
	{
		[Test]
		public void AddSitesTests()
		{
			var testSites = IoCContainer.GetServices();

			Assert.IsEmpty(testSites);

			IoCContainer.AddSites(typeof(TomorrowIo));
			testSites = IoCContainer.GetServices();

			Assert.AreEqual(1, testSites.Count);
			Assert.IsTrue(testSites[0] is TomorrowIo);

			IoCContainer.AddSites(typeof(StormglassIo));
			testSites = IoCContainer.GetServices();

			Assert.AreEqual(2, testSites.Count);
			Assert.IsTrue(testSites[0] is TomorrowIo);
			Assert.IsTrue(testSites[1] is StormglassIo);

			Assert.Pass();
		}

		[Test]
		public void RemoveSitesTests()
		{
			var testSites = IoCContainer.GetServices();

			IoCContainer.AddSites(typeof(TomorrowIo));
			IoCContainer.AddSites(typeof(StormglassIo));
			testSites = IoCContainer.GetServices();

			Assert.AreEqual(2, testSites.Count);
			Assert.IsTrue(testSites[0] is TomorrowIo);
			Assert.IsTrue(testSites[1] is StormglassIo);

			IoCContainer.RemoveSites(typeof(TomorrowIo));
			testSites = IoCContainer.GetServices();

			Assert.AreEqual(1, testSites.Count);
			Assert.IsTrue(testSites[0] is StormglassIo);

			IoCContainer.RemoveSites(typeof(StormglassIo));
			testSites = IoCContainer.GetServices();

			Assert.IsEmpty(testSites);

			Assert.Pass();

		}
	}
}