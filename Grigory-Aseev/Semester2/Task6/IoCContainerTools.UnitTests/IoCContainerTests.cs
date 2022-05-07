using NUnit.Framework;
using SiteInterfaces;
using Sites;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IoCContainerTools.UnitTests
{
    public class IoCContainerTests
    {
        [Test]
        public void GetSitesTest()
        {
            List<ISite> sites = IoCContainer.GetSites().ToList();
            Assert.IsNotNull(sites);
            var types = sites.Select(x => x.GetType()).ToList();
            Assert.AreEqual(types.Count, 3);
            Assert.True(types.Contains(typeof(OpenWeatherMap)));
            Assert.True(types.Contains(typeof(TomorrowIO)));
            Assert.True(types.Contains(typeof(StormGlassIO)));
        }

        [Test]
        public void DisconnectSiteTest()
        {
            Assert.IsTrue(IoCContainer.DisconnectSite(typeof(OpenWeatherMap)));
            Assert.IsFalse(IoCContainer.DisconnectSite(typeof(OpenWeatherMap)));

            Assert.IsTrue(IoCContainer.Sites.Count == 2);
            Assert.IsFalse(IoCContainer.Sites.Contains(typeof(OpenWeatherMap)));
            Assert.IsTrue(IoCContainer.Sites.Contains(typeof(TomorrowIO)));
            Assert.IsTrue(IoCContainer.Sites.Contains(typeof(StormGlassIO)));

            IoCContainer.ConnectSite(typeof(OpenWeatherMap));

            AssertNormallyState();
        }

        [Test]
        public void ConnectSite()
        {
            Assert.IsFalse(IoCContainer.ConnectSite(typeof(OpenWeatherMap)));
            Assert.IsTrue(IoCContainer.DisconnectSite(typeof(OpenWeatherMap)));
            Assert.IsTrue(IoCContainer.ConnectSite(typeof(OpenWeatherMap)));

            AssertNormallyState();
        }


        [Test]
        public void SetToSitesTest()
        {
            IoCContainer.DisconnectSite(typeof(StormGlassIO));

            typeof(IoCContainer).GetProperty("Sites", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public).SetValue(null, new List<Type>() { typeof(OpenWeatherMap), typeof(TomorrowIO), typeof(StormGlassIO) });
            AssertNormallyState();
        }

        private void AssertNormallyState()
        {
            Assert.IsTrue(IoCContainer.Sites.Count == 3);
            Assert.IsTrue(IoCContainer.Sites.Contains(typeof(OpenWeatherMap)));
            Assert.IsTrue(IoCContainer.Sites.Contains(typeof(TomorrowIO)));
            Assert.IsTrue(IoCContainer.Sites.Contains(typeof(StormGlassIO)));
        }
    }
}