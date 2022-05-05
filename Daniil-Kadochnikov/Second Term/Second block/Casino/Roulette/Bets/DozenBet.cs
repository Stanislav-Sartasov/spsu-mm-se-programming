using Roulette.Bets.PossibleBets;
using Roulette.Cells;

namespace Roulette.Bets
{
	public class DozenBet : Bet
	{
		public readonly PossibleDozen BetCell;

		public DozenBet(int player, int money, PossibleDozen dozen) : base(player, money) => BetCell = dozen;

		public override int CheckBet(Cell winCell)
		{
			if ((DozenEnum)BetCell == winCell.Dozen)
				return 3;
			else
				return 0;
		}
	}
}
