namespace WeatherApplication;
using DataGetter;

public class WeatherWriter
{
    public string[] GeneralFields { get; } =
    {
        "temperature",
        "weather",
        "cloudiness",
        "humidity",
        "wind direction",
        "wind speed"
    };

    public string[] Units { get; } = { "", "", "%", "%", "deg", "m/s" };

    public string[] DataSources { get; } = { "TomorrowIo", "OpenWeatherMap" };

    public double[] LocationCoordinates { get; private set; }

    public string LocationName { get; private set; }

    private readonly string[] tomorrowIoFields =
    {
        "temperature",
        "weatherCode",
        "cloudCover",
        "humidity",
        "windDirection",
        "windSpeed"
    };

    private readonly string[] openWeatherMapFields =
    {
        "main.temp",
        "weather.main",
        "clouds.all",
        "main.humidity",
        "wind.deg",
        "wind.speed"
    };

    private readonly IWeatherGetter[] weatherGetters;

    public WeatherWriter(double[] locationCoordinates, string locationName, IWeatherGetter[] weatherGettersArray)
    {
        LocationName = locationName;
        LocationCoordinates = locationCoordinates;
        weatherGetters = weatherGettersArray;
    }

    public void WriteWeatherOnce()
    {
        string[]? tomorrowIoData = weatherGetters[0].GetWeather(LocationCoordinates, tomorrowIoFields);
        string[]? openWeatherMapData = weatherGetters[1].GetWeather(LocationCoordinates, openWeatherMapFields);
        var WeatherData = new List<string[]?> { tomorrowIoData, openWeatherMapData };

        Console.WriteLine($"The weather in {LocationName} now:");
        Console.WriteLine();

        for (int d = 0; d < WeatherData.Count; d++)
        {
            Console.WriteLine($"---------- {DataSources[d]} data ----------");
            if (WeatherData[d] == null)
            {
                Console.WriteLine("The server is not available.");
            }
            else
            {
                for (int i = 0; i < GeneralFields.Length; i++)
                {
                    if (GeneralFields[i] == "temperature")
                    {
                        Console.WriteLine($"temperature (°C): {WeatherData[d][i]}");
                        Console.WriteLine($"temperature (°F): {CelsiusToFahrenheit(Convert.ToDouble(WeatherData[d][i].Replace('.', ','))).ToString().Replace(',', '.')}");
                    }
                    else
                    {
                        Console.WriteLine($"{GeneralFields[i]}: {WeatherData[d][i]} {Units[i]}");
                    }
                }
            }

            Console.WriteLine();
        }
    }

    public void WriteWeatherManyTimes()
    {
        string? command = "";
        while (command == "")
        {
            WriteWeatherOnce();
            Console.WriteLine("Press Enter to update data.");
            Console.WriteLine("Press any other button to finish showing the weather.");
            command = Console.ReadLine();
        }
    }

    private double CelsiusToFahrenheit(double temperatureInСelsius)
    {
        return temperatureInСelsius * 9 / 5 + 32;
    }
}
