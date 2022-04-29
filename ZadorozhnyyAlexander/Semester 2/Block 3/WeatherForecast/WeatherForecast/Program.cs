using AbstractWeatherForecast;


class Program
{
    static void Main()
    {
        bool isOutFromProgram = false;
        bool isStormglassActive = false;
        bool isOpenweatherActive = false;

        Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        ShowDescription();

        AWeatherForecast stormglass = new StormglassWeatherForecast.StormglassWeatherForecast(new HttpClient());
        try
        {
            stormglass.Update();
            isStormglassActive = !isStormglassActive;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Attension! It is currently impossible to get a response from the Stormglass service.\n" +
                $"Use the \\update command to check again.: {ex.Message}");
        }

        AWeatherForecast openweather = new OpenweatherWeatherForecast.OpenweatherWeatherForecast(new HttpClient());
        try
        {
            openweather.Update();
            isOpenweatherActive = !isOpenweatherActive;
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
                    if (isStormglassActive)
                        stormglass.ShowFullWeatherForecast();
                    else
                        Console.WriteLine("Something went wrong!");
                    break;
                case "\\openweather":
                    if (isOpenweatherActive)
                        openweather.ShowFullWeatherForecast();
                    else
                        Console.WriteLine("Something went wrong!");
                    break;
                case "\\update":
                    try
                    {
                        stormglass.Update();
                        Console.WriteLine("Data from Stormglass succesfully updated!");
                    }
                    catch (Exception ex)
                    { 
                        Console.WriteLine($"It is impossible to update data from the Stormglass website for the reason: {ex.Message}");
                    }

                    try
                    {
                        openweather.Update();
                        Console.WriteLine("Data from Openweather succesfully updated!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"It is impossible to update data from the Openweather website for the reason: {ex.Message}");
                    }
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
            "5) \\openweather - Weather forecast from site: Openweather\n");
    }
}
