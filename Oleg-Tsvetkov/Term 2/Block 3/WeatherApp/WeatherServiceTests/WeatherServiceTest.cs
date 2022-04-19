using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using OpenWeatherMapServiceLib;
using StormGlassWeatherServiceLib;
using WeatherServiceLib;

namespace WeatherServiceTests
{
    public class WeatherServiceTest
    {

        [Test]
        public void TestStormGlassUsual()
        {
            AbstractWeatherService service = new StormGlassWeatherService(59.9311, 30.3609, "3a671f4a-be66-11ec-9d13-0242ac130002-3a671fc2-be66-11ec-9d13-0242ac130002");
            Assert.IsTrue(service.UpdateInfo());
        }

        [Test]
        public void TestStormGlassWrongKey()
        {
            AbstractWeatherService service = new StormGlassWeatherService(59.9311, 30.3609, "123");
            Assert.IsFalse(service.UpdateInfo());
        }

        [Test]
        public void TestOpenWeatherMapUsual()
        {
            AbstractWeatherService service = new OpenWeatherMapService(59.9311, 30.3609, "83287654f9b418ab802771fac776a42f");
            Assert.IsTrue(service.UpdateInfo());
        }

        [Test]
        public void TestOpenWeatherMapWrongKey()
        {
            AbstractWeatherService service = new OpenWeatherMapService(59.9311, 30.3609, "123");
            Assert.IsFalse(service.UpdateInfo());
        }

        [Test]
        public void TestPrintInfo()
        {
            AbstractWeatherService service = new OpenWeatherMapService(59.9311, 30.3609, "123");
            Assert.That(() => service.PrintInfo(), Throws.Nothing);
        }

        [Test]
        public void TestPrintInfoIfNothingProvided()
        {
            AbstractWeatherService service = new OpenWeatherMapService(59.9311, 30.3609, "123");

            StringWriter stringWriter = new StringWriter();
            TextWriter originalOutput = Console.Out;
            Console.SetOut(stringWriter);

            service.PrintInfo();

            List<string> result = new List<string>(stringWriter.ToString().Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None));
            result.RemoveAt(result.Count - 1);


            foreach (string line in result)
            {
                Console.WriteLine(line);
                Assert.IsTrue(line.Contains("Нет данных"));
            }

            Console.SetOut(originalOutput);

        }

        [Test]
        public void TestPrintInfoIfProvided()
        {
            AbstractWeatherService service = new OpenWeatherMapService(59.9311, 30.3609, "83287654f9b418ab802771fac776a42f");

            StringWriter stringWriter = new StringWriter();
            TextWriter originalOutput = Console.Out;
            Console.SetOut(stringWriter);

            service.UpdateInfo();
            service.PrintInfo();

            List<string> result = new List<string>(stringWriter.ToString().Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None));
            result.RemoveAt(result.Count - 1);

            int noInfoCounter = 0;

            foreach (string line in result)
            {
                Console.WriteLine(line);
                if (line.Contains("Нет данных"))
                {
                    ++noInfoCounter;
                }
            }
            Assert.IsTrue(noInfoCounter < result.Count / 2);

            Console.SetOut(originalOutput);
        }
    }
}