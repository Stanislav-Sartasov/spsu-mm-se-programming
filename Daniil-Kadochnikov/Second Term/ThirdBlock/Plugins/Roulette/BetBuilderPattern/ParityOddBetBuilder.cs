using Roulette.Bets;
using Roulette.Cells;

namespace Roulette.BetBuilderPattern
{
	internal class ParityOddBetBuilder : IBetBuilder
	{
		public int Player { get; set; }
		public int Money { get; set; }

		public Bet CreateBet()
		{
			return new ParityBet(Player, Money, ParityEnum.Odd);
		}
	}
}