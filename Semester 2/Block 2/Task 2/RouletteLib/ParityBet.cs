using System;
using System.Collections.Generic;

namespace RouletteLib
{
	public class ParityBet : BetEssence
	{
		public ParityBet(ParityBetsEnum bet)
		{
			Coefficient = 1;

			switch (bet)
			{
				case ParityBetsEnum.Even:
					VictoryList = new List<int>() { 2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30, 32, 34, 36 };
					break;
				case ParityBetsEnum.Odd:
					VictoryList = new List<int>() { 1, 3, 5, 7, 9, 11, 13, 15, 17, 19, 21, 23, 25, 27, 29, 31, 33, 35 };
					break;
				default:
					throw new Exception("Wrong parity.");
			}
		}
	}
}