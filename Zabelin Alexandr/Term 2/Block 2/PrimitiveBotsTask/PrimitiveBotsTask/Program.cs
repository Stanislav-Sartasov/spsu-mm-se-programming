using System;
using Blackjack;
using BlackjackBots;

namespace PrimitiveBotsTask
{
    class Program
    {
        static void Main()
        {
            Game game = new Game();
            APlayer riskyBot = new RiskyStrategyBot(1000);
            APlayer baseBot = new BaseStrategyBot(1000);
            APlayer conservativeBot = new ConservativeStrategyBot(1000);
            float[] averageBalance = new float[] {0, 0, 0};

            for (int i = 0; i < 1000; i++)
            {
                game.Play(riskyBot, 50, 40);
                game.Play(baseBot, 50, 40);
                game.Play(conservativeBot, 50, 40);

                averageBalance[0] += riskyBot.Balance / 1000F;
                averageBalance[1] += baseBot.Balance / 1000F;
                averageBalance[2] += conservativeBot.Balance / 1000F;

                riskyBot = new RiskyStrategyBot(1000);
                baseBot = new BaseStrategyBot(1000);
                conservativeBot = new ConservativeStrategyBot(1000);
            }

            Console.WriteLine("Start balace was 1000");
            Console.WriteLine($"riskyBot balance: {averageBalance[0]}");
            Console.WriteLine($"baseBot balance: {averageBalance[1]}");
            Console.WriteLine($"conservativeBot balance: {averageBalance[2]}");
        }
    }
}