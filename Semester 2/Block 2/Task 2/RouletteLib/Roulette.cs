using System;
using System.Collections.Generic;

namespace RouletteLib
{
	public class Roulette
	{
		public bool Spin (BetEssence bet)
		{
			Random spinResult = new Random();
			int number = spinResult.Next(37);

			if (bet.VictoryList.Contains(number))
			{
				return true;
			}

			return false;
		}
	}
}
