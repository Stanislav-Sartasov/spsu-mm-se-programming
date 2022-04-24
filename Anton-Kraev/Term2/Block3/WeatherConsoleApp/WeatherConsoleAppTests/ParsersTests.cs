using Newtonsoft.Json.Linq;
using NUnit.Framework;
using TomorrowIo = WeatherConsoleApp.Sites.TomorrowIo;
using OpenWeatherMap = WeatherConsoleApp.Sites.OpenWeatherMap;

namespace WeatherConsoleAppTests
{
    public class ParsersTests
    {
        [Test]
        public void OpenWeatherMapGoodJsonParseTest()
        {
            var json =
                new JObject(
                    new JProperty("coord", new JObject(new JProperty("lon", 30.24), new JProperty("lat", 59.94))),
                    new JProperty("weather",
                        new JArray(new JObject(new JProperty("id", 800), new JProperty("main", "Clear"),
                            new JProperty("description", "clear sky"), new JProperty("icon", "01d")))),
                    new JProperty("base", "stations"),
                    new JProperty("main",
                        new JObject(new JProperty("temp", 273.15), new JProperty("feels_like", 284.72),
                            new JProperty("temp_min", 286.21), new JProperty("temp_max", 286.21),
                            new JProperty("pressure", 1024), new JProperty("humidity", 0))),
                    new JProperty("visibility", 10000),
                    new JProperty("wind", new JObject(new JProperty("speed", 0), new JProperty("deg", 360))),
                    new JProperty("clouds", new JObject(new JProperty("all", 0))), new JProperty("dt", 1650470597),
                    new JProperty("sys",
                        new JObject(new JProperty("type", 1), new JProperty("id", 8926), new JProperty("country", "RU"),
                            new JProperty("sunrise", 1650421705), new JProperty("sunset", 1650475630))),
                    new JProperty("timezone", 10800), new JProperty("id", 536203),
                    new JProperty("name", "Saint Petersburg"), new JProperty("cod", 200)).ToString();
            

            var weather = OpenWeatherMap.JsonParser.Parse(json);

            Assert.AreEqual(0, weather.TempInCelcius);
            Assert.AreEqual(32, weather.TempInFahrenheit);
            Assert.AreEqual(0, weather.Humidity);
            Assert.AreEqual(0, weather.Cloudiness);
            Assert.AreEqual("North", weather.WindDir);
            Assert.AreEqual(0, weather.WindSpeed);
            Assert.AreEqual("Clear", weather.Precipitation);
        }

        [Test]
        public void OpenWeatherMapBadJsonParseTest()
        {
            string? json = null;
            var weather = OpenWeatherMap.JsonParser.Parse(json);
            Assert.AreEqual(null, weather);
        }

        [Test]
        public void TomorrowIoGoodJsonParseTest()
        {
            var json =
                new JObject(new JProperty("data",
                    new JObject(new JProperty("timelines",
                        new JArray(new JObject(new JProperty("timestep", "current"),
                            new JProperty("endTime", "2022-04-20T16:51:00Z"),
                            new JProperty("startTime", "2022-04-20T16:51:00Z"),
                            new JProperty("intervals",
                                new JArray(new JObject(new JProperty("startTime", "2022-04-20T16:51:00Z"),
                                    new JProperty("values",
                                        new JObject(new JProperty("cloudCover", 0), new JProperty("humidity", 0),
                                            new JProperty("precipitationType", 0), new JProperty("temperature", 0),
                                            new JProperty("windDirection", 360),
                                            new JProperty("windSpeed", 0)))))))))))).ToString();


            var weather = TomorrowIo.JsonParser.Parse(json);

            Assert.AreEqual(0, weather.TempInCelcius);
            Assert.AreEqual(32, weather.TempInFahrenheit);
            Assert.AreEqual(0, weather.Humidity);
            Assert.AreEqual(0, weather.Cloudiness);
            Assert.AreEqual("North", weather.WindDir);
            Assert.AreEqual(0, weather.WindSpeed);
            Assert.AreEqual("Clear", weather.Precipitation);
        }

        [Test]
        public void TomorrowIoBadJsonParseTest()
        {
            string? json = null;
            var weather = TomorrowIo.JsonParser.Parse(json);
            Assert.AreEqual(null, weather);
        }
    }
}