using ISites;
using static System.Console;
using Sites;

namespace Task_3
{
    class Programm
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                WriteLine("Not enough or too much arguments");
                return;
                  
            }

            var possibleArguments = new string[] { "0", "1" };
            if (!args.All(x => possibleArguments.Contains(x)))
            {
                WriteLine("Invalid argument");
                return;
            }

            var container = new IoCContainer();
            if (args[0] == "0") container = container.WithInactiveSite(SiteName.OpenWeatherMap);
            else container = container.WithActiveSite(SiteName.OpenWeatherMap);

            if (args[1] == "0") container = container.WithInactiveSite(SiteName.TommorowIo);
            else container = container.WithActiveSite(SiteName.TommorowIo);


            WriteLine("This programm show weather in Saint-Petersburg.\n");

            List<ISite> sites = container.GetSites();

            while (true)
            {
                WriteLine("Time now: " + DateTime.Now);
                WriteLine();

                foreach (ISite site in sites)
                {
                    site.ShowWeather();
                    WriteLine();
                }

                Write("Refresh is any input, exit is \"exit\".\n> ");
                string answer = ReadLine();
                if (answer.ToLower() == "exit")
                {
                    break;
                }
                Clear();
            }
        }
    }
}