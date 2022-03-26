using Roulette;
namespace Bots;

public class BotOleg : APlayer
{
    private int currentBetSum;
    private Colour currentColour;
    private bool firstBetInSequence;

    public BotOleg(int sum, Casino casino) : base(sum, casino)
    {
        currentBetSum = minBetAmount;
        currentColour = Colour.Black;
        firstBetInSequence = true;
    }

    public override void GiveNewRules(Casino casino)
    {
        minBetAmount = casino.minBetAmount;
        maxBetAmount = casino.maxBetAmount;

        currentBetSum = minBetAmount;
        firstBetInSequence = true;
    }

    protected override Bet MakeBet()
    {
        return new Bet(BetType.Colour, (int)currentColour, currentBetSum);
    }

    protected override void GiveResult(bool won)
    {
        if (won)
        {
            currentBetSum = minBetAmount;
        }
        else
        {
            if (firstBetInSequence)
            {
                currentColour = currentColour == Colour.Black ? Colour.Red : Colour.Black;
                firstBetInSequence = false;
            }

            currentBetSum = Math.Min(Math.Min(currentBetSum * 2, maxBetAmount), AmountOfMoney);
        }
    }
}
