using NUnit.Framework;
using Container;
using SiteInterface;

namespace ContainerTests
{
    public class Tests
    {
        [Test]
        public void GetSitesTest()
        {
            var sites = IoCContainer.GetSites(WeatherParameter.Current);
            Assert.IsNotNull(sites);
            Assert.IsTrue(sites.Count == 3);
            Assert.IsNotNull(sites.Find(x => x.Name == "OpenWeather"));
            Assert.IsNotNull(sites.Find(x => x.Name == "TomorrowIO"));
            Assert.IsNotNull(sites.Find(x => x.Name == "StormGlass"));
        }

        [Test]
        public void RemoveSiteFromContainerTest()
        {
            IoCContainer.RemoveSiteFromContainer(SitesName.TomorrowIO);
            var sites = IoCContainer.GetSites(WeatherParameter.Current);

            Assert.IsNotNull(sites);
            Assert.IsTrue(sites.Count == 2);
            Assert.IsNotNull(sites.Find(x => x.Name == "OpenWeather"));
            Assert.IsNull(sites.Find(x => x.Name == "TomorrowIO"));
            Assert.IsNotNull(sites.Find(x => x.Name == "StormGlass"));
        }

        [Test]
        public void AddSiteToContainerTest()
        {
            IoCContainer.RemoveSiteFromContainer(SitesName.TomorrowIO);
            IoCContainer.AddSiteToContainer(SitesName.TomorrowIO);
            var sites = IoCContainer.GetSites(WeatherParameter.Current);

            Assert.IsNotNull(sites);
            Assert.IsTrue(sites.Count == 3);
            Assert.IsNotNull(sites.Find(x => x.Name == "OpenWeather"));
            Assert.IsNotNull(sites.Find(x => x.Name == "TomorrowIO"));
            Assert.IsNotNull(sites.Find(x => x.Name == "StormGlass"));
        }
    }
}