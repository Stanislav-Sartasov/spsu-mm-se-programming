using System;
using Blackjack;
using BlackjackBots;

namespace PrimitiveBotsTask
{
    public class Program
    {
        static void Main()
        {
            Game game = new Game(1000);
            IBot riskyBot = new RiskyStrategyBot();
            IBot ordinaryBot = new BaseStrategyBot();
            IBot conservativeBot = new ConservativeStrategyBot();
            float[] averageBalance = new float[3];

            for (int i = 0; i < 1000; i++)                                  // 40 bets with every bet = 50$ and start balance as 1000$
            {
                game.Play(40, 50, riskyBot);
                averageBalance[0] += game.GetPlayerBalance() / 1000F;

                game.Play(40, 50, ordinaryBot);
                averageBalance[1] += game.GetPlayerBalance() / 1000F;

                game.Play(40, 50, conservativeBot);
                averageBalance[2] += game.GetPlayerBalance() / 1000F;
            }

            Console.WriteLine($"Star balance was {game.StartBalance}$");
            Console.WriteLine($"Risky bot balance: {averageBalance[0]}$");
            Console.WriteLine($"Ordinary bot balance: {averageBalance[1]}$");
            Console.WriteLine($"Conservative bot balance: {averageBalance[2]}$");
        }
    }
}