namespace WeatherApplication;
using DataGetter;

public class Program
{
    public static void Main(string[] args)
    {
        double[] location = new double[] { 59.939099, 30.315877 };
        string locationName = "St. Petersburg";
        if (args.Length != 1)
        {
            Console.WriteLine("The program expects strictly one argument - the path to the file with API keys.");
        }
        else
        {
            var weatherGetters = new IWeatherGetter[2];
            weatherGetters[0] = TomorrowIoWeatherGetter.CreateGetter(args[0]);
            weatherGetters[1] = OpenWeatherMapGetter.CreateGetter(args[0]);

            var weatherWriter = new WeatherWriter(location, locationName, weatherGetters);
            weatherWriter.WriteWeatherManyTimes();
        }
    }
}
