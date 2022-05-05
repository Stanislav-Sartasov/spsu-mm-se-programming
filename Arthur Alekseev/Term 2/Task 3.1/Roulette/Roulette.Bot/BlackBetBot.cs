using System.Collections.Generic;
using Roulette.Common.Bet;
using Roulette.Common.GamePlay;

namespace Roulette.Bot
{
	public class BlackBetBot : Bot
	{
		public BlackBetBot(int startingMoney) : base("Black Bet Bot", startingMoney)
		{
			Description =
				"This bot loves color Black, \nso it bets half of it's money on color Black. \nPlays 3 bets and stops";
		}

		public override List<Bet> MakeBets()
		{
			var bets = new List<Bet>();

			if (++BetsPlayed > 3)
				IsPlaying = false;

			if (!IsPlaying)
				return bets;


			bets.Add(new ColorBet(Money / 2, this, Color.Black));

			return bets;
		}
	}
}