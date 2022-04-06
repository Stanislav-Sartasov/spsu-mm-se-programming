using NUnit.Framework;
using System.Collections.Generic;

namespace Parser.UnitTests
{
    public class ParserTests
    {
        [Test]
        public void ParseTest()
        {
            string messenge = "tempC:-1.23C,clouds45%,humidity67%,windSpeed89km/h,windDegree100,fallout:snow";
            List<string> patterns = new List<string>
            {
                @"(?<=tempC.)-?\d+\.\d+(?=C)",
                @"(?<=clouds)\d+(?=%)",
                @"(?<=humidity)\d+(?=%)",
                @"(?<=windSpeed)\d+(?=km/h)",
                @"(?<=windDegree)\d+",
                @"(?<=fallout.)\w+"
            };
            Weather.Weather weather = new Parser(messenge).Parse(patterns);
            Assert.AreEqual(weather.TempC, "-1,23");
            Assert.AreEqual(weather.TempF, "29,79");
            Assert.AreEqual(weather.Clouds, "45");
            Assert.AreEqual(weather.Humidity, "67");
            Assert.AreEqual(weather.WindSpeed, "89");
            Assert.AreEqual(weather.WindDegree, "100");
            Assert.AreEqual(weather.FallOut, "snow");
            Assert.Pass();
        }
    }
}