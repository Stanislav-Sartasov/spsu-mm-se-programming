using Roulette.Bets;

namespace Roulette.BetBuilderPattern
{
	public class BetDirector
	{
		private IBetBuilder betBuilder;

		internal BetDirector(IBetBuilder builder)
		{
			betBuilder = builder;
		}

		public void Construct(int player, int money)
		{
			betBuilder.Player = player;
			betBuilder.Money = money;
		}

		public Bet GetBet()
		{
			return betBuilder.CreateBet();
		}
	}
}
