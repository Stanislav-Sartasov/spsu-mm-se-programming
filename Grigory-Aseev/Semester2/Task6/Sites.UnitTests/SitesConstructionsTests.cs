using NUnit.Framework;

namespace Sites.UnitTests
{
    public class SitesConstructionsTests
    {
        [Test]
        public void OpenWeatherMapTest()
        {
            var site = new OpenWeatherMap();
            Assert.AreEqual("openweathermap.org", site.SiteAddress);
        }

        [Test]
        public void TomorrowIOTest()
        {
            var site = new TomorrowIO();
            Assert.AreEqual("tomorrow.io", site.SiteAddress);
        }

        [Test]
        public void StormGlassIOTest()
        {
            var site = new StormGlassIO();
            Assert.AreEqual("stormglass.io", site.SiteAddress);
        }
    }
}
