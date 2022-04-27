using System;

namespace BotsLib
{
	public abstract class Bot
	{
		protected int StartCash;
		protected string BetEssence;

		public Bot(string betEssence, int startCash)
		{
			StartCash = startCash;
			BetEssence = betEssence;
		}

		public abstract int Play(int numberOfBets);

		public double CountAverageCasheLeftover(int numberOfBets, int numberOfPlays)
		{
			int sum = 0;
			for (int i = 0; i < numberOfPlays; i++)
			{
				sum += Play(numberOfBets);
			}

			return sum / numberOfPlays;
		}
	}
}
