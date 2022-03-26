namespace Roulette;

public abstract class APlayer
{
    public int AmountOfMoney { get; protected internal set; }
    protected int minBetAmount;
    protected int maxBetAmount;

    public APlayer(int sum, Casino casino)
    {
        AmountOfMoney = sum;
        minBetAmount = casino.minBetAmount;
        maxBetAmount = casino.maxBetAmount;
    }

    public virtual void GiveNewRules(Casino casino)
    {
        minBetAmount = casino.minBetAmount;
        maxBetAmount = casino.maxBetAmount;
    }

    protected internal virtual bool WantsToPlay()
    {
        return AmountOfMoney >= minBetAmount;
    }

    protected internal abstract Bet MakeBet();

    protected internal abstract void GiveResult(bool won);
}
