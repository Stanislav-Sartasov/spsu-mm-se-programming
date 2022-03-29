using Cards;
using BotStructure;
using BaseBot;
using KellyBot;
using ThorpBot;

namespace Casino
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine(
                "This program implements blackjack with basic rules and 3 bots with different strategies.\n" +
                "Now will be launched 1000 gaming sessions of 40 draws and\n" +
                "will be calculated minimum, average and maximum balance of bots after this sessions.\n"
                );

            Console.WriteLine("Bot with base strategy:");
            GamesLaunch("base");

            Console.WriteLine("Bot with strategy based on <<Thorp system>>:");
            GamesLaunch("thorp");

            Console.WriteLine("Bot with strategy based on <<Kelly criterion>>:");
            GamesLaunch("kelly");
        }

        private static void GamesLaunch(string strategy)
        {
            int min = int.MaxValue;
            int avg = 0;
            int max = 0;

            for (int i = 0; i < 1000; i++)
            {
                IBot bot;
                if (strategy == "base")
                    bot = new BaseStrategyBot(1000);
                else if (strategy == "thorp")
                    bot = new ThorpSystemBot(1000);
                else if (strategy == "kelly")
                    bot = new KellyCriterionBot(1000);
                else
                    return;

                Casino.Game(bot, 40, new Shoes());
                max = max > bot.Balance ? max : bot.Balance;
                min = min < bot.Balance ? min : bot.Balance;
                avg += bot.Balance;
            }

            Console.WriteLine("Minimum balance = " + min);
            Console.WriteLine("Average balance = " + avg / 1000);
            Console.WriteLine("Maximum balance = " + max + "\n");
        }
    }
}