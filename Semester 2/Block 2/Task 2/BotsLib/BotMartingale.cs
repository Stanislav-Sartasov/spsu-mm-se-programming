using System;
using RouletteLib;

namespace BotsLib
{
	public class BotMartingale : Bot
	{
		private readonly int StartBetAmount;

		public BotMartingale(int startBetAmount, string betEssence, int startCash) : base (betEssence, startCash)
		{
			StartBetAmount = startBetAmount;
		}

		public override int Play(int numberOfBets)
		{
			Roulette roulette = new Roulette();
			int cash = StartCash;
			int betAmount = StartBetAmount;

			for (int i = 0; i < numberOfBets; i++)
			{
				if (cash < betAmount)
					return cash;

				if (roulette.Spin(BetEssence))
				{
					cash += betAmount;
					betAmount = StartBetAmount;
				}
				else
				{
					cash -= betAmount;
					betAmount *= 2;
				}
			}

			return cash;
		}
	}
}
