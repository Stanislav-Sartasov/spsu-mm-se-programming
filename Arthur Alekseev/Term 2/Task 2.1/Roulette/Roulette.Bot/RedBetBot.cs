using System.Collections.Generic;
using Roulette.Common.Bet;
using Roulette.Common.GamePlay;

namespace Roulette.Bot
{
	public class RedBetBot : Bot
	{
		public RedBetBot(int startingMoney) : base("Red Bet Bot", startingMoney)
		{
			Description =
				"This bot loves color Red, \nso it bets half of it's money on color Red. \nPlays 3 bets and stops";
		}

		public override List<Bet> MakeBets()
		{
			var bets = new List<Bet>();

			if (++BetsPlayed > 3)
				IsPlaying = false;

			if (!IsPlaying)
				return bets;

			bets.Add(new ColorBet(Money / 2, this, Color.Red));

			return bets;
		}
	}
}