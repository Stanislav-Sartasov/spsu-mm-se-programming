using System;

namespace Roulette
{
	public abstract class APlayer
	{
		protected int cash;
		protected int oldCash;
		protected string name;
		protected int currentBet;
		protected TypeOfBet typeOfBet;
		protected int cells;

		public int GetBet()
		{
			return currentBet;
		}

		public int GetBalance()
		{
			return cash;
		}

		public TypeOfBet GetTypeOfBet()
		{
			return typeOfBet;
		}

		public int GetCell()
		{
			return cells;
		}

		public string GetName()
		{
			return name;
		}

		public void Winnings(int coefficient, bool result)
		{
			if (result)
			{
				oldCash = cash;
				cash += coefficient * currentBet;
				Console.WriteLine($"{GetName()} bet on {GetTypeOfBet()} and won {coefficient * currentBet}");
			}
			else
			{
				oldCash = cash;
				Console.WriteLine($"{GetName()} bet on {GetTypeOfBet()} and lose");
				cash -= currentBet;
			}
		}

		public abstract void Bet();
	}
}
