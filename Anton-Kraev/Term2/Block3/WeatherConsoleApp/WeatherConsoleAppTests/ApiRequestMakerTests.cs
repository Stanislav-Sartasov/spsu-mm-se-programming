using System.Reflection;
using NUnit.Framework;
using WeatherConsoleApp.ApiRequestMaker;
using OpenWeatherMap = WeatherConsoleApp.Sites.OpenWeatherMap;
using TomorrowIo = WeatherConsoleApp.Sites.TomorrowIo;

namespace WeatherConsoleAppTests
{
    public class ApiRequestMakerTests
    {
        [Test]
        public void OpenWeatherMapSetApiKeyTest()
        {
            var apiRequestMaker = new OpenWeatherMap.ApiRequestMaker();
            var key = (string?)typeof(OpenWeatherMap.ApiRequestMaker).GetField("key", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(apiRequestMaker);
            Assert.AreEqual("e2d900011a6c02deccc3e56e7534ed32", key);
        }

        [Test]
        public void TomorrowIoSetApiKeyTest()
        {
            var apiRequestMaker = new TomorrowIo.ApiRequestMaker();
            var key = (string?)typeof(TomorrowIo.ApiRequestMaker).GetField("key", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(apiRequestMaker);
            Assert.AreEqual("aklQAoAZ4rhfO9brbUCK5p47DarnvEVO", key);
        }

        [Test]
        public void OpenWeatherMapRequestsTest()
        {
            AbstractApiRequestMaker apiRequestMaker = new OpenWeatherMap.ApiRequestMaker();
            Assert.IsNotNull(apiRequestMaker.GetResponse());
            Assert.IsNull(apiRequestMaker.accessError);

            apiRequestMaker.ChangeApiKey("fdsfsdfsdf");
            Assert.IsNull(apiRequestMaker.GetResponse());
            Assert.IsNotNull(apiRequestMaker.accessError);

            apiRequestMaker.ChangeApiKey("e2d900011a6c02deccc3e56e7534ed32");
            Assert.IsNotNull(apiRequestMaker.GetResponse());
            Assert.IsNull(apiRequestMaker.accessError);

            apiRequestMaker.ChangeApiKey("sdfunmadl");
            apiRequestMaker.SetDefaultApiKey();
            Assert.IsNotNull(apiRequestMaker.GetResponse());
            Assert.IsNull(apiRequestMaker.accessError);
        }

        [Test]
        public void TomorrowIoRequestsTest()
        {
            AbstractApiRequestMaker apiRequestMaker = new TomorrowIo.ApiRequestMaker();
            Assert.IsNotNull(apiRequestMaker.GetResponse());
            Assert.IsNull(apiRequestMaker.accessError);

            apiRequestMaker.ChangeApiKey("fdsfsdfsdf");
            Assert.IsNull(apiRequestMaker.GetResponse());
            Assert.IsNotNull(apiRequestMaker.accessError);

            apiRequestMaker.ChangeApiKey("aklQAoAZ4rhfO9brbUCK5p47DarnvEVO");
            Assert.IsNotNull(apiRequestMaker.GetResponse());
            Assert.IsNull(apiRequestMaker.accessError);

            apiRequestMaker.ChangeApiKey("sdfunmadl");
            apiRequestMaker.SetDefaultApiKey();
            Assert.IsNotNull(apiRequestMaker.GetResponse());
            Assert.IsNull(apiRequestMaker.accessError);
        }
    }
}