using Roulette.Bets.PossibleBets;
using Roulette.Cells;

namespace Roulette.Bets
{
	public class ParityBet : Bet
	{
		public readonly PossibleParity BetCell;

		public ParityBet(int player, int money, PossibleParity parity) : base(player, money) => BetCell = parity;

		public override int CheckBet(Cell winCell)
		{
			if ((ParityEnum)BetCell == winCell.Parity)
				return 2;
			else
				return 0;
		}
	}
}