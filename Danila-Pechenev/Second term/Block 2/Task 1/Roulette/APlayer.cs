namespace Roulette;

public abstract class APlayer
{
    protected internal int amountOfMoney;
    protected int minBetAmount;
    protected int maxBetAmount;

    public APlayer(int sum, Casino casino)
    {
        amountOfMoney = sum;
        minBetAmount = casino.minBetAmount;
        maxBetAmount = casino.maxBetAmount;
    }

    protected internal virtual void GiveNewRules(Casino casino)
    {
        minBetAmount = casino.minBetAmount;
        maxBetAmount = casino.maxBetAmount;
    }

    protected internal virtual bool WantsToPlay()
    {
        return amountOfMoney >= minBetAmount;
    }

    protected internal abstract Bet MakeBet();

    protected internal abstract void GiveResult(bool won);
}
