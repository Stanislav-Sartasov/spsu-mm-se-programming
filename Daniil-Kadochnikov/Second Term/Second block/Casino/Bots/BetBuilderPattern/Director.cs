using Roulette.Bets;

namespace Bots.BetBuilderPattern
{
	internal class BetDirector
	{
		private IBetBuilder betBuilder;

		internal BetDirector(IBetBuilder builder)
		{
			betBuilder = builder;
		}

		internal void Construct(int player, int money)
		{
			betBuilder.Player = player;
			betBuilder.Money = money;
		}

		internal Bet GetBet()
		{
			return betBuilder.CreateBet();
		}
	}
}
