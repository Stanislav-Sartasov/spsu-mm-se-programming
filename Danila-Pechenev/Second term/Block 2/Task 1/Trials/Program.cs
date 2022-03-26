using Roulette;
using Bots;
namespace Trials;

public class Program
{
    public static void Main(string[] args)
    {
        int countTrials = 100000;
        int countBets = 40;
        int startAmountOfMoney = 15000;

        Casino casino = new Casino(100, 20000);

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
        Console.WriteLine($"Олег: {(double)sumOleg / countTrials}");
        Console.WriteLine($"Игорь: {(double)sumIgor / countTrials}");
        Console.WriteLine($"Андрей: {(double)sumAndrei / countTrials}");
    }
}