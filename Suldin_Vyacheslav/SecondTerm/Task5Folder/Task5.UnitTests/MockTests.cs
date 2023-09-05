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

namespace Task5.UnitTests
{
    public class MockTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]

        public void WebTests()
        {
            var mock = new Mock<IGetRequest>();
            mock.Setup(x => x.Send()).Returns("AllFine");
            mock.Setup(x => x.GetResponce()).Returns("{\"some\":\"json\"}");
            var correct = new JsonGetter(mock.Object);
            JObject json = correct.GetJSON();
            mock.Verify(gr => gr.Send());

            if (json == null && json["some"] == null && json["some"].ToString() != "json".ToString())
            {
                Assert.Fail();
            }

            mock = new Mock<IGetRequest>();
            mock.Setup(x => x.Send()).Returns("very danger error (822)");
            correct = new JsonGetter(mock.Object);
            json = correct.GetJSON();

            if (json == null && json["ERROR"] == null && !json["ERROR"].ToString().Contains("822"))
            {
                Assert.Fail();
            }

            Assert.Pass();
        }

        [Test]

        public void TomorrowTest()
        {
            var json = JObject.Parse("{\"data\":{\"timelines\":[{\"timestep\":\"current\",\"endTime\":\"2022-04-11T18:30:00Z\",\"startTime\":\"2022-04-11T18:30:00Z\",\"intervals\":[{\"startTime\":\"2022-04-11T18:30:00Z\",\"values\":{\"cloudCover\":24,\"humidity\":71,\"precipitationIntensity\":0,\"precipitationType\":0,\"temperature\":3.38,\"windDirection\":258.63,\"windSpeed\":2.63}}]}]}}");

            var jsonGenerator = new Mock<JsonGetter>(new GetRequest(null, null));
            jsonGenerator.Setup(x => x.GetJSON()).Returns(json);

            TouchAllProperties<TIORoot>(new TIORoot());
            
            var tomorrowTest = new TomorrowIOParser();

            tomorrowTest.Parse(jsonGenerator.Object.GetJSON());

            var result = tomorrowTest.GetWeatherInfo();
            if (result.Name != "TomorrowIO" ||
                result.ImperialTemp != "35,38" ||
                result.MetricTemp != "3,38" ||
                result.CloudCover != "24" ||
                result.Humidity != "71" ||
                result.Precipipations != "NoPrecip:0" ||
                result.WindDegree != "258,63" ||
                result.WindSpeed != "2,63" ||
                result.Error != null) Assert.Fail();

            Assert.Pass();
        }

        [Test]

        public void StormGlassTest()
        {
            TouchAllProperties<SGRoot>(new SGRoot());
            var json = JObject.Parse("{\"hours\":[{\"airTemperature\":{\"dwd\":1.54,\"noaa\":1.78,\"sg\":1.54},\"cloudCover\":{\"dwd\":0.0,\"noaa\":100.0,\"sg\":0.0},\"humidity\":{\"dwd\":73.59,\"noaa\":84.3,\"sg\":73.59},\"precipitation\":{\"dwd\":0.0,\"noaa\":0.02,\"sg\":0.0},\"time\":\"2022-04-11T21:00:00+00:00\",\"windDirection\":{\"icon\":261.87,\"noaa\":25.67,\"sg\":261.87},\"windSpeed\":{\"icon\":2.12,\"noaa\":2.92,\"sg\":2.12}}],\"meta\":{\"cost\":1,\"dailyQuota\":10,\"end\":\"2022-04-11 21:25\",\"lat\":59.873703,\"lng\":29.828038,\"params\":[\"airTemperature\",\"cloudCover\",\"humidity\",\"precipitation\",\"windSpeed\",\"windDirection\"],\"requestCount\":1,\"start\":\"2022-04-11 21:00\"}}");

            var jsonGenerator = new Mock<JsonGetter>(new GetRequest(null, null));
            jsonGenerator.Setup(x => x.GetJSON()).Returns(json);

            var stormGlassTest = new StormGlassParser();

            stormGlassTest.Parse(jsonGenerator.Object.GetJSON());

            var result = stormGlassTest.GetWeatherInfo();
            if (result.Name != "StormGlass" ||
                result.ImperialTemp != "33,78" ||
                result.MetricTemp != "1,78" ||
                result.CloudCover != "0" ||
                result.Humidity != "84,3" ||
                result.Precipipations != ":0,02" ||
                result.WindDegree != "261,87" ||
                result.WindSpeed != "2,92" ||
                result.Error != null) Assert.Fail();

            Assert.Pass();
        }

        [Test]

        public void OpenWeatherTest()
        {
            TouchAllProperties<OWRoot>(new OWRoot());

            var json = JObject.Parse("{\"coord\":{\"lon\":29.828,\"lat\":59.8737},\"weather\":[{\"id\":802,\"main\":\"Clouds\",\"description\":\"scattered clouds\",\"icon\":\"03n\"}],\"base\":\"stations\",\"main\":{\"temp\":4.8,\"feels_like\":3.53,\"temp_min\":3.81,\"temp_max\":4.81,\"pressure\":1013,\"humidity\":87,\"sea_level\":1013,\"grnd_level\":1008},\"visibility\":10000,\"wind\":{\"speed\":1.65,\"deg\":243,\"gust\":2.08},\"clouds\":{\"all\":49},\"dt\":1649701395,\"sys\":{\"type\":2,\"id\":2045832,\"country\":\"RU\",\"sunrise\":1649645798,\"sunset\":1649696790},\"timezone\":10800,\"id\":6647815,\"name\":\"Novyye Mesta\",\"cod\":200}");

            var jsonGenerator = new Mock<JsonGetter>(new GetRequest(null, null));
            jsonGenerator.Setup(x => x.GetJSON()).Returns(json);

            var openWeatherTest = new OpenWeatherParser();

            openWeatherTest.Parse(jsonGenerator.Object.GetJSON());

            var result = openWeatherTest.GetWeatherInfo();
            if (result.Name != "OpenWeather" ||
                result.ImperialTemp != "36,8" ||
                result.MetricTemp != "4,8" ||
                result.CloudCover != "49" ||
                result.Humidity != "87" ||
                result.Precipipations != "NoPrecip" ||
                result.WindDegree != "243" ||
                result.WindSpeed != "1,65" ||
                result.Error != null) Assert.Fail();

            Assert.Pass();
        }

        [Test]

        public void GismeteoTest()
        {

            TouchAllProperties<GMRoot>(new GMRoot());

            var json = JObject.Parse("{\"meta\":{\"message\":\"\",\"code\":\"200\"},\"response\":{\"precipitation\":{\"type_ext\":null,\"intensity\":0,\"correction\":null,\"amount\":0,\"duration\":0,\"type\":0},\"pressure\":{\"h_pa\":1006,\"mm_hg_atm\":755,\"in_hg\":39.6},\"humidity\":{\"percent\":76},\"icon\":\"n\",\"gm\":2,\"wind\":{\"direction\":{\"degree\":260,\"scale_8\":7},\"speed\":{\"km_h\":4,\"m_s\":1,\"mi_h\":2}},\"cloudiness\":{\"type\":0,\"percent\":10},\"date\":{\"UTC\":\"2022-04-11 18:00:00\",\"local\":\"2022-04-11 21:00:00\",\"time_zone_offset\":180,\"hr_to_forecast\":null,\"unix\":1649700000},\"radiation\":{\"uvb_index\":null,\"UVB\":null},\"city\":163243,\"kind\":\"Obs\",\"storm\":false,\"temperature\":{\"comfort\":{\"C\":4.2,\"F\":39.6},\"water\":{\"C\":3,\"F\":37.4},\"air\":{\"C\":4.2,\"F\":39.6}},\"description\":{\"full\":\"Ясно\"}}}");

            var jsonGenerator = new Mock<JsonGetter>(new GetRequest(null, null));
            jsonGenerator.Setup(x => x.GetJSON()).Returns(json);

            var gismeteoTest = new GisMeteoParser();

            gismeteoTest.Parse(jsonGenerator.Object.GetJSON());

            var result = gismeteoTest.GetWeatherInfo();
            if (result.Name != "GisMeteo" ||
                result.ImperialTemp != "39,6" ||
                result.MetricTemp != "4,2" ||
                result.CloudCover != "10" ||
                result.Humidity != "76" ||
                result.Precipipations != "NoPrecip:0" ||
                result.WindDegree != "260" ||
                result.WindSpeed != "1" ||
                result.Error != null) Assert.Fail();

            Assert.Pass();
        }

        [Test]
        public void ConsoleWriterTest()
        {
            var json = JObject.Parse("{\"meta\":{\"message\":\"\",\"code\":\"200\"},\"response\":{\"precipitation\":{\"type_ext\":null,\"intensity\":0,\"correction\":null,\"amount\":0,\"duration\":0,\"type\":0},\"pressure\":{\"h_pa\":1006,\"mm_hg_atm\":755,\"in_hg\":39.6},\"humidity\":{\"percent\":76},\"icon\":\"n\",\"gm\":2,\"wind\":{\"direction\":{\"degree\":260,\"scale_8\":7},\"speed\":{\"km_h\":4,\"m_s\":1,\"mi_h\":2}},\"cloudiness\":{\"type\":0,\"percent\":10},\"date\":{\"UTC\":\"2022-04-11 18:00:00\",\"local\":\"2022-04-11 21:00:00\",\"time_zone_offset\":180,\"hr_to_forecast\":null,\"unix\":1649700000},\"radiation\":{\"uvb_index\":null,\"UVB\":null},\"city\":163243,\"kind\":\"Obs\",\"storm\":false,\"temperature\":{\"comfort\":{\"C\":4.2,\"F\":39.6},\"water\":{\"C\":3,\"F\":37.4},\"air\":{\"C\":4.2,\"F\":39.6}},\"description\":{\"full\":\"Ясно\"}}}");

            var jsonGenerator = new Mock<JsonGetter>(new GetRequest(null, null));
            jsonGenerator.Setup(x => x.GetJSON()).Returns(json);

            var gismeteoTest = new GisMeteoParser();

            gismeteoTest.Parse(jsonGenerator.Object.GetJSON());

            ConsoleWriter cs = new ConsoleWriter();
            try
            {
                ConsoleWriter.ShowWeatherInfo(gismeteoTest.GetWeatherInfo());

            }
            catch (Exception)
            {
                Assert.Fail();
            }
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