using Roulette.Cells;

namespace Roulette.Bets
{
	public class NumberBet : Bet
	{
		public readonly int BetCell;

		public NumberBet(int player, int money, int number) : base(player, money)
		{
			if (0 <= number && number <= 36)
				BetCell = number;
			else
				throw new System.ArgumentOutOfRangeException("The number bet is out of the range from 0 to 36.");
		}

		public override int CheckBet(Cell winCell)
		{
			if (BetCell == winCell.Number)
				return 36;
			else
				return 0;
		}
	}
}