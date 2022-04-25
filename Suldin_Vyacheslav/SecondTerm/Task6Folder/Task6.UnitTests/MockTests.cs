using System;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Moq;
using WebLibrary;
using Parsers;
using TomorrowIO;
using StormGlass;
using GisMeteo;
using OpenWeather;
using System.Collections.Generic;
using System.Reflection;

namespace Task6.UnitTests
{
    public class MockTests
    {
        [SetUp]
        public void Setup()
        {
        }


        [Test]
        public void GetRequestTets()
        {
            

            string[] links = new string[] { "https://www.amazon.com/", "http://hwproj.me/courses/65", null };
            List<string>[] headers = new List<string>[] { new List<string>() { "key:header" }, null };
            foreach (string link in links)
            {
                foreach (var header in headers)
                {
                    var mock = new Mock<IRequestable>();
                    mock.Setup(x => x.GetAddres()).Returns(link);
                    mock.Setup(x => x.GetHeaders()).Returns(header);

                    GetRequest gr = new GetRequest(mock.Object);
                    string statement = gr.Send();
                    switch (link)
                    {
                        case "https://www.amazon.com/":
                            {
                                Assert.AreEqual("AllFine", statement);
                                Assert.AreNotEqual(null, gr.GetResponce());
                                break;
                            }
                        case "http://hwproj.me/courses/65":
                            {
                                Assert.True(statement.Contains(((int)ErrorType.BadGateway).ToString()));
                                break;
                            }
                        default:
                            {
                                Assert.AreEqual("Invalid URI: The URI is empty.",statement);
                                break;
                            }
                    }
                }

            }
            Assert.Pass();
        }

        [Test]

        public void WebTests()
        {
            var mock = new Mock<IGetRequest>();
            mock.Setup(x => x.Send()).Returns("AllFine");
            mock.Setup(x => x.GetResponce()).Returns("{\"some\":\"json\"}");
            var correct = new JsonHolder(mock.Object);
            JObject json = correct.GetJSON();
            mock.Verify(gr => gr.Send());

            Assert.AreEqual("json", json["some"].ToString());

            mock = new Mock<IGetRequest>();
            mock.Setup(x => x.Send()).Returns("very danger error (822)");
            correct = new JsonHolder(mock.Object);
            json = correct.GetJSON();

            Assert.AreEqual("very danger error (822)", json["ERROR"].ToString());
           

            Assert.Pass();
        }

        [Test]

        public void TomorrowTest()
        {
            var json = JObject.Parse("{\"data\":{\"timelines\":[{\"timestep\":\"current\",\"endTime\":\"2022-04-11T18:30:00Z\",\"startTime\":\"2022-04-11T18:30:00Z\",\"intervals\":[{\"startTime\":\"2022-04-11T18:30:00Z\",\"values\":{\"cloudCover\":24,\"humidity\":71,\"precipitationIntensity\":0,\"precipitationType\":0,\"temperature\":3.38,\"windDirection\":258.63,\"windSpeed\":2.63}}]}]}}");


            var jsonGenerator = new Mock<IJsonHolder>();
            jsonGenerator.Setup(x => x.GetJSON()).Returns(json);

            TouchAllProperties<TIORoot>(new TIORoot());

            var tomorrowTest = new TomorrowIOParser("");

            Assert.AreEqual(null, tomorrowTest.Headers);
            Assert.AreEqual("null", tomorrowTest.Key);
            Assert.AreEqual($"https://api.tomorrow.io/v4/timelines?location=59.873703,29.828038&fields=temperature,cloudCover,humidity,precipitationType,precipitationIntensity,windSpeed,windDirection&timesteps=current&units=metric&apikey=null", tomorrowTest.Link);

            tomorrowTest = new TomorrowIOParser(jsonGenerator.Object.GetJSON());

            var result = tomorrowTest.GetWeatherInfo();


            Assert.AreEqual("TomorrowIO", result.Name);
            Assert.AreEqual("35,38", result.ImperialTemp);
            Assert.AreEqual("3,38", result.MetricTemp);
            Assert.AreEqual("24", result.CloudCover);
            Assert.AreEqual("71", result.Humidity);
            Assert.AreEqual("NoPrecip:0", result.Precipipations);
            Assert.AreEqual("258,63", result.WindDegree);
            Assert.AreEqual("2,63", result.WindSpeed);
            Assert.AreEqual(null, result.Error);

            Assert.Pass();
        }

        [Test]

        public void StormGlassTest()
        {
            TouchAllProperties<SGRoot>(new SGRoot());
            var json = JObject.Parse("{\"hours\":[{\"airTemperature\":{\"dwd\":1.54,\"noaa\":1.78,\"sg\":1.54},\"cloudCover\":{\"dwd\":0.0,\"noaa\":100.0,\"sg\":0.0},\"humidity\":{\"dwd\":73.59,\"noaa\":84.3,\"sg\":73.59},\"precipitation\":{\"dwd\":0.0,\"noaa\":0.02,\"sg\":0.0},\"time\":\"2022-04-11T21:00:00+00:00\",\"windDirection\":{\"icon\":261.87,\"noaa\":25.67,\"sg\":261.87},\"windSpeed\":{\"icon\":2.12,\"noaa\":2.92,\"sg\":2.12}}],\"meta\":{\"cost\":1,\"dailyQuota\":10,\"end\":\"2022-04-11 21:25\",\"lat\":59.873703,\"lng\":29.828038,\"params\":[\"airTemperature\",\"cloudCover\",\"humidity\",\"precipitation\",\"windSpeed\",\"windDirection\"],\"requestCount\":1,\"start\":\"2022-04-11 21:00\"}}");

            var jsonGenerator = new Mock<IJsonHolder>();
            jsonGenerator.Setup(x => x.GetJSON()).Returns(json);

            var stormGlassTest = new StormGlassParser("");

            Assert.AreEqual("Authorization: null", stormGlassTest.Headers[0]);
            Assert.AreEqual("null", stormGlassTest.Key);
            Assert.AreEqual($"https://api.stormglass.io/v2/weather/point?lat=59.873703&lng=29.828038&params=airTemperature,cloudCover,humidity,precipitation,windSpeed,windDirection&start={((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds()}&end={((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds()}", stormGlassTest.Link);


            stormGlassTest = new StormGlassParser(jsonGenerator.Object.GetJSON());

            var result = stormGlassTest.GetWeatherInfo();

            Assert.AreEqual("StormGlass", result.Name);
            Assert.AreEqual("33,78", result.ImperialTemp);
            Assert.AreEqual("1,78", result.MetricTemp);
            Assert.AreEqual("0", result.CloudCover);
            Assert.AreEqual("84,3", result.Humidity);
            Assert.AreEqual(":0,02", result.Precipipations);
            Assert.AreEqual("261,87", result.WindDegree);
            Assert.AreEqual("2,92", result.WindSpeed);
            Assert.AreEqual(null, result.Error);

            Assert.Pass();
        }

        [Test]

        public void OpenWeatherTest()
        {
            TouchAllProperties<OWRoot>(new OWRoot());

            var json = JObject.Parse("{\"coord\":{\"lon\":29.828,\"lat\":59.8737},\"weather\":[{\"id\":802,\"main\":\"Clouds\",\"description\":\"scattered clouds\",\"icon\":\"03n\"}],\"base\":\"stations\",\"main\":{\"temp\":4.8,\"feels_like\":3.53,\"temp_min\":3.81,\"temp_max\":4.81,\"pressure\":1013,\"humidity\":87,\"sea_level\":1013,\"grnd_level\":1008},\"visibility\":10000,\"wind\":{\"speed\":1.65,\"deg\":243,\"gust\":2.08},\"clouds\":{\"all\":49},\"dt\":1649701395,\"sys\":{\"type\":2,\"id\":2045832,\"country\":\"RU\",\"sunrise\":1649645798,\"sunset\":1649696790},\"timezone\":10800,\"id\":6647815,\"name\":\"Novyye Mesta\",\"cod\":200}");

            var jsonGenerator = new Mock<IJsonHolder>();

            jsonGenerator.Setup(x => x.GetJSON()).Returns(json);

            var openWeatherTest = new OpenWeatherParser("");

            Assert.AreEqual(null, openWeatherTest.Headers);
            Assert.AreEqual("null", openWeatherTest.Key);
            Assert.AreEqual("https://api.openweathermap.org/data/2.5/weather?lat=59.873703&lon=29.828038&appid=null&units=metric", openWeatherTest.Link);


            openWeatherTest = new OpenWeatherParser(jsonGenerator.Object.GetJSON());

            var result = openWeatherTest.GetWeatherInfo();


            Assert.AreEqual("OpenWeather", result.Name);
            Assert.AreEqual("36,8", result.ImperialTemp);
            Assert.AreEqual("4,8", result.MetricTemp);
            Assert.AreEqual("49", result.CloudCover);
            Assert.AreEqual("87", result.Humidity);
            Assert.AreEqual("NoPrecip", result.Precipipations);
            Assert.AreEqual("243", result.WindDegree);
            Assert.AreEqual("1,65", result.WindSpeed);
            Assert.AreEqual(null, result.Error);

            Assert.Pass();
        }

        [Test]

        public void GismeteoTest()
        {

            TouchAllProperties<GMRoot>(new GMRoot());

            var json = JObject.Parse("{\"meta\":{\"message\":\"\",\"code\":\"200\"},\"response\":{\"precipitation\":{\"type_ext\":null,\"intensity\":0,\"correction\":null,\"amount\":0,\"duration\":0,\"type\":0},\"pressure\":{\"h_pa\":1006,\"mm_hg_atm\":755,\"in_hg\":39.6},\"humidity\":{\"percent\":76},\"icon\":\"n\",\"gm\":2,\"wind\":{\"direction\":{\"degree\":260,\"scale_8\":7},\"speed\":{\"km_h\":4,\"m_s\":1,\"mi_h\":2}},\"cloudiness\":{\"type\":0,\"percent\":10},\"date\":{\"UTC\":\"2022-04-11 18:00:00\",\"local\":\"2022-04-11 21:00:00\",\"time_zone_offset\":180,\"hr_to_forecast\":null,\"unix\":1649700000},\"radiation\":{\"uvb_index\":null,\"UVB\":null},\"city\":163243,\"kind\":\"Obs\",\"storm\":false,\"temperature\":{\"comfort\":{\"C\":4.2,\"F\":39.6},\"water\":{\"C\":3,\"F\":37.4},\"air\":{\"C\":4.2,\"F\":39.6}},\"description\":{\"full\":\"????\"}}}");

            var jsonGenerator = new Mock<IJsonHolder>();
            jsonGenerator.Setup(x => x.GetJSON()).Returns(json);

            var gismeteoTest = new GisMeteoParser("");

            Assert.AreEqual("X-Gismeteo-Token: null", gismeteoTest.Headers[0]);
            Assert.AreEqual("null", gismeteoTest.Key);
            Assert.AreEqual("https://api.gismeteo.net/v2/weather/current/?latitude=59.873703&longitude=29.828038", gismeteoTest.Link);

            gismeteoTest = new GisMeteoParser(jsonGenerator.Object.GetJSON());

            var result = gismeteoTest.GetWeatherInfo();


            Assert.AreEqual("GisMeteo", result.Name);
            Assert.AreEqual("39,6", result.ImperialTemp);
            Assert.AreEqual("4,2", result.MetricTemp);
            Assert.AreEqual("10", result.CloudCover);
            Assert.AreEqual("76", result.Humidity);
            Assert.AreEqual("NoPrecip:0", result.Precipipations);
            Assert.AreEqual("260", result.WindDegree);
            Assert.AreEqual("1", result.WindSpeed);
            Assert.AreEqual(null, result.Error);

            Assert.Pass();
        }

        public static void TouchAllProperties<T>(T root)
        {
            var propeties = new List<PropertyInfo>() { };
            propeties.AddRange(root.GetType().GetProperties());
            while (propeties.Count != 0)
            {
                var removeProperties = new List<PropertyInfo>() { };
                var newPropeties = new List<PropertyInfo>() { };
                foreach (var property in propeties)
                {
                    var parCons = property.DeclaringType.GetConstructor(new Type[] { });
                    var par = parCons.Invoke(null);

                    var pptType = property.PropertyType;


                    ConstructorInfo constructor;

                    if (pptType.IsArray)
                    {
                        var subType = property.PropertyType.GetElementType();
                        constructor = subType.GetConstructor(new Type[] { });
                    }

                    else
                        constructor = pptType.GetConstructor(new Type[] { });

                    if (constructor != null)
                    {
                        var child = constructor.Invoke(null);
                        newPropeties.AddRange(child.GetType().GetProperties());
                    }

                    property.GetGetMethod().Invoke(par, null);
                    removeProperties.Add(property);
                }
                foreach (var remove in removeProperties)
                    propeties.Remove(remove);

                propeties.AddRange(newPropeties);
            }
        }
    }
}