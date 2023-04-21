namespace WeatherApplication.UnitTests;
using System.Collections.Generic;
using NUnit.Framework;
using WeatherApplication;
using DataGetter;
using System.Text;
using System.IO;
using System;
using Moq;

public class WeatherWriterTests
{
    [Test]
    public void WriteWeatherOnceTest()
    {
        double[] locationCoordinates = new double[] { 55.75583, 37.6173 };
        string locationName = "Moscow";

        string[] generalFields =
            {"temperature",
            "weather",
            "cloudiness",
            "humidity",
            "wind direction",
            "wind speed"};
        string[] units = { "", "", "%", "%", "deg", "m/s" };

        var weatherGettersFields = new Dictionary<string, string[]>();
        weatherGettersFields.Add("TomorrowIo", new string[]
        {"temperature",
        "weatherCode",
        "cloudCover",
        "humidity",
        "windDirection",
        "windSpeed"
        });
        weatherGettersFields.Add("OpenWeatherMap", new string[]
        {
            "main.temp",
            "weather.main",
            "clouds.all",
            "main.humidity",
            "wind.deg",
            "wind.speed"
        });

        var weatherGetters = new List<IWeatherGetter>();
        var mockTomorrowIo = new Mock<IWeatherGetter>();
        mockTomorrowIo.Setup(x => x.GetWeather(It.IsAny<double[]>(), It.IsAny<string[]>())).Returns(new string[] { "3.38", "Cloudy", "88", "72", "307.5", "4.5" });
        mockTomorrowIo.Setup(x => x.Name).Returns("TomorrowIo");

        var mockOpenWeatherMap = new Mock<IWeatherGetter>();
        mockOpenWeatherMap.Setup(x => x.GetWeather(It.IsAny<double[]>(), It.IsAny<string[]>())).Returns(new string[] { "3.12", "Clouds", "100", "82", "300", "7" });
        mockOpenWeatherMap.Setup(x => x.Name).Returns("OpenWeatherMap");

        weatherGetters.Add(mockTomorrowIo.Object);
        weatherGetters.Add(mockOpenWeatherMap.Object);

        var weatherWriter = new WeatherWriter(locationCoordinates, locationName, generalFields, units);
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        weatherWriter.WriteWeatherOnce(weatherGetters, weatherGettersFields);
        var outputLines = stringWriter.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        stringWriter.Close();

        Assert.AreEqual("The weather in Moscow now:", outputLines[0]);
        Assert.AreEqual("---------- TomorrowIo data ----------", outputLines[1]);
        Assert.AreEqual("---------- OpenWeatherMap data ----------", outputLines[9]);
        Assert.AreEqual(17, outputLines.Length);
    }

    [Test]
    public void WriteWeatherManyTimesTest()
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("");
        stringBuilder.AppendLine("s");
        var stringReader = new StringReader(stringBuilder.ToString());
        Console.SetIn(stringReader);

        double[] locationCoordinates = new double[] { 55.75583, 37.6173 };
        string locationName = "Moscow";

        string[] generalFields =
            {"temperature",
            "weather",
            "cloudiness",
            "humidity",
            "wind direction",
            "wind speed"};
        string[] units = { "", "", "%", "%", "deg", "m/s" };

        var weatherGettersFields = new Dictionary<string, string[]>();
        weatherGettersFields.Add("TomorrowIo", new string[]
        {"temperature",
        "weatherCode",
        "cloudCover",
        "humidity",
        "windDirection",
        "windSpeed"
        });
        weatherGettersFields.Add("OpenWeatherMap", new string[]
        {
            "main.temp",
            "weather.main",
            "clouds.all",
            "main.humidity",
            "wind.deg",
            "wind.speed"
        });

        var weatherGetters = new List<IWeatherGetter>();
        var mockTomorrowIo = new Mock<IWeatherGetter>();
        mockTomorrowIo.Setup(x => x.GetWeather(It.IsAny<double[]>(), It.IsAny<string[]>())).Returns(new string[] { "3.38", "Cloudy", "88", "72", "307.5", "4.5" });
        mockTomorrowIo.Setup(x => x.Name).Returns("TomorrowIo");

        var mockOpenWeatherMap = new Mock<IWeatherGetter>();
        mockOpenWeatherMap.Setup(x => x.GetWeather(It.IsAny<double[]>(), It.IsAny<string[]>())).Returns(new string[] { "3.12", "Clouds", "100", "82", "300", "7" });
        mockOpenWeatherMap.Setup(x => x.Name).Returns("OpenWeatherMap");

        weatherGetters.Add(mockTomorrowIo.Object);
        weatherGetters.Add(mockOpenWeatherMap.Object);

        var weatherWriter = new WeatherWriter(locationCoordinates, locationName, generalFields, units);
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        weatherWriter.WriteWeatherManyTimes(weatherGetters, weatherGettersFields);
        var outputLines = stringWriter.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        stringWriter.Close();

        Assert.AreEqual("The weather in Moscow now:", outputLines[0]);
        Assert.AreEqual("---------- TomorrowIo data ----------", outputLines[1]);
        Assert.AreEqual("---------- OpenWeatherMap data ----------", outputLines[9]);
        Assert.AreEqual("Press Enter to update data.", outputLines[17]);
        Assert.AreEqual("Press any other button to finish showing the weather.", outputLines[18]);
        Assert.AreEqual("The weather in Moscow now:", outputLines[19]);
        Assert.AreEqual("---------- TomorrowIo data ----------", outputLines[20]);
        Assert.AreEqual("---------- OpenWeatherMap data ----------", outputLines[28]);
        Assert.AreEqual(38, outputLines.Length);
    }
}
