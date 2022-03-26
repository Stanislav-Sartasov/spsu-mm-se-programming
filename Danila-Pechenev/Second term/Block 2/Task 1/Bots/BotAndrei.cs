using System.Security.Cryptography;
using Roulette;
namespace Bots;

public class BotAndrei : APlayer
{
    private int lastResult;

    public BotAndrei(int sum, Casino casino) : base(sum, casino)
    {
        lastResult = 0;
    }

    public override void GiveNewRules(Casino casino)
    {
        minBetAmount = casino.minBetAmount;
        maxBetAmount = casino.maxBetAmount;

        lastResult = 0;
    }

    protected override Bet MakeBet()
    {
        if (lastResult == 0)
        {
            return new Bet(BetType.Number, RandomNumberGenerator.GetInt32(36) + 1, minBetAmount);
        }

        return new Bet(BetType.Number, lastResult, minBetAmount);
    }

    protected override void GiveResult(bool won, int result)
    {
        lastResult = result;
    }
}
