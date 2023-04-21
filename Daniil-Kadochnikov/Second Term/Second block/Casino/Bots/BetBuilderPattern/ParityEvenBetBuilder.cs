using Roulette.Bets;
using Roulette.Cells;

namespace Bots.BetBuilderPattern
{
	internal class ParityEvenBetBuilder : IBetBuilder
	{
		public int Player { get; set; }
		public int Money { get; set; }

		public Bet CreateBet()
		{
			return new ParityBet(Player, Money, ParityEnum.Even);
		}
	}
}