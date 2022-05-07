using Roulette.Cells;

namespace Roulette.Bets
{
	public class DozenBet : Bet
	{
		public readonly DozenEnum BetCell;

		public DozenBet(int player, int money, DozenEnum dozen) : base(player, money) => BetCell = dozen;

		public override int CheckBet(Cell winCell)
		{
			if (BetCell == winCell.Dozen)
				return 3;
			else
				return 0;
		}
	}
}
