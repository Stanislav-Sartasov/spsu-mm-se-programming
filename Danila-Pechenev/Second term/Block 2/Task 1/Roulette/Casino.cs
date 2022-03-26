namespace Roulette;

public class Casino
{
    public readonly int minBetAmount;
    public readonly int maxBetAmount;
    private readonly RouletteWheel wheel;

    public Casino(int minBet, int maxBet)
    {
        minBetAmount = minBet;
        maxBetAmount = maxBet;
        wheel = new RouletteWheel();
    }

    public bool PlayWith(APlayer player)
    {
        if (player.amountOfMoney < minBetAmount) return false;

        int lastAmountOfMoney = player.amountOfMoney;
        Bet bet = player.MakeBet();
        int result = wheel.ThrowBall();
        if (!validateBet(bet, lastAmountOfMoney)) return false;

        switch (bet.type)
        {
            case BetType.Number:
                player.amountOfMoney += (bet.number == result ? 35 : -1) * bet.sum;
                break;
            case BetType.Dozen:
                player.amountOfMoney += (bet.number == (int)wheel.GetDozen(result) ? 2 : -1) * bet.sum;
                break;
            case BetType.Colour:
                player.amountOfMoney += (bet.number == (int)wheel.GetColour(result) ? 1 : -1) * bet.sum;
                break;
            case BetType.Parity:
                player.amountOfMoney += (bet.number == (int)wheel.GetParity(result) ? 1 : -1) * bet.sum;
                break;
        }

        player.GiveResult(player.amountOfMoney >= lastAmountOfMoney);
        return true;
    }

    private bool validateBet(Bet bet, int playerAmountOfMoney)
    {
        if (bet.sum >= playerAmountOfMoney || 
            bet.sum > maxBetAmount ||
            bet.sum < minBetAmount) return false;
        if (bet.number <= 0) return false;
        if (bet.type == BetType.Number && bet.number > 36) return false;
        if (bet.type == BetType.Dozen && bet.number > 2) return false;
        if ((bet.type == BetType.Colour || bet.type == BetType.Parity) && bet.number > 1) return false;
        return true;
    }
}
