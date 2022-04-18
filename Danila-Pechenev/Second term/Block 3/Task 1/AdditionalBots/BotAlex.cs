namespace AdditionalBots;
using Roulette;

public class BotAlex : APlayer
{
    public BotAlex(int sum, Casino casino)
        : base(sum, casino)
    {
    }

    protected override Bet MakeBet()
    {
        return new Bet(BetType.Parity, 0, MinBetAmount);
    }

    protected override void GiveResult(bool won)
    {
        return;
    }
}
