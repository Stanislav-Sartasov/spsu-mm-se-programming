using WeatherManagerAPI;
using IoCContainer;

namespace Weather
{
    class Program
    {
        public static void Main()
        {
            List<bool> siteList = new List<bool>() { true, false };
            var container = new WeatherIoCContainer();
            ConsoleKeyInfo cli;
            Console.SetWindowSize(61, 26);

            do
            {
                int countSites = 0;
                Console.Clear();
                WeatherPrinter.PrintGreetings();

                var APIs = container.GetTypesContainer(siteList);

                foreach(var api in APIs)
                {
                    api.EmptyPattern();
                    WeatherPrinter.WeatherOutput(api);
                    countSites++;
                    if(countSites == 1)
                    {
                        Console.SetWindowSize(61, 26);
                    }
                    else
                    {
                        Console.SetWindowPosition(0, 0);
                        Console.SetWindowSize(61, 33);
                    }
                }

                PrinterMenu.PrintMenu();
                cli = Console.ReadKey();
                PrinterMenu.ChooseSites(siteList, cli.Key);
            } while (cli.Key != ConsoleKey.Escape);
            Console.Clear();
            Console.WriteLine("\tkThx for using our program, have a nice day\n");
        }
    }
}