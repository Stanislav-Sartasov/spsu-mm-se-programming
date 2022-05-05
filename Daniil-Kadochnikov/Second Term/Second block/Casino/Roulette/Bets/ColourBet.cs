using Roulette.Bets.PossibleBets;
using Roulette.Cells;

namespace Roulette.Bets
{
	public class ColourBet : Bet
	{
		public readonly PossibleColour BetCell;

		public ColourBet(int player, int money, PossibleColour colour) : base(player, money) => BetCell = colour;

		public override int CheckBet(Cell winCell)
		{
			if ((ColourEnum)BetCell == winCell.Colour)
				return 2;
			else
				return 0;
		}
	}
}