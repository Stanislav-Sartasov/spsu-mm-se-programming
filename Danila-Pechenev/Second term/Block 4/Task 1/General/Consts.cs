namespace General;

public static class Consts
{
    public static double[] LocationCoordinates { get; } = new double[] { 59.939099, 30.315877 };

    public static string LocationName { get; } = "St. Petersburg";

    public static string[] GeneralFields { get; } = { "temperature", "weather", "cloudiness", "humidity", "wind direction", "wind speed" };

    public static string[] Units { get; } = { "", "", "%", "%", "deg", "m/s" };

    public static Dictionary<string, string[]> weatherGettersFields { get; } =
        new Dictionary<string, string[]>()
        {
            { "TomorrowIo", new string[]
                { "temperature",
                "weatherCode",
                "cloudCover",
                "humidity",
                "windDirection",
                "windSpeed" }
            },
            { "OpenWeatherMap", new string[]
                { "main.temp",
                "weather.main",
                "clouds.all",
                "main.humidity",
                "wind.deg",
                "wind.speed" }
            }
        };

    public static string PathToFileWithKeys { get; } = "../../../../General/APIKeys.json";
}
