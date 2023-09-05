using AbstractWeatherForecast;
using StormglassWeatherForecast;
using OpenweatherWeatherForecast;
using DataParsers;
using IoC;

class Program
{
    private const string openweatherKey = "b13eb5267bf4c9746e2f70d69a172b94";
    private const string stormglassKey = "b720d1e4-c40a-11ec-844a-0242ac130002-b720d252-c40a-11ec-844a-0242ac130002";

    public static void Main()
    {
        bool isOutFromProgram = false;

        Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        ShowDescription();

        var weatherContainer = new ContainerConfig()
            .AddService<OpenweatherForecast, OpenweatherParser>(openweatherKey, (int)SiteTypes.Openweather)
            .AddService<StormglassForecast, StormglassParser>(stormglassKey, (int)SiteTypes.Stormglass);

        try
        {
            weatherContainer.GetWeatherForecast<StormglassForecast>().Update();
        }
        catch (KeyNotFoundException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Attension! It is currently impossible to get a response from the Stormglass service.\n" +
                $"Use the \\update command to check again.: {ex.Message}");
        }
        
        try
        {
            weatherContainer.GetWeatherForecast<OpenweatherForecast>().Update();
        }
        catch (KeyNotFoundException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Attension! It is currently impossible to get a response from the Openweather service.\n" +
                $"Use the \\update command to check again.: {ex.Message}");
        }

        while(!isOutFromProgram)
        {
            switch(Console.ReadLine().ToLower())
            {
                case "\\stormglass":
                    if (weatherContainer.IsServiceActive<StormglassForecast>())
                        weatherContainer.GetWeatherForecast<StormglassForecast>().ShowFullWeatherForecast();
                    else
                        Console.WriteLine("Something went wrong!");
                    break;
                case "\\openweather":
                    if (weatherContainer.IsServiceActive<OpenweatherForecast>())
                        weatherContainer.GetWeatherForecast<OpenweatherForecast>().ShowFullWeatherForecast();
                    else
                        Console.WriteLine("Something went wrong!");
                    break;
                case "\\update":
                    try
                    {
                        weatherContainer.GetWeatherForecast<StormglassForecast>().Update();
                        Console.WriteLine("Data from Stormglass succesfully updated!");
                    }
                    catch(KeyNotFoundException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"It is impossible to update data from the Stormglass website for the reason: {ex.Message}");
                    }

                    try
                    {
                        weatherContainer.GetWeatherForecast<OpenweatherForecast>().Update();
                        Console.WriteLine("Data from Openweather succesfully updated!");
                    }
                    catch (KeyNotFoundException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"It is impossible to update data from the Openweather website for the reason: {ex.Message}");
                    }
                    break;
                case "\\turn off stormglass":
                    weatherContainer.RemoveService<StormglassForecast>();
                    Console.WriteLine("Service Stormglass turned off.");
                    break;
                case "\\turn off openweather":
                    weatherContainer.RemoveService<OpenweatherForecast>();
                    Console.WriteLine("Service Openweather turned off.");
                    break;
                case "\\turn on stormglass":
                    weatherContainer.AddService<StormglassForecast, StormglassParser>(stormglassKey, (int)SiteTypes.Stormglass);
                    Console.WriteLine("Service Stormglass turn on! Try to update!");
                    break;
                case "\\turn on openweather":
                    weatherContainer.AddService<OpenweatherForecast, OpenweatherParser>(openweatherKey, (int)SiteTypes.Openweather);
                    Console.WriteLine("Openweather turn on! Try to update!");
                    break;
                case "\\exit":
                    isOutFromProgram = !isOutFromProgram;
                    Console.WriteLine("The work of the program is completed.");
                    break;
                case "\\help":
                    ShowDescription();
                    break;
                default:
                    Console.WriteLine("Unknown command. Try again or use \\help.");
                    break;
            }
        }   
    }

    private static void ShowDescription()
    {
        Console.WriteLine("This program shows the weather forecast in St. Petersburg.\n" +
            "The following commands are available to you:\n" +
            "1) \\help - Output a description of the program\n" +
            "2) \\exit - Exit the program\n" +
            "3) \\update - Update data\n" +
            "4) \\stormglass - Weather forecast from site: Stormglass\n" +
            "5) \\turn on stormglass - Turn on weather forecast from site: Stormglass\n" +
            "6) \\turn off stormglass - Turn off weather forecast from site: Stormglass\n" +
            "7) \\openweather - Weather forecast from site: Stormglass\n" +
            "8) \\turn on openweather - Turn on weather forecast from site: Openweather\n" +
            "9) \\turn off openweather - Turn off weather forecast from site: Openweather\n");
    }
}
