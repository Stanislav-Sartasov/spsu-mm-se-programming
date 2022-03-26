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
        if (!player.WantsToPlay()) return false;
        if (player.AmountOfMoney < minBetAmount) return false;

        int lastAmountOfMoney = player.AmountOfMoney;

        Bet bet = player.MakeBet();
        if (!validateBet(bet, lastAmountOfMoney)) return false;

        int result = wheel.ThrowBall();

        if (result == 0)
        {
            player.AmountOfMoney -= bet.sum;
        }
        else
        {
            switch (bet.type)
            {
                case BetType.Number:
                    player.AmountOfMoney += (bet.number == result ? 35 : -1) * bet.sum;
                    break;
                case BetType.Dozen:
                    player.AmountOfMoney += (bet.number == (int)wheel.GetDozen(result) ? 2 : -1) * bet.sum;
                    break;
                case BetType.Colour:
                    player.AmountOfMoney += (bet.number == (int)wheel.GetColour(result) ? 1 : -1) * bet.sum;
                    break;
                case BetType.Parity:
                    player.AmountOfMoney += (bet.number == (int)wheel.GetParity(result) ? 1 : -1) * bet.sum;
                    break;
            }
        }

        player.GiveResult(player.AmountOfMoney >= lastAmountOfMoney);
        return true;
    }

    private bool validateBet(Bet bet, int playerAmountOfMoney)
    {
        if (bet.sum > playerAmountOfMoney || 
            bet.sum > maxBetAmount ||
            bet.sum < minBetAmount) return false;
        if (bet.number < 0) return false;
        if (bet.type == BetType.Number && (bet.number > 36 || bet.number == 0)) return false;
        if (bet.type == BetType.Dozen && bet.number > 2) return false;
        if ((bet.type == BetType.Colour || bet.type == BetType.Parity) && bet.number > 1) return false;
  
        return true;
    }
}
