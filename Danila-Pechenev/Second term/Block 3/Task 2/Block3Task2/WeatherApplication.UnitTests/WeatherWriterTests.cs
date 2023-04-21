namespace WeatherApplication.UnitTests;
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
        double[] location = new double[] { 55.75583, 37.6173 };
        string locationName = "Moscow";

        var weatherGetters = new IWeatherGetter[2];
        var mockTomorrowIo = new Mock<IWeatherGetter>();
        mockTomorrowIo.Setup(x => x.GetWeather(It.IsAny<double[]>(), It.IsAny<string[]>())).Returns(new string[] { "3.38", "Cloudy", "88", "72", "307.5", "4.5" });

        var mockOpenWeatherMap = new Mock<IWeatherGetter>();
        mockOpenWeatherMap.Setup(x => x.GetWeather(It.IsAny<double[]>(), It.IsAny<string[]>())).Returns(new string[] { "3.12", "Clouds", "100", "82", "300", "7" });
        weatherGetters[0] = mockTomorrowIo.Object;
        weatherGetters[1] = mockOpenWeatherMap.Object;

        var weatherWriter = new WeatherWriter(location, locationName, weatherGetters);
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        weatherWriter.WriteWeatherOnce();
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

        double[] location = new double[] { 55.75583, 37.6173 };
        string locationName = "Moscow";

        var weatherGetters = new IWeatherGetter[2];
        var mockTomorrowIo = new Mock<IWeatherGetter>();
        mockTomorrowIo.Setup(x => x.GetWeather(It.IsAny<double[]>(), It.IsAny<string[]>())).Returns(new string[] { "3.38", "Cloudy", "88", "72", "307.5", "4.5" });

        var mockOpenWeatherMap = new Mock<IWeatherGetter>();
        mockOpenWeatherMap.Setup(x => x.GetWeather(It.IsAny<double[]>(), It.IsAny<string[]>())).Returns(new string[] { "3.12", "Clouds", "100", "82", "300", "7" });
        weatherGetters[0] = mockTomorrowIo.Object;
        weatherGetters[1] = mockOpenWeatherMap.Object;

        var weatherWriter = new WeatherWriter(location, locationName, weatherGetters);
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        weatherWriter.WriteWeatherManyTimes();
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
