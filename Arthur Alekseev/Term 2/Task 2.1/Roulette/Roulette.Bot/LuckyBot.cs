using System.Collections.Generic;
using Roulette.Common.Bet;

namespace Roulette.Bot
{
	public class LuckyBot : Bot
	{
		public LuckyBot(int startingMoney) : base("Lucky Bot", startingMoney)
		{
			Description =
				"This bot has it's lucky number 13, \nso it bets 1/13 of it's money on this number. \nPlays 13 times and stops.";
		}

		public override List<Bet> MakeBets()
		{
			var bets = new List<Bet>();

			if (++BetsPlayed > 13)
				IsPlaying = false;

			if (!IsPlaying)
				return bets;

			bets.Add(new NumberBet(Money / 13, this, 13));

			return bets;
		}
	}
}