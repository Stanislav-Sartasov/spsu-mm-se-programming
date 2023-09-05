﻿using System;
using RouletteLib;

namespace BotsLib
{
	public abstract class Bot
	{
		protected int StartCash;
		protected BetEssence Bet;

		public Bot(BetEssence bet, int startCash)
		{
			StartCash = startCash;
			Bet = bet;
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
