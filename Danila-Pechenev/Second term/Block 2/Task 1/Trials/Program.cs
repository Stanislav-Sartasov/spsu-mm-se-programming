using Roulette;
using Bots;
namespace Trials;

public class Program
{
    public static void Main(string[] args)
    {
        int countTrials = 10000;
        int countBets = 40;

        Casino casino = new Casino(100, 20000);

        BotOleg botOleg;
        BotIgor botIgor;

        int tmpSum = 0;
        for (int trial = 0; trial < countTrials; trial++)
        {
            botOleg = new BotOleg(15000, casino);
            for (int i = 0; i < countBets; i++)
            {
                casino.PlayWith(botOleg);
            }
            tmpSum += botOleg.AmountOfMoney;
        }
        Console.WriteLine($"Олег: {(double)tmpSum / countTrials}");


        tmpSum = 0;
        for (int trial = 0; trial < countTrials; trial++)
        {
            botIgor = new BotIgor(15000, casino);
            for (int i = 0; i < countBets; i++)
            {
                casino.PlayWith(botIgor);
            }
            tmpSum += botIgor.AmountOfMoney;
        }
        Console.WriteLine($"Игорь: {(double)tmpSum / countTrials}");
    }
}