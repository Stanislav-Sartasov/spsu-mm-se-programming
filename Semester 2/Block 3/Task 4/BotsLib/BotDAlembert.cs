using System;
using RouletteLib;

namespace BotsLib
{
	public class BotDAlembert : Bot
	{
		private const int BaseBetAmount = 10;

		public BotDAlembert(BetEssence bet, int startCash) : base(bet, startCash)
		{
		}

		public override int Play(int numberOfBets)
		{
			Roulette roulette = new Roulette();
			int cash = StartCash;
			int betAmount = BaseBetAmount;

			for (int i = 0; i < numberOfBets; i++)
			{
				if (cash < betAmount)
					return cash;

				if (roulette.Spin(Bet))
				{
					cash += Bet.Coefficient * betAmount;
					if (betAmount != BaseBetAmount)
						betAmount -= BaseBetAmount;
				}
				else
				{
					cash -= betAmount;
					betAmount += BaseBetAmount;
				}
			}

			return cash;
		}
	}
}
