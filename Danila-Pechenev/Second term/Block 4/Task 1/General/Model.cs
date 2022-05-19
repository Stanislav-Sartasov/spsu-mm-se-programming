namespace General;
using DataGetter;

public class Model
{
    private Dictionary<string, IWeatherGetter> weatherGetters = new Dictionary<string, IWeatherGetter>();

    public Model()
    {
        var allWeatherGetters = IoC.Container.GetAllWeatherGetters(Consts.PathToFileWithKeys);
        List<string> services = IoC.Container.UsedServices;
        for (int i = 0; i < services.Count; i++)
        {
            weatherGetters[services[i]] = allWeatherGetters[i];
        }
    }

    public string GetData(string service)
    {
        string result = "";
        result += $"{service} data:" + Environment.NewLine + Environment.NewLine;
        if (Consts.weatherGettersFields.ContainsKey(service))
        {
            string[]? weatherData = weatherGetters[service].GetWeather(Consts.LocationCoordinates, Consts.weatherGettersFields[service]);
            if (weatherData == null)
            {
                result += "Wrong key." + Environment.NewLine;
            }
            else if (weatherData.Length == 0)
            {
                result += "The server is not available." + Environment.NewLine;
            }
            else
            {
                for (int i = 0; i < Consts.GeneralFields.Length; i++)
                {
                    if (Consts.GeneralFields[i] == "temperature")
                    {
                        result += $"temperature (°C): {weatherData[i]}" + Environment.NewLine;
                        result += $"temperature (°F): {CelsiusToFahrenheit(Convert.ToDouble(weatherData[i].Replace('.', ','))).ToString().Replace(',', '.')}" + Environment.NewLine;
                    }
                    else
                    {
                        result += $"{Consts.GeneralFields[i]}: {weatherData[i]} {Consts.Units[i]}" + Environment.NewLine;
                    }
                }
            }
        }
        else
        {
            result += "Unknown service." + Environment.NewLine;
        }

        return result;
    }

    protected double CelsiusToFahrenheit(double temperatureInСelsius)
    {
        return Math.Round(temperatureInСelsius * 9 / 5 + 32, 3);
    }
}
