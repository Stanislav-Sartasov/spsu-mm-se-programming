using NUnit.Framework.Internal;
using Model;
using Task6;



namespace Task6Test
{
    public class Task6Tests
    {
        [Test]
        public void PrintWeatherTest()
        {
            var output = new StringWriter();
            Console.SetOut(output);
            string expectedOutput =
@"Temperature: 10 °C (50 °F)
Wind: 6 m/s (West)
Cloud coverage: 48%
Precipitation: Clear
Humidity: 0%
";

            var weather = new WeatherInfo(10, 50, 48, 0, "Clear", 270, 6);
            Printer.PrintWeather(weather);
            Assert.That(output.ToString(), Is.EqualTo(expectedOutput + Environment.NewLine));

            Assert.Pass();
        }

        [TestCase(22.4, "North")]
        [TestCase(66.7, "Northeast")]
        [TestCase(240, "Southwest")]
        public void GetWindDirectionTest(double deg, string wind)
        {
            Assert.That(WeatherInfo.GetWindDirection(deg), Is.EqualTo(wind));

            Assert.Pass();
        }

        [TestCase(-200)]
        [TestCase(555)]
        public void GetBadWindDirectionTest(double deg)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => WeatherInfo.GetWindDirection(deg));

            Assert.Pass();
        }


        [Test]
        public void OpenWeatherRequestTest()
        {
            var openWeather = new OpenWeatherMapApi(59.9386, 30.3141, "4294da65175d97bba60b63e2dfac025b");
            Assert.IsTrue(openWeather.ConnectToService("https://api.openweathermap.org/data/2.5/weather?lat=59.9386&lon=30.3141&appid=2491f45aad3da8d3d29704c2378163b2&units=metric"));
            Assert.IsNotNull(openWeather.WeatherForecast);
            Assert.DoesNotThrow(() => openWeather.GetData());

            Assert.Pass();
        }

        [Test]
        public void OpenWeatherWrongRequestTest()
        {
            var openWeather = new OpenWeatherMapApi(59.9386, 30.3141, "nokey");
            Assert.IsFalse(openWeather.ConnectToService("https://api.openweathermap.org/data/2.5/weather?lat=59.9386&lon=30.3141&appid=nokey&units=metric"));
            Assert.IsNull(openWeather.WeatherForecast);
            Assert.Throws<Exception>(() => openWeather.GetData());

            Assert.Pass();
        }

        [Test]
        public void TomorrowIoRequestTest()
        {
            var tomorrowIo = new TomorrowIoApi(59.9386, 30.3141, "ZkNpxmBkyZkUJxO4xJ6HDuTOs4BtXXsy");
            Assert.IsTrue(tomorrowIo.ConnectToService("https://api.tomorrow.io/v4/timelines?location=59.9386,30.3141&timezone=Europe/Moscow&fields=temperature,cloudCover,humidity,precipitationType,windDirection,windSpeed&timesteps=current&units=metric&apikey=M5cHY0KsL51QLGbH11EknDPTQhxjRmcw"));
            Assert.IsNotNull(tomorrowIo.WeatherForecast);
            Assert.DoesNotThrow(() => tomorrowIo.GetData());

            Assert.Pass();
        }

        [Test]
        public void TomorrowIoWrongRequestTest()
        {
            var tomorrowIo = new TomorrowIoApi(59.9386, 30.3141, "nokey");
            Assert.IsFalse(tomorrowIo.ConnectToService("https://api.tomorrow.io/v4/timelines?location=59.9386,30.3141&timezone=Europe/Moscow&fields=temperature,cloudCover,humidity,precipitationType,windDirection,windSpeed&timesteps=current&units=metric&apikey=nokey"));
            Assert.IsNull(tomorrowIo.WeatherForecast);
            Assert.Throws<Exception>(() => tomorrowIo.GetData());

            Assert.Pass();
        }

        [Test]
        public void OpenWeatherParseTest()
        {
            var json = "{\"coord\":{\"lon\":30.3141,\"lat\":59.9386},\"weather\":[{\"id\":804,\"main\":\"Clouds\",\"description\":\"overcast clouds\",\"icon\":\"04d\"}],\"base\":\"stations\",\"main\":{\"temp\":10.05,\"feels_like\":9.38,\"temp_min\":10.05,\"temp_max\":10.06,\"pressure\":1014,\"humidity\":87},\"visibility\":10000,\"wind\":{\"speed\":4,\"deg\":180},\"clouds\":{\"all\":100},\"dt\":1655431410,\"sys\":{\"type\":1,\"id\":8926,\"country\":\"RU\",\"sunrise\":1655436920,\"sunset\":1655504693},\"timezone\":10800,\"id\":519690,\"name\":\"Novaya Gollandiya\",\"cod\":200}";
            var openWeather = new OpenWeatherMapApi(59.9386, 30.3141, "4294da65175d97bba60b63e2dfac025b");
            var parsing = openWeather.Parse(json);
            Assert.That(parsing.TempC, Is.EqualTo(10.05));
            Assert.That(parsing.Humidity, Is.EqualTo(87));
            Assert.That(parsing.CloudsPercent, Is.EqualTo(100));
            Assert.That(parsing.Precipitation, Is.EqualTo("Clouds"));
            Assert.That(parsing.WindSpeed, Is.EqualTo(4));
            Assert.That(parsing.WindDirection, Is.EqualTo("South"));

            Assert.Pass();
        }

        [Test]
        public void TomorrowIoParseTest()
        {
            var json = "{\"data\":{\"timelines\":[{\"timestep\":\"current\",\"endTime\":\"2022-06-16T21:32:00+03:00\",\"startTime\":\"2022-06-16T21:32:00+03:00\",\"intervals\":[{\"startTime\":\"2022-06-16T21:32:00+03:00\",\"values\":{\"cloudCover\":98,\"humidity\":87,\"precipitationType\":0,\"temperature\":11.13,\"windDirection\":195,\"windSpeed\":3.81}}]}]}}";
            var tomorrowIo = new TomorrowIoApi(59.9386, 30.3141, "ZkNpxmBkyZkUJxO4xJ6HDuTOs4BtXXsy");
            var parsing = tomorrowIo.Parse(json);
            Assert.That(parsing.TempC, Is.EqualTo(11.13));
            Assert.That(parsing.Humidity, Is.EqualTo(87));
            Assert.That(parsing.CloudsPercent, Is.EqualTo(98));
            Assert.That(parsing.Precipitation, Is.EqualTo("N/A"));
            Assert.That(parsing.WindSpeed, Is.EqualTo(3.81));
            Assert.That(parsing.WindDirection, Is.EqualTo("South"));

            Assert.Pass();
        }

        [Test]
        public void ContainerTest()
        {
            List<IApi> testContainer = IoCContainer.Container().ToList();
            Assert.IsNotNull(testContainer);
            Assert.That(testContainer.Count, Is.EqualTo(2));
            Assert.That(testContainer[0].ApiName, Is.EqualTo("OpenWeatherMap"));
            Assert.That(testContainer[1].ApiName, Is.EqualTo("TomorrowIo"));

            Assert.Pass();
        }
    }
}