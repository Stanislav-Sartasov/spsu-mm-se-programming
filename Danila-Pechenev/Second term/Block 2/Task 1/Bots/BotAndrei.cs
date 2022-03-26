namespace Bots;
using System.Security.Cryptography;
using Roulette;

public class BotAndrei : APlayer
{
    private int currentNumber;

    public BotAndrei(int sum, Casino casino)
        : base(sum, casino)
    {
        currentNumber = 0;
    }

    public override void GiveNewRules(Casino casino)
    {
        MinBetAmount = casino.MinBetAmount;
        MaxBetAmount = casino.MaxBetAmount;

        currentNumber = 0;
    }

    protected override Bet MakeBet()
    {
        if (currentNumber == 0)
        {
            return new Bet(BetType.Number, RandomNumberGenerator.GetInt32(36) + 1, MinBetAmount);
        }

        return new Bet(BetType.Number, currentNumber, MinBetAmount);
    }

    protected override void GiveResult(bool won)
    {
        if (won)
        {
            currentNumber = RandomNumberGenerator.GetInt32(36) + 1;
        }
    }
}
