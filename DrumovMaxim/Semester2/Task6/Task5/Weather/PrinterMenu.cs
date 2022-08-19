using WeatherPattern;
using WeatherManagerAPI;

namespace Weather
{
    public static class PrinterMenu
    {
        public static void PrintMenu()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Menu to choose sites: ");
            Console.WriteLine("\t A - Only Tomorrow.io");
            Console.WriteLine("\t B - Only StormGlass.io");
            Console.WriteLine("\t C or each other - Both Sites");
            Console.WriteLine("\t Esc - Exit");
            Console.ResetColor();
        }

        public static void ChooseSites(List<bool> sites, ConsoleKey cli)
        {
            DefaultSetSites(sites);

            if (cli == ConsoleKey.A)
            {
                sites[1] = false;
            }
            else if (cli == ConsoleKey.B)
            {
                sites[0] = false;
            }
            else if (cli == ConsoleKey.C || cli != ConsoleKey.Escape)
            {
                return;
            }
        }

        private static void DefaultSetSites(List<bool> sites)
        {
            sites[0] = true;
            sites[1] = true;
        }
    }
}
