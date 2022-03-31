using System;
using System.Collections.Generic;
using Roulette.Common.Bet;
using Roulette.Common.GamePlay;

namespace Roulette.Bot
{
	public class CautiousBot : Bot
	{
		private readonly Random _random;

		public CautiousBot(int startingMoney) : base("Cautious Bot", startingMoney)
		{
			_random = new Random();
			Description =
				"This bot plays safely. \nIt bets 20% of it's money and only in 1/2 or 1/3 bets. \nIf there is less than 67% of it's money left, it will no longer make any bets.";
		}

		public override List<Bet> MakeBets()
		{
			var bets = new List<Bet>();

			if (Money < InitialMoney / 3 * 2)
				IsPlaying = false;

			if (++BetsPlayed >= MaxTurns)
				IsPlaying = false;

			if (!IsPlaying)
				return bets;

			var betType = _random.Next() % 3;

			switch (betType)
			{
				case 0:
					bets.Add(new ColorBet(Money / 5, this, (Color) (_random.Next() % 3)));
					break;
				case 1:
					bets.Add(new DozenBet(Money / 5, this, _random.Next() % 3));
					break;
				case 2:
					bets.Add(new ParityBet(Money / 5, this, (Parity) (_random.Next() % 2)));
					break;
			}

			return bets;
		}
	}
}