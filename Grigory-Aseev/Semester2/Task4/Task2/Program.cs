using Plugins;
using PlayerStructure;

namespace Task4
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("This program loads plug-ins to simulate blackjack with basic American rules.\nIt displays the statistics of 1000 games with 40 bot bets.\nThis program loads plugins by default, you can also load your own plugins.\n");

            string pathToPlugins = "../../../../Plugins/Dlls/";
            PlayWithPlugins(pathToPlugins);

            Console.WriteLine("Press any key if you want to load your plugins or press Escape to exit the application.");
            while (Console.ReadKey(true).Key != ConsoleKey.Escape)
            {
                Console.WriteLine("Enter either the full path to the plugin with the dll extension or enter the folder where it is located.");
                Console.Write("The path: ");
                pathToPlugins = Console.ReadLine();
                Console.WriteLine();
                PlayWithPlugins(pathToPlugins);
                Console.WriteLine("Press any key if you want to load your plugins or press Escape to exit the application.");
            }
        }

        private static void PlayWithPlugins(string? path)
        {
            BotsLoader loader = new BotsLoader();
            CasinoLaunch casino;
            (List<IPlayer>, List<string>) bots;

            int length = loader.Load(path);
            bots = loader.TakeBots();

            for (int i = 0; i < length; i++)
            {
                casino = new CasinoLaunch(bots.Item1[i], bots.Item2[i]);
                casino.StartCasino();
                casino.PrintInfo();
            }
        }
    }
}