using System;
using System.Collections.Generic;

namespace RouletteLib
{
	public class Roulette
	{
		private readonly List<int> RedNumbers = new List<int>() { 1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36 };
		private readonly List<int> BlackNumbers = new List<int>() { 2, 4, 6, 8, 10, 11, 13, 15, 17, 20, 22, 24, 26, 28, 29, 31, 33, 35 };
		private readonly List<int> EvenNumbers = new List<int>() { 2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30, 32, 34, 36 };
		private readonly List<int> OddNumbers = new List<int>() { 1, 3, 5, 7, 9, 11, 13, 15, 17, 19, 21, 23, 25, 27, 29, 31, 33, 35 };
		private readonly List<int> FirstDozen = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
		private readonly List<int> SecondDozen = new List<int>() { 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 };
		private readonly List<int> ThirdDozen = new List<int>() { 24, 25, 26, 27, 28, 29, 30, 31, 31, 32, 33, 34, 35, 35 };

		public bool Spin (string betEssence)
		{
			Random spinResult = new Random();
			int number = spinResult.Next(37);

			if (betEssence.Equals("red"))
			{
				if (RedNumbers.Contains(number))
					return true;
			}
			else if (betEssence.Equals("black"))
			{
				if (BlackNumbers.Contains(number))
					return true;
			}
			else if (betEssence.Equals("even"))
			{
				if (EvenNumbers.Contains(number))
					return true;
			}
			else if (betEssence.Equals("odd"))
			{
				if (OddNumbers.Contains(number))
					return true;
			}
			else if (betEssence.Equals("first dozen"))
			{
				if (FirstDozen.Contains(number))
					return true;
			}
			else if (betEssence.Equals("second dozen"))
			{
				if (SecondDozen.Contains(number))
					return true;
			}
			else if (betEssence.Equals("third dozen"))
			{
				if (ThirdDozen.Contains(number))
					return true;
			}
			else if (int.TryParse(betEssence, out int n) && n >= 0 && n <= 36)
			{
				if (number == n)
					return true;
			}
			else
			{
				throw new Exception("The bet can only be on: white or black, even or odd, first/second/third dozen, a number from [0,36]");
			}

			return false;
		}
	}
}
