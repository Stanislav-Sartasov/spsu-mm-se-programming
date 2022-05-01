using NUnit.Framework;
using Task_3;
using Sites;
using ISites;
using System.Collections.Generic;

namespace Task_3.UnitTests
{
    public class IoCContainerTests
    {
        [Test]
        public void WithActiveSiteTest()
        {
            var container = new IoCContainer().WithActiveSite(SiteName.OpenWeatherMap);
            var sites = container.GetSites();

            Assert.AreEqual(sites.Count, 1);
            Assert.AreEqual(sites[0].Request.Accept, "*/*");

            Assert.Pass();
        }

        [Test]
        public void WithInactiveSiteTest()
        {
            var container = new IoCContainer().WithInactiveSite(SiteName.TommorowIo);
            var sites = container.GetSites();

            Assert.AreEqual(sites.Count, 1);
            Assert.IsFalse(sites[0].Request.Connect);

            Assert.Pass();
        }

        [Test]
        public void WithActiveSitesTests()
        {
            var container = new IoCContainer()
                .WithActiveSites(new List<SiteName> { SiteName.OpenWeatherMap, SiteName.TommorowIo });
            var sites = container.GetSites();

            Assert.AreEqual(sites.Count, 2);
            Assert.AreEqual(sites[0].Request.Accept, "*/*");
            Assert.AreEqual(sites[1].Request.Host, "www.tomorrow.io");

            Assert.Pass();
        }

        [Test]
        public void WithInactiveSitesTests()
        {
            var container = new IoCContainer()
                .WithInactiveSites(new List<SiteName> { SiteName.OpenWeatherMap, SiteName.TommorowIo });
            var sites = container.GetSites();

            Assert.AreEqual(sites.Count, 2);
            Assert.IsFalse(sites[0].Request.Connect);
            Assert.IsFalse(sites[1].Request.Connect);

            Assert.Pass();
        }

        [Test]
        public void WithRemoveSiteTest()
        {
            var container = new IoCContainer()
                .WithActiveSites(new List<SiteName> { SiteName.OpenWeatherMap, SiteName.TommorowIo })
                .WithRemoveSite(SiteName.TommorowIo);
            var sites = container.GetSites();

            Assert.AreEqual(sites.Count, 1);
            Assert.AreEqual(sites[0].Request.Accept, "*/*");

            Assert.Pass();
        }

        [Test]
        public void WithTurnOnSiteTest()
        {
            var container = new IoCContainer()
                .WithInactiveSite(SiteName.OpenWeatherMap)
                .WithTurnOnSite(SiteName.OpenWeatherMap);
            var sites = container.GetSites();

            Assert.AreEqual(sites[0].Request.Accept, "*/*");

            Assert.Pass();
        }

        [Test]
        public void WithTurnOffSiteTest()
        {
            var container = new IoCContainer()
                .WithActiveSite(SiteName.OpenWeatherMap)
                .WithTurnOffSite(SiteName.OpenWeatherMap);
            var sites = container.GetSites();

            Assert.IsFalse(sites[0].Request.Connect);

            Assert.Pass();
        }
    }
}