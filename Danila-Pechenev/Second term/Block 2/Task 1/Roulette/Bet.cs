namespace Roulette;

public class Bet
{
    public BetType Type { get; init; }

    public int Number { get; init; }

    public int Sum { get; init; }

    public Bet(BetType betType, int betNumber, int betSum)
    {
        Type = betType;
        Number = betNumber;
        Sum = betSum;
    }
}
