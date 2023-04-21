using Roulette.Bets;
using Roulette.Cells;

namespace Bots.BetBuilderPattern
{
	internal class ColourRedBetBuilder : IBetBuilder
	{
		public int Player { get; set; }
		public int Money { get; set; }

		public Bet CreateBet()
		{
			return new ColourBet(Player, Money, ColourEnum.Red);
		}
	}
}