using System;

namespace Roulette
{
	public static class Game
	{
		public static int VictoryCell;

		public static void SpintheDrum()
		{
			Random rnd = new Random();
			VictoryCell = rnd.Next(0,36);
		}

		public static void GetMoney(APlayer person)
		{
			person.Winnings(CheckOfWin.GetCoefficient(person.GetTypeOfBet()), CheckOfWin.CheckofWin(person.GetTypeOfBet(), person.GetCell()));
		}
	}
}
