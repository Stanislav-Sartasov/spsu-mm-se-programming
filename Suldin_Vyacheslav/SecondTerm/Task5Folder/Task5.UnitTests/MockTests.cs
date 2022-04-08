using System;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Moq;
using WebLibrary;
using Parsers;

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
            string[] parametres = new string[7] { "temperature" ,"cloudCover" , "humidity" ,
            "precipitationType" , "precipitationIntensity", "windSpeed", "windDirection" };
            object[] values = new object[6] { "data", "timelines", 0, "intervals", 0, "values" };
            var json = new JObject();

            json["data"] = new JObject();
            json["data"]["timelines"] = new JArray() { new JObject() };
            json["data"]["timelines"][0]["intervals"] = new JArray() { new JObject() };
            json["data"]["timelines"][0]["intervals"][0]["values"] = new JObject();

            foreach (string param in parametres)
            {
                json["data"]["timelines"][0]["intervals"][0]["values"][param] = new JObject();
                json["data"]["timelines"][0]["intervals"][0]["values"][param] = "3";
            }

            var jsonGenerator = new Moq.Mock<JsonGetter>(new GetRequest(null, null));
            jsonGenerator.Setup(x => x.GetJSON()).Returns(json);


            var tomorrowTest = new TomorrowIOParser();
            var data = new Data(tomorrowTest.GetType());
            tomorrowTest.Parse(jsonGenerator.Object.GetJSON());

            for (int i = 0; i < tomorrowTest.ParsedInfo.Count; i++)
            {
                if (tomorrowTest.ParsedInfo[i] != "TomorrowIO" &&
                    tomorrowTest.ParsedInfo[i] != "3" &&
                    tomorrowTest.ParsedInfo[i] != "37.4" &&
                    !tomorrowTest.ParsedInfo[i].Contains(((PrecipitationType)3).ToString()))
                    Assert.Fail(i.ToString());
            }

            Assert.Pass();
        }
        [Test]

        public void StormGlassTest()
        {
            string[] parametres = new string[6] { "airTemperature", "cloudCover", "humidity", "precipitation", "windSpeed", "windDirection" };

            var json = new JObject();

            json["hours"] = new JArray() { new JObject() };

            foreach (string param in parametres)
            {
                json["hours"][0][param] = new JObject();
                json["hours"][0][param]["noaa"] = new JObject();
                json["hours"][0][param]["noaa"] = "3";
            }


            var jsonGenerator = new Moq.Mock<JsonGetter>(new GetRequest(null, null));
            jsonGenerator.Setup(x => x.GetJSON()).Returns(json);

            
            var stormTest = new StormGlassParser();
            var data = new Data(stormTest.GetType());
            stormTest.Parse(jsonGenerator.Object.GetJSON());

            for (int i = 0; i < stormTest.ParsedInfo.Count; i++)
            {
                if (stormTest.ParsedInfo[i] != "StormGlass" &&
                    stormTest.ParsedInfo[i] != "3" &&
                    stormTest.ParsedInfo[i] != "37.4" &&
                    stormTest.ParsedInfo[i] != ((PrecipitationType)3).ToString())
                    Assert.Fail(i.ToString());
            }

            Assert.Pass();
        }

        [Test]

        public void OpenWeatherTest()
        {

            string[] parametres = new string[6] { "temp", "all", "humidity", "precipitationType", "speed", "deg" };

            var json = new JObject();

            json["main"] = new JObject();
            json["main"][parametres[0]] = "3";
            json["main"][parametres[2]] = "3";


            json["wind"] = new JObject();
            json["wind"][parametres[4]] = "3";
            json["wind"][parametres[5]] = "3";

            json["clouds"] = new JObject();
            json["clouds"][parametres[1]] = "3";


            for (int j = 0; j < 3; j++)
            {
                switch (j)
                {
                    case 0:
                        {
                            break;
                        }
                    case 1:
                        {
                            json["rain"] = new JObject();
                            json["rain"]["1h"] = "3";
                            break;
                        }
                    case 2:
                        {
                            json["snow"] = new JObject();
                            json["snow"]["1h"] = "3";
                            break;
                        }
                }

                var jsonGenerator = new Moq.Mock<JsonGetter>(new GetRequest(null, null));
                jsonGenerator.Setup(x => x.GetJSON()).Returns(json);

                var openTest = new OpenWeatherParser();
                var data = new Data(openTest.GetType());
                openTest.Parse(jsonGenerator.Object.GetJSON());

                for (int i = 0; i < openTest.ParsedInfo.Count; i++)
                {
                    if (!String.Equals(openTest.ParsedInfo[i], "OpenWeather") &&
                        openTest.ParsedInfo[i] != "3" &&
                        openTest.ParsedInfo[i] != "37.4" &&
                        !((PrecipitationType)j).ToString().Contains(((PrecipitationType)j).ToString()))
                        Assert.Fail(i.ToString() + " " + j.ToString());
                }
            }
            Assert.Pass();
        }
    }
}