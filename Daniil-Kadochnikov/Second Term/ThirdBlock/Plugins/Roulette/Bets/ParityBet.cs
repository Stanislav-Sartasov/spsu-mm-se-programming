using Roulette.Cells;

namespace Roulette.Bets
{
	public class ParityBet : Bet
	{
		public readonly ParityEnum BetCell;

		public ParityBet(int player, int money, ParityEnum parity) : base(player, money) => BetCell = parity;

		public override int CheckBet(Cell winCell)
		{
			if (BetCell == winCell.Parity)
				return 2;
			else
				return 0;
		}
	}
}