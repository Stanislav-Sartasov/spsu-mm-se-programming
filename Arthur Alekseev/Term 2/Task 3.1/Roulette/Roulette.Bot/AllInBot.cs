using System;
using System.Collections.Generic;
using Roulette.Common.Bet;
using Roulette.Common.GamePlay;

namespace Roulette.Bot
{
	public class AllInBot : Bot
	{
		private readonly Random _random;

		public AllInBot(int startingMoney) : base("All In Bot", startingMoney)
		{
			_random = new Random();
			Description =
				"This bot goes all in, \nit makes a random bet and gives all money to it. \n Has a big chance (25%) of stop betting.";
		}

		public override List<Bet> MakeBets()
		{
			var bets = new List<Bet>();
			var betType = _random.Next() % 4;

			if (!IsPlaying)
				return bets;

			IsPlaying = _random.Next(4) == 0;

			if (++BetsPlayed >= MaxTurns)
				IsPlaying = false;

			switch (betType)
			{
				case 0:
					bets.Add(new ColorBet(Money, this, (Color) (_random.Next() % 3)));
					break;
				case 1:
					bets.Add(new DozenBet(Money, this, _random.Next() % 3));
					break;
				case 2:
					bets.Add(new NumberBet(Money, this, _random.Next() % 36 + 1));
					break;
				case 3:
					bets.Add(new ParityBet(Money, this, (Parity) (_random.Next() % 2)));
					break;
			}

			return bets;
		}
	}
}