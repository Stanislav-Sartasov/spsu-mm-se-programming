using System;
using BlackjackLibrary;
using DecksLibrary;
using System.Reflection;

namespace BlackJackDemostration
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("This programm represents the blackjack game with opportunity to split");
            Console.Write(" cards, double wagers and surrender.\n");
            Console.WriteLine("You can choose one of 3 strategies to play:");
            Console.WriteLine("0 - basic strategy");
            Console.WriteLine("1 - cards counting strategy, based on basic strategy and cards count");
            Console.WriteLine("2 - simple strategy, based on basic strategy and players score at the moment");
            Console.WriteLine("The programm will print the approximate sum of your wagers when the game ends");
            Console.WriteLine("Please enter the correct number of strategy you want to play");
            int strategy = Convert.ToInt32(Console.ReadLine()); // in ascii table "0" has 48 number
            if (strategy < 0 || strategy > 2)
            {
                Console.WriteLine("You number isn't right.");
                return;
            }
            Bots botsStrategy = (Bots)strategy;
            Decks playingDecks = new Decks();
            playingDecks.FillCards();
            double win = 0;
            try
            {
                BlackjackGame game = new BlackjackGame(playingDecks, 1600, botsStrategy); // 1600 is initial players money
                for (int i = 0; i < 40; i++)
                {
                    win += game.Game();
                }
                Console.WriteLine("Approximate sum with 40 played rounds is {0}", win / 40.0);
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
