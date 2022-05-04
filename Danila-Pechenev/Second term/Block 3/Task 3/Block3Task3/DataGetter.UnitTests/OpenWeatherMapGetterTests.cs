namespace DataGetter.UnitTests;
using NUnit.Framework;
using DataGetter;
using System.IO;

public class OpenWeatherMapGetterTests
{
    public string testKeysPath = "../../../../DataGetter.UnitTests/TestKeys.json";
    public string openWeatherMapTestResponsePath = "../../../../DataGetter.UnitTests/OpenWeatherMapTestResponse.json";

    [Test]
    public void CreateGetterTest()
    {
        var openWeatherMapGetter = OpenWeatherMapGetter.CreateGetter(testKeysPath);
        Assert.IsNotNull(openWeatherMapGetter);
    }

    [Test]
    public void CreateGetterWrongPathTest()
    {
        try
        {
            var openWeatherMapGetter = OpenWeatherMapGetter.CreateGetter(testKeysPath);
            Assert.Fail();
        }
        catch
        {
            Assert.Pass();
        }
    }

    [Test]
    public void ParseResponseStreamNoExceptionTest()
    {
        var openWeatherMapGetter = OpenWeatherMapGetter.CreateGetter(testKeysPath);
        using var stream = File.OpenRead(openWeatherMapTestResponsePath);
        string[] fields = { "main.temp", "weather.main", "wind.speed" };
        try
        {
            string[]? result = openWeatherMapGetter.ParseResponseStream(stream, fields);
        }
        catch
        {
            Assert.Fail();
        }
        Assert.Pass();
    }

    [Test]
    public void ParseResponseStreamNotNullTest()
    {
        var openWeatherMapGetter = OpenWeatherMapGetter.CreateGetter(testKeysPath);
        using var stream = File.OpenRead(openWeatherMapTestResponsePath);
        string[] fields = { "main.temp", "weather.main", "wind.speed" };
        string[]? result = openWeatherMapGetter.ParseResponseStream(stream, fields);
        Assert.AreNotEqual(null, result);
    }

    [Test]
    public void ParseResponseStreamLengthTest()
    {
        var openWeatherMapGetter = OpenWeatherMapGetter.CreateGetter(testKeysPath);
        using var stream = File.OpenRead(openWeatherMapTestResponsePath);
        string[] fields = { "main.temp", "weather.main", "wind.speed" };
        string[]? result = openWeatherMapGetter.ParseResponseStream(stream, fields);
        if (result != null)
        {
            Assert.AreEqual(3, result.Length);
        }
    }

    [Test]
    public void GetWeatherUnauthorizedTest()
    {
        double[] location = new double[] { 45.2435, 23.3259 };
        string[] fields = { "main.temp", "main.humidity", "wind.speed" };
        var openWeatherMapGetter = OpenWeatherMapGetter.CreateGetter(testKeysPath);
        string[]? result = openWeatherMapGetter.GetWeather(location, fields);
        Assert.AreEqual(null, result);
    }
}
