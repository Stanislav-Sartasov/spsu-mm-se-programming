using Roulette;
namespace Bots;

public class BotIgor : APlayer
{
    List<int> strategyList;

    public BotIgor(int sum, Casino casino) : base(sum, casino)
    {
        strategyList = CreateNewStrategyList(minBetAmount);
    }

    public override void GiveNewRules(Casino casino)
    {
        minBetAmount = casino.minBetAmount;
        maxBetAmount = casino.maxBetAmount;

        strategyList = CreateNewStrategyList(minBetAmount);
    }

    protected override Bet MakeBet()
    {
        return new Bet(BetType.Parity, (int)Parity.Odd, 
            Math.Max(minBetAmount, strategyList[0] + strategyList[strategyList.Count - 1]));
    }

    protected override void GiveResult(bool won, int result)
    {
        if (won)
        {
            strategyList.RemoveAt(strategyList.Count - 1);
            strategyList.RemoveAt(0);
            if (strategyList.Count < 2)
            {
                strategyList = CreateNewStrategyList(minBetAmount);
            }
        }
        else
        {
            if (strategyList[0] + strategyList[strategyList.Count - 1] <= AmountOfMoney)
            {
                strategyList.Add(strategyList[0] + strategyList[strategyList.Count - 1]);
            }
            else
            {
                strategyList = CreateNewStrategyList(minBetAmount);
            }
        }
    }

    private List<int> CreateNewStrategyList(int minBetAmount)
    {
        int size = 7;

        strategyList = new List<int>();
        for (int i = 0; i < size; i++)
        {
            strategyList.Add(minBetAmount / 2 + (i - 3));
        }

        return strategyList;
    }
}
