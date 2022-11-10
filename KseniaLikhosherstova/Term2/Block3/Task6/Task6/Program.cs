using Model;

namespace Task6
{
    internal class Program
    {
        public static void Main()
        {
            Start();
        }

        private static void Start()
        {
            IApi api;

            while (true)
            {
                Console.Clear();

                var greetNumber = Printer.Greetings();

                if (greetNumber == Choice.Exit)
                {
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    Environment.Exit(0);
                }

                if (greetNumber == Choice.OpenWeatherMap)
                {
                    try
                    {
                        api = IoCContainer.Container().First(x => x.ApiName == "OpenWeatherMap");
                        var data = api.GetData();
                        Printer.PrintWeather(data);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else if (greetNumber == Choice.TommorowIo)
                {
                    try
                    {
                        api = IoCContainer.Container().First(x => x.ApiName == "TomorrowIo");
                        var data = api.GetData();
                        Printer.PrintWeather(data);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else if (greetNumber == Choice.Both)
                {
                    foreach (var site in IoCContainer.Container())
                    {
                        try
                        {
                            Printer.PrintWeather(site.GetData());
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }
}













