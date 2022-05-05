using Cards;
using BotStructure;
using BaseBot;
using KellyBot;
using ThorpBot;
using Plugins;

namespace Casino
{
    class Program
    {
        static void Main()
        {
            var bots = BotsLoader.LoadBots("../../../../Plugins/BotsDlls");

            Console.WriteLine(
                "This program implements blackjack with basic rules and 3 bots with different strategies.\n" +
                "Now will be launched 1000 gaming sessions of 40 draws and\n" +
                "will be calculated minimum, average and maximum balance of bots after this sessions.\n"
                );

            foreach (var bot in bots)
            {
                if (bot is BaseStrategyBot)
                    Console.WriteLine("Bot with <<base>> strategy:");
                else if (bot is KellyCriterionBot)
                    Console.WriteLine("Bot with strategy based on <<Kelly criterion>>:");
                else if (bot is ThorpSystemBot)
                    Console.WriteLine("Bot with strategy based on <<Thorp system>>:");

                GamesLaunch(bot);
            }
        }

        private static void GamesLaunch(IBot bot)
        {
            int min = int.MaxValue;
            int avg = 0;
            int max = 0;

            for (int i = 0; i < 1000; i++)
            {
                Casino.Game(bot, 40, new Shoes());
                max = max > bot.Balance ? max : bot.Balance;
                min = min < bot.Balance ? min : bot.Balance;
                avg += bot.Balance;
                bot.Balance = 1000;
            }

            Console.WriteLine("Minimum balance = " + min);
            Console.WriteLine("Average balance = " + avg / 1000);
            Console.WriteLine("Maximum balance = " + max + "\n");
        }
    }
}