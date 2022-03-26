namespace Bots;
using Roulette;

public class BotOleg : APlayer
{
    private int currentBetSum;
    private Colour currentColour;
    private bool firstBetInSequence;

    public BotOleg(int sum, Casino casino)
        : base(sum, casino)
    {
        currentBetSum = MinBetAmount;
        currentColour = Colour.Black;
        firstBetInSequence = true;
    }

    public override void GiveNewRules(Casino casino)
    {
        MinBetAmount = casino.MinBetAmount;
        MaxBetAmount = casino.MaxBetAmount;

        currentBetSum = MinBetAmount;
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
            currentBetSum = MinBetAmount;
        }
        else
        {
            if (firstBetInSequence)
            {
                currentColour = currentColour == Colour.Black ? Colour.Red : Colour.Black;
                firstBetInSequence = false;
            }

            currentBetSum = Math.Min(Math.Min(currentBetSum * 2, MaxBetAmount), AmountOfMoney);
        }
    }
}
