namespace Roulette;

public abstract class APlayer
{
    public int AmountOfMoney { get; protected internal set; }

    protected int MinBetAmount { get; set; }

    protected int MaxBetAmount { get; set; }

    public APlayer(int sum, Casino casino)
    {
        AmountOfMoney = sum;
        MinBetAmount = casino.MinBetAmount;
        MaxBetAmount = casino.MaxBetAmount;
    }

    public virtual void GiveNewRules(Casino casino)
    {
        MinBetAmount = casino.MinBetAmount;
        MaxBetAmount = casino.MaxBetAmount;
    }

    protected internal virtual bool WantsToPlay()
    {
        return AmountOfMoney >= MinBetAmount;
    }

    protected internal abstract Bet MakeBet();

    protected internal abstract void GiveResult(bool won);
}
