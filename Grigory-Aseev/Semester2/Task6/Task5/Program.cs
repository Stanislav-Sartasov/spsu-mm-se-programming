using SiteInterfaces;
using IoCContainerTools;

namespace Task6
{
    class Program
    {
        public static void Main()
        {
            IEnumerable<ISite> sites;
            Console.WriteLine("This program shows data (temperature (degrees and Fahrenheit), clouds, humidity, precipitation,\nwind direction and speed) of the current weather in the city of St. Petersburg from three different sources.");
            string sources = "===> openweathermap, tomorrow.io, stormglass.io <===";
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            PrintTools.PrintCentrally(sources);
            Console.ResetColor();
            Console.WriteLine();
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                string? availableServices = IoCContainer.Sites.FirstOrDefault()?.Name;
                for (int i = 1; i < IoCContainer.Sites.Count; i++)
                {
                    availableServices = availableServices + ", " + IoCContainer.Sites[i].Name;
                }
                if (availableServices is not null)
                {
                    PrintTools.PrintCentrally($"{availableServices} are connected.");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("None of the sites are connected.");
                }
                Console.ResetColor();
                sites = IoCContainer.GetSites();
                foreach (var site in sites)
                {
                    PrintTools.PrintCentrally($"Requesting data from the site: {site.SiteAddress} ...");
                    site.GetRequest();
                    PrintTools.PrintCentrally("The data is being processed...");
                    site.Parse();
                }
                Console.WriteLine();
                PrintTools.PrintForecast(sites.ToList());
                Console.WriteLine();
                PrintTools.PrintCentrally("Press the Esc key to exit the application, the A key to go to adding sites,");
                PrintTools.PrintCentrally("the D key to go to deleting sites or any other key to update the weather data");
                ConsoleKey usersRequest = Console.ReadKey(true).Key;
                Console.Clear();
                if (usersRequest == ConsoleKey.Escape)
                {
                    break;
                }
                else if (usersRequest == ConsoleKey.A || usersRequest == ConsoleKey.D)
                {
                    List<Type> types = new List<Type>();
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    PrintTools.PrintCentrally("You are in the services setup menu, to exit from here, type \"exit\".");
                    PrintTools.PrintCentrally("To interact with the website service, just type its name (for example: \"OpenWeatherMap\", \"TomorrowIO\" or \"StormGlassIO\")");
                    Console.ResetColor();
                    Console.WriteLine();
                    while (true)
                    {
                        Type? type;
                        bool conditionToExit;
                        if (usersRequest == ConsoleKey.A)
                        {
                            Console.Write("Add a site with the name: ");
                        }
                        else
                        {
                            Console.Write("Delete a site with the name: ");
                        }
                        (type, conditionToExit) = UserInputHandler.ReadType(Console.ReadLine());
                        if (conditionToExit)
                        {
                            Console.WriteLine("You are exiting the service setup menu...");
                            break;
                        }
                        Console.ForegroundColor = ConsoleColor.Red;
                        if (type is not null)
                        {
                            if (usersRequest == ConsoleKey.A)
                            {
                                if (IoCContainer.ConnectSite(type))
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("Service successfully added.");
                                }
                                else
                                {
                                    Console.WriteLine("Service was already available.");
                                }
                            }
                            else
                            {
                                if (IoCContainer.DisconnectSite(type))
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("Service successfully deleted");
                                }
                                else
                                {
                                    Console.WriteLine("Service was already unavailable.");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("The web service with this name was not found, try again...\navailable services: >>> OpenWeatherMap, TomorrowIO, StormGlassIO <<<\ncommands: >> exit << ");
                        }
                        Console.ResetColor();
                    }
                }
                Console.Clear();
            }
            Console.ForegroundColor = ConsoleColor.Magenta;
            PrintTools.PrintCentrally("I hope these sources helped you, thank you for using my application, see you soon ^_^");
            Console.ResetColor();
        }
    }
}