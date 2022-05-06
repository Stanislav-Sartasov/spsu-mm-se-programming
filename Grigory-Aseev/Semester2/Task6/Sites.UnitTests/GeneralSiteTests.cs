using NUnit.Framework;
using System.Reflection;

namespace Sites.UnitTests
{
    public class GeneralSiteTests
    {
        [Test]
        public void GetBadRequestTest()
        {
            var site = new OpenWeatherMap();
            typeof(ASite).GetProperty("url", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(site, "Jotaro....Dio...");
            Assert.AreEqual("No errors detected.", site.ExceptionMessages);
            Assert.IsFalse(site.GetRequest());
            Assert.AreNotEqual("No errors detected.", site.ExceptionMessages);
            var prop = typeof(ASite).GetProperty("requestSuccess", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(site);
            Assert.IsFalse((bool)prop);
        }

        [Test]
        public void GetGoodRequestTest()
        {
            var site = new OpenWeatherMap();
            Assert.AreEqual("No errors detected.", site.ExceptionMessages);
            Assert.IsTrue(site.GetRequest());
            Assert.AreEqual("No errors detected.", site.ExceptionMessages);
            var prop = typeof(ASite).GetProperty("requestSuccess", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(site);
            Assert.IsTrue((bool)prop);
        }

        [Test]
        public void ClearTest()
        {
            var site = new OpenWeatherMap();
            typeof(ASite).GetProperty("ExceptionMessages", BindingFlags.Public | BindingFlags.Instance).SetValue(site, "sddsvsd");
            site.GetRequest();
            site.Clear();
            Assert.IsNull((string?)typeof(ASite).GetProperty("response", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(site));
            Assert.IsFalse((bool)typeof(ASite).GetProperty("requestSuccess", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(site));
            Assert.IsNull(site.WeatherInfo);
            Assert.AreEqual("No errors detected.", (string)typeof(ASite).GetProperty("ExceptionMessages", BindingFlags.Public | BindingFlags.Instance).GetValue(site));
        }
    }
}