using System;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using WeatherLib.Container;
using WeatherLib.Sites.OpenWeatherMap;
using WeatherLib.Sites.TomorrowIo;

namespace WeatherLibTests
{
    public class IoCContainerTests
    {
        [Test]
        public void CreateContainerTest()
        {
            var sites = IoCContainer.GetServices();

            var connectedSites = (List<Type>)typeof(IoCContainer)
                .GetField("connectedSites", BindingFlags.NonPublic | BindingFlags.Static)
                .GetValue(sites);

            Assert.IsTrue(connectedSites.Contains(typeof(OpenWeatherMapWeatherService)));
            Assert.IsTrue(connectedSites.Contains(typeof(TomorrowIoWeatherService)));
            Assert.AreEqual(2, sites.Count);
        }

        [Test]
        public void DisableServiceTest()
        {
            IoCContainer.Disable(typeof(TomorrowIoWeatherService));
            var sites = IoCContainer.GetServices();

            Assert.IsTrue(sites[0] is OpenWeatherMapWeatherService);
            Assert.AreEqual(1, sites.Count);

            IoCContainer.Disable(typeof(TomorrowIoWeatherService));
            sites = IoCContainer.GetServices();

            Assert.IsTrue(sites[0] is OpenWeatherMapWeatherService);
            Assert.AreEqual(1, sites.Count);

            IoCContainer.Disable(typeof(OpenWeatherMapWeatherService));
            sites = IoCContainer.GetServices();

            Assert.AreEqual(0, sites.Count);
        }

        [Test]
        public void ConnectServiceTest()
        {
            IoCContainer.Connect(typeof(OpenWeatherMapWeatherService));
            var sites = IoCContainer.GetServices();

            Assert.IsTrue(sites[0] is OpenWeatherMapWeatherService);
            Assert.IsTrue(sites[1] is TomorrowIoWeatherService);
            Assert.AreEqual(2, sites.Count);

            IoCContainer.Disable(typeof(OpenWeatherMapWeatherService));
            IoCContainer.Connect(typeof(OpenWeatherMapWeatherService));
            sites = IoCContainer.GetServices();

            Assert.IsTrue(sites[1] is OpenWeatherMapWeatherService);
            Assert.IsTrue(sites[0] is TomorrowIoWeatherService);
            Assert.AreEqual(2, sites.Count);
        }
    }
}