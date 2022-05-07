using Roulette.Bets;

namespace Bots.BetBuilderPattern
{
	internal interface IBetBuilder
	{
		int Player { get; set; }
		int Money { get; set; }

		Bet CreateBet();
	}
}
