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
		public int AmountOfBets { get; protected set; }
		public int BetsWin { get; internal set; }
		protected internal int flag;
		private static string[] PossibleBets;

		public Player(string name, int deposit)
		{
			Name = name;
			Deposit = deposit;
			Balance = deposit;
			Profit = 0;
			AmountOfBets = 0;
			BetsWin = 0;
			flag = 1;

			PossibleBets = new string[44];
			for (int x = 0; x < 37; x++)
			{
				PossibleBets[x] = x.ToString();
			}
			PossibleBets[37] = "red";
			PossibleBets[38] = "black";
			PossibleBets[39] = "odd";
			PossibleBets[40] = "even";
			PossibleBets[41] = "dozen 1";
			PossibleBets[42] = "dozen 2";
			PossibleBets[43] = "dozen 3";
		}

		public abstract List<Bet> MakeBet(int player);

		public void GetInfo()
		{
			Console.WriteLine("Player {0} made a deposit of {1} units.\nCurrent balance is {2}.", Name, Deposit, Balance);
			Console.WriteLine("Current Profit is {0}. {1} bets were made and {2} of them won.", Profit, AmountOfBets, BetsWin);
			Console.WriteLine("");
		}
	}
}