using System;
using System.Collections.Generic;
using Roulette.Common.Bet;
using Roulette.Common.GamePlay;

namespace Roulette.Bot
{
	public class RandomBot : Bot
	{
		private readonly Random _random;

		public RandomBot(int money) : base("Random Bot", money)
		{
			_random = new Random();
			Description = "This bot is mad. \nIt does random bets win random amount of money. \nHas 10% chance to stop";
		}

		public override List<Bet> MakeBets()
		{
			var bets = new List<Bet>();
			var betType = _random.Next() % 4;
			var money = _random.Next(Money);

			if (++BetsPlayed >= MaxTurns)
				IsPlaying = false;

			if (!IsPlaying)
				return bets;

			IsPlaying = _random.Next(10) == 0;


			switch (betType)
			{
				case 0:
					bets.Add(new ColorBet(money, this, (Color) (_random.Next() % 3)));
					break;
				case 1:
					bets.Add(new DozenBet(money, this, _random.Next() % 3));
					break;
				case 2:
					bets.Add(new NumberBet(money, this, _random.Next() % 36 + 1));
					break;
				case 3:
					bets.Add(new ParityBet(money, this, (Parity) (_random.Next() % 2)));
					break;
			}

			return bets;
		}
	}
}