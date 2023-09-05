namespace AdditionalBots;
using Roulette;

public class BotRobert : APlayer
{
    public BotRobert(int sum, Casino casino)
        : base(sum, casino)
    {
    }

    protected override Bet MakeBet()
    {
        return new Bet(BetType.Colour, 1, MinBetAmount);
    }

    protected override void GiveResult(bool won)
    {
        return;
    }
}
