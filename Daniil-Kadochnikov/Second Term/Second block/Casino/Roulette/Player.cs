using Roulette.Bets;
using System;
using System.Collections.Generic;

namespace Roulette
{
	public abstract class Player
	{
		public readonly string Name;
		public readonly int Deposit;
		public int Balance { get; protected internal set; }
		public int Profit { get; internal set; }
		public int AmountOfBets { get; internal set; }
		public int BetsWin { get; internal set; }
		protected internal int flag;

		public Player(string name, int deposit)
		{
			Name = name;
			Deposit = deposit;
			Balance = deposit;
			Profit = 0;
			AmountOfBets = 0;
			BetsWin = 0;
			flag = 1;
		}

		public abstract List<Bet> MakeBet(int player);

		public void ShowInfo()
		{
			Console.WriteLine("Player {0} made a deposit of {1} units.\nCurrent balance is {2}.", Name, Deposit, Balance);
			Console.WriteLine("Current Profit is {0}. {1} bets were made and {2} of them won.", Profit, AmountOfBets, BetsWin);
			Console.WriteLine("");
		}
	}
}