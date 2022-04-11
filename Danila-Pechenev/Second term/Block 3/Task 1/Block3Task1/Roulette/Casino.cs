namespace Roulette;

public class Casino
{
    private readonly RouletteWheel wheel;

    public int MinBetAmount { get; init; }

    public int MaxBetAmount { get; init; }

    public Casino(int minBet, int maxBet)
    {
        MinBetAmount = minBet;
        MaxBetAmount = maxBet;
        wheel = new RouletteWheel();
    }

    public bool PlayWith(APlayer player)
    {
        if (!player.WantsToPlay())
        {
            return false;
        }

        if (player.AmountOfMoney < MinBetAmount)
        {
            return false;
        }

        int lastAmountOfMoney = player.AmountOfMoney;

        Bet bet = player.MakeBet();
        if (!ValidateBet(bet, lastAmountOfMoney))
        {
            return false;
        }

        int result = wheel.ThrowBall();

        if (result == 0)
        {
            player.AmountOfMoney -= bet.Sum;
        }
        else
        {
            switch (bet.Type)
            {
                case BetType.Number:
                    player.AmountOfMoney += (bet.Number == result ? 35 : -1) * bet.Sum;
                    break;
                case BetType.Dozen:
                    player.AmountOfMoney += (bet.Number == (int)wheel.GetDozen(result) ? 2 : -1) * bet.Sum;
                    break;
                case BetType.Colour:
                    player.AmountOfMoney += (bet.Number == (int)wheel.GetColour(result) ? 1 : -1) * bet.Sum;
                    break;
                case BetType.Parity:
                    player.AmountOfMoney += (bet.Number == (int)wheel.GetParity(result) ? 1 : -1) * bet.Sum;
                    break;
            }
        }

        player.GiveResult(player.AmountOfMoney >= lastAmountOfMoney);
        return true;
    }

    private bool ValidateBet(Bet bet, int playerAmountOfMoney)
    {
        if (bet.Sum > playerAmountOfMoney ||
            bet.Sum > MaxBetAmount ||
            bet.Sum < MinBetAmount ||
            bet.Number < 0 ||
            (bet.Type == BetType.Number && bet.Number == 0) ||
            (bet.Type == BetType.Number && (bet.Number > 36 || bet.Number == 0)) ||
            (bet.Type == BetType.Dozen && bet.Number > 2) ||
            ((bet.Type == BetType.Colour || bet.Type == BetType.Parity) && bet.Number > 1))
        {
            return false;
        }

        return true;
    }
}
