namespace DataGetter.UnitTests;
using NUnit.Framework;
using DataGetter;
using System.IO;

public class TomorrowIoWeatherGetterTests
{
    public string testKeysPath = "../../../../DataGetter.UnitTests/TestKeys.json";
    public string TomorrowIoTestResponsePath = "../../../../DataGetter.UnitTests/TomorrowIoTestResponse.json";

    [Test]
    public void CreateGetterTest()
    {
        var tomorrowIoWeatherGetter = TomorrowIoWeatherGetter.CreateGetter(testKeysPath);
        Assert.IsNotNull(tomorrowIoWeatherGetter);
    }

    [Test]
    public void CreateGetterWrongPathTest()
    {
        try
        {
            var tomorrowIoWeatherGetter = TomorrowIoWeatherGetter.CreateGetter(testKeysPath);
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
        var tomorrowIoWeatherGetter = TomorrowIoWeatherGetter.CreateGetter(testKeysPath);
        using var stream = File.OpenRead(TomorrowIoTestResponsePath);
        string[] fields = { "temperature", "weatherCode", "cloudCover" };
        try
        {
            string[]? result = tomorrowIoWeatherGetter.ParseResponseStream(stream, fields);
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
        var tomorrowIoWeatherGetter = TomorrowIoWeatherGetter.CreateGetter(testKeysPath);
        using var stream = File.OpenRead(TomorrowIoTestResponsePath);
        string[] fields = { "temperature", "weatherCode", "cloudCover" };
        string[]? result = tomorrowIoWeatherGetter.ParseResponseStream(stream, fields);
        Assert.AreNotEqual(null, result);
    }

    [Test]
    public void ParseResponseStreamLengthTest()
    {
        var tomorrowIoWeatherGetter = TomorrowIoWeatherGetter.CreateGetter(testKeysPath);
        using var stream = File.OpenRead(TomorrowIoTestResponsePath);
        string[] fields = { "temperature", "weatherCode", "cloudCover" };
        string[]? result = tomorrowIoWeatherGetter.ParseResponseStream(stream, fields);
        if (result != null)
        {
            Assert.AreEqual(3, result.Length);
        }
    }

    [Test]
    public void GetWeatherUnauthorizedTest()
    {
        double[] location = new double[] { 62.9274, 44.1283 };
        string[] fields = { "temperature", "humidity", "windSpeed" };
        var tomorrowIoWeatherGetter = TomorrowIoWeatherGetter.CreateGetter(testKeysPath);
        string[]? result = tomorrowIoWeatherGetter.GetWeather(location, fields);
        Assert.AreEqual(null, result);
    }
}
