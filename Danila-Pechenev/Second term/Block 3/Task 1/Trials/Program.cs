namespace Trials;
using Roulette;
using PluginLoader;

public class Program
{
    public static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("The program accepts only one command line argument - path to the folder.");
            return;
        }

        const int countBets = 40;
        const int countTrials = 10000;

        const int minBet = 200;
        const int maxBet = 20000;

        Casino casino = new Casino(minBet, maxBet);
        const int startAmountOfMoney = 12000;
        object[] parameters = { startAmountOfMoney, casino };

        Console.WriteLine($"Minimal bet in the casino: {minBet}.");
        Console.WriteLine($"Maximal bet in the casino: {maxBet}.");
        Console.WriteLine($"The initial amount of money for each bot: {startAmountOfMoney}.");
        Console.WriteLine();
        Console.WriteLine($"On average after {countBets} bets:");

        var bots = BotLoader.LoadBots(args[0], parameters);

        if (bots != null)
        {
            int[] sums = new int[bots.Length];

            for (int trial = 0; trial < countTrials; trial++)
            {
                bots = BotLoader.LoadBots(args[0], parameters);
                for (int botNumber = 0; botNumber < bots.Length; botNumber++)
                {
                    for (int i = 0; i < countBets; i++)
                    {
                        casino.PlayWith(bots[botNumber]);
                    }

                    sums[botNumber] += bots[botNumber].AmountOfMoney;
                }
            }

            for (int botNumber = 0; botNumber < bots.Length; botNumber++)
            {
                Console.WriteLine($"{ bots[botNumber].GetType().Name } has { Math.Round((double)sums[botNumber] / countTrials)}");
            }
        }
    }
}
