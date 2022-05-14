namespace IoC;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DataGetter;

public static class Container
{
    public static string[] AvailableServices { get; private set; } =
    {
        "TomorrowIo",
        "OpenWeatherMap"
    };

    public static List<string> UsedServices { get; private set; } = new List<string>()
    {
        "TomorrowIo",
        "OpenWeatherMap"
    };

    public static bool AddService(string name)
    {
        if (UsedServices.Contains(name))
        {
            return true;
        }
        else if (AvailableServices.Contains(name))
        {
            UsedServices.Add(name);
            return true;
        }

        return false;
    }

    public static bool RemoveService(string name)
    {
        if (UsedServices.Contains(name))
        {
            UsedServices.Remove(name);
            return true;
        }

        return false;
    }

    public static List<IWeatherGetter> GetWeatherGetters(string pathToFileWithKey)
    {
        var services = Host.CreateDefaultBuilder()
            .ConfigureServices(services => services
            .AddSingleton<IWeatherGetter, TomorrowIoWeatherGetter>(provider => UsedServices.Contains("TomorrowIo") ? TomorrowIoWeatherGetter.CreateGetter(pathToFileWithKey) : null)
            .AddSingleton<IWeatherGetter, OpenWeatherMapGetter>(provider => UsedServices.Contains("OpenWeatherMap") ? OpenWeatherMapGetter.CreateGetter(pathToFileWithKey) : null)
            )
            .Build().Services;

        return services.GetServices<IWeatherGetter>().Where(service => service != null).ToList();
    }
}
