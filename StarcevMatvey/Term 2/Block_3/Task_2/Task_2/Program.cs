using ISites;
using Sites;
using static System.Console;

namespace Task_2
{
    class Programm
    {
        static void Main(string[] args)
        {
            WriteLine("This programm show weather in Saint-Petersburg.\n");

            List<ISite> sites = new List<ISite>
            {
                new OpenWeatherMap(),
                new TomorrowIo()
            };

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
                string answer = Console.ReadLine();
                if (answer.ToLower() == "exit")
                {
                    break;
                }
                Clear();
            }
        }
    }
}