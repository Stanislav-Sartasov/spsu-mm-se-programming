using Roulette.Cells;

namespace Roulette.Bets
{
	public class ColourBet : Bet
	{
		public readonly ColourEnum BetCell;

		public ColourBet(int player, int money, ColourEnum colour) : base(player, money) => BetCell = colour;

		public override int CheckBet(Cell winCell)
		{
			if (BetCell == winCell.Colour)
				return 2;
			else
				return 0;
		}
	}
}