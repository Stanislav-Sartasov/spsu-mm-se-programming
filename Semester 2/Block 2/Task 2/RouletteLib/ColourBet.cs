using System;
using System.Collections.Generic;

namespace RouletteLib
{
	public class ColourBet : BetEssence
	{
		public ColourBet(ColourBetsEnum bet)
		{
			Coefficient = 1;

			switch (bet)
			{
				case ColourBetsEnum.Red:
					VictoryList = new List<int>() { 1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36 };
					break;
				case ColourBetsEnum.Black:
					VictoryList = new List<int>() { 2, 4, 6, 8, 10, 11, 13, 15, 17, 20, 22, 24, 26, 28, 29, 31, 33, 35 };
					break;
				default:
					throw new Exception("Wrong colour.");
			}
		}
	}
}
