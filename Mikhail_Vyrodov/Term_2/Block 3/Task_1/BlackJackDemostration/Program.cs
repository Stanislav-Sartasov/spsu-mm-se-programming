using System;
using BlackjackLibrary;
using DecksLibrary;
using PluginLibrary;
using System.Reflection;

namespace BlackJackDemostration
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("This programm represents the blackjack game with opportunity to split");
            Console.Write(" cards, double wagers and surrender.\n");
            Console.WriteLine("There are 3 strategies to play:");
            Console.WriteLine("Basic strategy");
            Console.WriteLine("Cards counting strategy, based on basic strategy and cards count");
            Console.WriteLine("Simple strategy, based on basic strategy and players score at the moment");
            Console.WriteLine("There are 3 bots with different strategies");
            Console.WriteLine("The programm will print the approximate sum of their wagers when the game ends");
            Decks playingDecks = new Decks();
            playingDecks.FillCards();
            double win = 0;
            try
            {
                BlackjackGame game = new BlackjackGame(playingDecks, 1600); // 1600 is initial players money
                for (int botsCount = 0; botsCount < 3; botsCount++)
                {
                    Console.WriteLine("The turn of " + (botsCount + 1).ToString() + " bot.");
                    LibraryLoader libraryLoader = new LibraryLoader("../../../../BotsLibrary/BotsLibrary.dll");
                    Assembly asm = libraryLoader.LoadLibrary();
                    PluginHelper pluginHelper = new PluginHelper(asm);
                    object[] parameters = new object[] { game.PlayingDealer.DealerCards[0], game.InitialMoney, game.PlayingCards, (uint)16 };
                    pluginHelper.CreatePlayer(parameters, botsCount);
                    game.Player = pluginHelper.players[botsCount];
                    for (int i = 0; i < 40; i++)
                    {
                        win += game.Game();
                    }
                    Console.WriteLine("Approximate sum with 40 played rounds is {0}", win / 40.0);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("Press enter button to exit");
                Console.ReadLine();
            } 
        }
    }
}
