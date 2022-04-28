using Tools;
using Container;
using SiteInterface;

namespace WeatherViewer
{
    public static class Program
    {
        public static void Main()
        {
            Console.WriteLine("The program shows the weather from 3 source\n");

            var sites = IoCContainer.GetSites(WeatherParameter.Current);
            bool flag = true;
            bool updateFlag = true;
            bool changedFlag = false;

            while (flag)
            {
                if (updateFlag)
                {
                    foreach (var site in sites)
                    {
                        Console.WriteLine(Tool.ConvertWeatherToString(site.GetCityWeatherForecast()));
                    }

                    updateFlag = false;
                }

                if (changedFlag)
                {
                    sites = IoCContainer.GetSites(WeatherParameter.Current);
                    changedFlag = false;
                }

                Console.WriteLine("If you want to update data from sites, then write - 'Update'.\nAvailable sites: OpenWeather, StormGlass, TomorrowIO\n" +
                                  "You may add or delete site using such command: 'Add [site name]' or 'Delete [site Name]'\nYou can also view the sites " +
                                  "from which data is requested using command: 'Sites'\nIf you want to close the program, then write - 'Exit'.\n");

                var userAnswer = Console.ReadLine().ToLower();
                var commands = userAnswer.Split(" ");

                while (userAnswer.Last() == ' ')
                {
                    userAnswer = userAnswer.Remove(userAnswer.Length - 1);
                }

                while (userAnswer != "exit" && userAnswer != "update" && !Tool.CheckCommand(commands))
                {
                    Console.WriteLine("Wrong command. Try again, please.\nIf you want to update data from sites, then write - 'Update'.\n" +
                                      "You may add or delete site using such command: 'Add [site name]' or 'Delete [site name]'\nYou can also view " +
                                      "the sites from which data is requested using command: 'Sites'\nIf you want to close the program, then write - 'Exit'.\n");

                    userAnswer = Console.ReadLine().ToLower().Replace(" ", "");
                    commands = userAnswer.Split(" ");

                    while (userAnswer.Last() == ' ')
                    {
                        userAnswer.Remove(userAnswer.Length - 1);
                    }
                }

                if (userAnswer == "exit")
                {
                    flag = false;
                }
                else if (commands[0] == "sites")
                {
                    Console.Write("\nThese sites are connected: ");

                    foreach (var site in sites)
                    {
                        Console.Write(site.Name + " ");
                    }

                    Console.WriteLine("\n");
                }
                else if (commands[0] == "add")
                {
                    var name = Tool.GetSite(commands[1]);

                    if (IoCContainer.ConnectedSites.Contains(name))
                    {
                        Console.WriteLine("\nThis site is alreade connected :)");
                    }
                    else
                    {
                        IoCContainer.AddSiteToContainer(name);
                        changedFlag = true;
                    }

                    Console.WriteLine("\n");
                }
                else if (commands[0] == "delete")
                {
                    var name = Tool.GetSite(commands[1]);

                    if (!IoCContainer.ConnectedSites.Contains(name))
                    {
                        Console.WriteLine("\nThis site is alreade disconnected :)");
                    }
                    else
                    {
                        IoCContainer.RemoveSiteFromContainer(name);
                        changedFlag = true;
                    }

                    Console.WriteLine("\n");
                }
                else if (commands[0] == "update")
                {
                    updateFlag = true;
                }
            }

            Console.WriteLine("\nThat's all! Thank you!\n");
            return;
        }
    }
}