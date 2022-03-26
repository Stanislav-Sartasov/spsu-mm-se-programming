namespace Roulette;

public struct Bet
{
    public readonly BetType type;
    public readonly int number;
    public readonly int sum;

    public Bet(BetType betType, int betNumber, int betSum)
    {
        type = betType;
        number = betNumber;
        sum = betSum;
    }
}
