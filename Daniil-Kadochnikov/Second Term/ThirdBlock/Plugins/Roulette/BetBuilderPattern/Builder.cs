using Roulette.Bets;

namespace Roulette.BetBuilderPattern
{
	internal interface IBetBuilder
	{
		int Player { get; set; }
		int Money { get; set; }

		Bet CreateBet();
	}
}
