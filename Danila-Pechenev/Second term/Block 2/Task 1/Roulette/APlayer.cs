namespace Roulette;

public abstract class APlayer
{
    public int amountOfMoney;

    public APlayer(int sum)
    {
        amountOfMoney = sum;
    }

    public abstract Bet MakeBet();
}
