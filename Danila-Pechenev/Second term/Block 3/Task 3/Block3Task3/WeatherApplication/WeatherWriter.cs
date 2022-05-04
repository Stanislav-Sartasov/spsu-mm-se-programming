namespace WeatherApplication;
using DataGetter;

public class WeatherWriter
{
    public string[] GeneralFields { get; private set; }

    public string[] Units { get; private set; }

    public double[] LocationCoordinates { get; private set; }

    public string LocationName { get; private set; }

    public WeatherWriter(double[] locationCoordinates, string locationName, string[] generalFields, string[] units)
    {
        LocationName = locationName;
        LocationCoordinates = locationCoordinates;
        GeneralFields = generalFields;
        Units = units;
    }

    public void WriteWeatherOnce(List<IWeatherGetter> weatherGetters, Dictionary<string, string[]> weatherGettersFields)
    {
        Console.WriteLine($"The weather in {LocationName} now:");
        Console.WriteLine();

        for (int weatherGetter = 0; weatherGetter < weatherGetters.Count; weatherGetter++)
        {
            Console.WriteLine($"---------- {weatherGetters[weatherGetter].Name} data ----------");
            if (weatherGettersFields.ContainsKey(weatherGetters[weatherGetter].Name))
            {
                string[]? weatherData = weatherGetters[weatherGetter].GetWeather(LocationCoordinates, weatherGettersFields[weatherGetters[weatherGetter].Name]);
                if (weatherData == null)
                {
                    Console.WriteLine("Wrong key.");
                }
                else if (weatherData.Length == 0)
                {
                    Console.WriteLine("The server is not available.");
                }
                else
                {
                    for (int i = 0; i < GeneralFields.Length; i++)
                    {
                        if (GeneralFields[i] == "temperature")
                        {
                            Console.WriteLine($"temperature (°C): {weatherData[i]}");
                            Console.WriteLine($"temperature (°F): {CelsiusToFahrenheit(Convert.ToDouble(weatherData[i].Replace('.', ','))).ToString().Replace(',', '.')}");
                        }
                        else
                        {
                            Console.WriteLine($"{GeneralFields[i]}: {weatherData[i]} {Units[i]}");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Unknown service.");
            }

            Console.WriteLine();
        }
    }

    public void WriteWeatherManyTimes(List<IWeatherGetter> weatherGetters, Dictionary<string, string[]> weatherGettersFields)
    {
        string? command = "";
        while (command == "")
        {
            WriteWeatherOnce(weatherGetters, weatherGettersFields);
            Console.WriteLine("Press Enter to update data.");
            Console.WriteLine("Press any other button to finish showing the weather.");
            command = Console.ReadLine();
        }
    }

    protected double CelsiusToFahrenheit(double temperatureInСelsius)
    {
        return Math.Round(temperatureInСelsius * 9 / 5 + 32, 3);
    }
}
