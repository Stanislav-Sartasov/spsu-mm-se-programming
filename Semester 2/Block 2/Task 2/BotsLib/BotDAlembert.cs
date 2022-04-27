using System;
using RouletteLib;

namespace BotsLib
{
	public class BotDAlembert : Bot
	{
		private readonly int BaseBetAmount;

		public BotDAlembert(int baseBetAmount, string betEssence, int startCash) : base(betEssence, startCash)
		{
			BaseBetAmount = baseBetAmount;
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

				if (roulette.Spin(BetEssence))
				{
					cash += betAmount;
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
