using System;

namespace Roulette
{
	public class CheckOfWin
	{
		public static bool HitTheSector(int min, int max)
		{
			if (min <= max)
				return (Game.VictoryCell >= min && Game.VictoryCell <= max);
			return (Game.VictoryCell <= min && Game.VictoryCell >= max);
		}

		public static bool CheckofWin(TypeOfBet typeOfBet, int cells)
		{
			int vCell = Game.VictoryCell;
			switch (typeOfBet)
			{
				case TypeOfBet.Single:
					return (cells == vCell);
				case TypeOfBet.Split:
					return (cells == vCell || cells + 1 == vCell);
				case TypeOfBet.Basket:
					return (cells == vCell || cells + 1 == vCell || vCell == 0);
				case TypeOfBet.Red:
					return (((vCell < 10 || vCell > 18 && vCell < 28) && vCell % 2 == 1) ||
							((vCell > 11 && vCell < 19 || vCell > 29) && vCell % 2 == 0));
				case TypeOfBet.Black:
					return (!(((vCell < 10 || vCell > 18 && vCell < 28) && vCell % 2 == 1) ||
							((vCell > 11 && vCell < 19 || vCell > 29) && vCell % 2 == 0)) && vCell != 0);
				case TypeOfBet.Even:
					return (vCell % 2 == 0);
				case TypeOfBet.Odd:
					return (vCell % 2 == 1);
				case TypeOfBet.FirstDozen:
					return HitTheSector(1, 12);
				case TypeOfBet.SecondDozen:
					return HitTheSector(13, 24);
				case TypeOfBet.ThirdDozen:
					return HitTheSector(25, 36);
				case TypeOfBet.SnakeBet:
					{
						int[] snakeArray = new int[] { 1, 5, 9, 12, 14, 16, 19, 23, 27, 30, 32, 34 };
						for (int i = 0; i < snakeArray.Length; i++)
						{
							if (vCell == snakeArray[i])
								return true;
						}
						return false;
					}
				default:
					Console.WriteLine("Something wrong");
					return false;
			}
		}

		public static int GetCoefficient(TypeOfBet typeOfBet)
		{
			int coefficient;
			if (typeOfBet >= TypeOfBet.FirstDozen)
				coefficient = 2;
			else if (typeOfBet >= TypeOfBet.Red)
				coefficient = 1;
			else if (typeOfBet == TypeOfBet.Basket)
				coefficient = 11;
			else if (typeOfBet == TypeOfBet.Split)
				coefficient = 18;
			else
				coefficient = 35;
			return coefficient;
		}
	}
}
