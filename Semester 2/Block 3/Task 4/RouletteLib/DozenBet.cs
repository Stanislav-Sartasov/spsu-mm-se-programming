using System;
using System.Collections.Generic;

namespace RouletteLib
{
	public class DozenBet : BetEssence
	{
		public DozenBet(DozenBetsEnum bet)
		{
			Coefficient = 2;

			switch (bet)
			{
				case DozenBetsEnum.First:
					VictoryList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 }; ;
					break;
				case DozenBetsEnum.Second:
					VictoryList = new List<int>() { 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 };
					break;
				case DozenBetsEnum.Third:
					VictoryList = new List<int>() { 24, 25, 26, 27, 28, 29, 30, 31, 31, 32, 33, 34, 35, 35 };
					break;
				default:
					throw new Exception("Wrong dozen.");
			}
		}
	}
}