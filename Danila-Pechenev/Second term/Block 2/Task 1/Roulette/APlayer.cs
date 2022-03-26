namespace Roulette;

public abstract class APlayer
{
    internal int amountOfMoney;
    protected int minBetAmount;
    protected int maxBetAmount;

    public APlayer(int sum, Casino casino)
    {
        amountOfMoney = sum;
        minBetAmount = casino.minBetAmount;
        maxBetAmount = casino.maxBetAmount;
    }

    internal void GiveNewRules(Casino casino)
    {
        minBetAmount = casino.minBetAmount;
        maxBetAmount = casino.maxBetAmount;
    }

    internal abstract Bet MakeBet();

    internal abstract void GiveResult(bool won);
}
