using Roulette.Bets;
using Roulette.Cells;

namespace Roulette.BetBuilderPattern
{
	internal class ColourBlackBetBuilder : IBetBuilder
	{ 
		public int Player { get; set; }
		public int Money { get; set; }

		public Bet CreateBet()
		{
			return new ColourBet(Player, Money, ColourEnum.Black);
		}
	}
}