namespace Trials;
using Roulette;
using Bots;

public class Program
{
    public static void Main(string[] args)
    {
        int minBet = 100;
        int maxBet = 20000;
        int countTrials = 100000;
        int countBets = 40;
        int startAmountOfMoney = 12000;

        Casino casino = new Casino(minBet, maxBet);

        BotOleg botOleg;
        BotIgor botIgor;
        BotAndrei botAndrei;

        int sumOleg = 0;
        int sumIgor = 0;
        int sumAndrei = 0;

        for (int trial = 0; trial < countTrials; trial++)
        {
            botOleg = new BotOleg(startAmountOfMoney, casino);
            botIgor = new BotIgor(startAmountOfMoney, casino);
            botAndrei = new BotAndrei(startAmountOfMoney, casino);

            for (int i = 0; i < countBets; i++)
            {
                casino.PlayWith(botOleg);
                casino.PlayWith(botIgor);
                casino.PlayWith(botAndrei);
            }

            sumOleg += botOleg.AmountOfMoney;
            sumIgor += botIgor.AmountOfMoney;
            sumAndrei += botAndrei.AmountOfMoney;
        }

        Console.WriteLine($"Minimal bet in the casino: {minBet}.");
        Console.WriteLine($"Maximal bet in the casino: {maxBet}.");
        Console.WriteLine($"The initial amount of money for each bot: {startAmountOfMoney}");
        Console.WriteLine($"On average after {countBets} bets:");
        Console.WriteLine($"Bot Oleg has {Math.Round((double)sumOleg / countTrials)},");
        Console.WriteLine($"Bot Igor has {Math.Round((double)sumIgor / countTrials)},");
        Console.WriteLine($"Bor Andrei has {Math.Round((double)sumAndrei / countTrials)}.");
    }
}
