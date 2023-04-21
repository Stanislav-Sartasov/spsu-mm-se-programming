using System;
using System.Collections.Generic;

namespace RouletteLib
{
	public class SingleBet : BetEssence
	{
		public SingleBet(int bet)
		{
			Coefficient = 35;

			if (bet >= 0 && bet <= 36)
			{
				VictoryList = new List<int>() { bet };
			}
			else
			{
				throw new Exception("Single bet can only be on integer number from [0,36]");
			}
		}
	}
}