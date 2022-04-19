using System;
using System.Collections.Generic;
using Roulette.Common.Bet;
using Roulette.Common.GamePlay;

namespace Roulette.Bot
{
	public class AllInOneWinBot : Bot
	{
		private readonly Random _random;

		public AllInOneWinBot(int startingMoney) : base("All In, One Win Bot", startingMoney)
		{
			_random = new Random();
			Description =
				"This bot goes all in. \nIt makes a random bet and gives all money to it. \nBut it stops doing bets if it won.";
		}

		public override List<Bet> MakeBets()
		{
			var bets = new List<Bet>();

			if (Money != InitialMoney)
				IsPlaying = false;

			if (++BetsPlayed >= MaxTurns)
				IsPlaying = false;

			if (!IsPlaying)
				return bets;

			var betType = _random.Next() % 4;

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