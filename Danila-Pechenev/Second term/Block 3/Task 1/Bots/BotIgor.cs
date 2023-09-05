namespace Bots;
using Roulette;

public class BotIgor : APlayer
{
    private List<int> strategyList;

    public BotIgor(int sum, Casino casino)
        : base(sum, casino)
    {
        strategyList = CreateNewStrategyList(MinBetAmount);
    }

    public override void GiveNewRules(Casino casino)
    {
        MinBetAmount = casino.MinBetAmount;
        MaxBetAmount = casino.MaxBetAmount;

        strategyList = CreateNewStrategyList(MinBetAmount);
    }

    protected override Bet MakeBet()
    {
        return new Bet(
            BetType.Parity,
            (int)Parity.Odd,
            Math.Min(Math.Max(MinBetAmount, strategyList[0] + strategyList[^1]), MaxBetAmount));
    }

    protected override void GiveResult(bool won)
    {
        if (won)
        {
            strategyList.RemoveAt(strategyList.Count - 1);
            strategyList.RemoveAt(0);
            if (strategyList.Count < 2)
            {
                strategyList = CreateNewStrategyList(MinBetAmount);
            }
        }
        else
        {
            if (strategyList[0] + strategyList[^1] <= AmountOfMoney)
            {
                strategyList.Add(strategyList[0] + strategyList[^1]);
            }
            else
            {
                strategyList = CreateNewStrategyList(MinBetAmount);
            }
        }

        if (strategyList[0] + strategyList[^1] > AmountOfMoney)
        {
            strategyList = CreateNewStrategyList(MinBetAmount);
        }
    }

    private List<int> CreateNewStrategyList(int minBetAmount)
    {
        int size = 7;

        strategyList = new List<int>();
        for (int i = 0; i < size; i++)
        {
            strategyList.Add((minBetAmount / 2) + (i - 3));
        }

        return strategyList;
    }
}
