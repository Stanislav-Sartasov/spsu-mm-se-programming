using System;
using AbstractClasses;
using BlackjackMechanics.GameTools;
using PlaginLoaderLibrary;


namespace BlackjackGame
{
    public class Program
    {
        static void Main(String[] args)
        {
            int countGames = 50000;
            int startMoney = 10000;
            int startRate = 500;
            int sumAfterGame = 0;
            int countLoseAllBalanceGames = 0;

            Console.WriteLine("This program show how work uploading dll files(bots).\n" +
                "You have to specify the path to the folder where the bots are stored as the only argument.");

            if (args.Length != 1)
            {
                Console.WriteLine("You entered the wrong number of arguments. Please, try again.");
                return;
            }

            PlaginLoader loader = new PlaginLoader(args[0], startMoney, startRate);

            for (int botNumber = 0; botNumber < loader.AllBots.Count; botNumber++)
            {
                Console.WriteLine($"Demonstration of {botNumber + 1} bot. Start money --> {startMoney}");

                var botSample = loader.AllBots[botNumber];
                for (int i = 0; i < countGames; i++)
                {
                    var bot = (ABot)botSample.Clone();
                    Game game = new Game(bot);
                    game.CreateGame(8);
                    game.StartGame();
                    sumAfterGame += (int)bot.Money;
                    if (bot.Money <= 0)
                        countLoseAllBalanceGames++;
                    
                }

                Console.WriteLine($"{botNumber + 1}) {botSample.GetType()} statistic: ");
                Console.WriteLine($"Average winnings over 40 games ---> {sumAfterGame / 50000}/{startMoney} <--- Your start balance");
                Console.WriteLine("The number of games when a player loses his entire balance ---> " + countLoseAllBalanceGames + "\n");

                sumAfterGame = 0;
                countLoseAllBalanceGames = 0;
            }
        }
    }
}