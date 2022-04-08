using System;
using System.Collections.Generic;


namespace Roulette
{
	public abstract class Player
	{
		public readonly string Name;
		public readonly int Deposit;
		public int Balance { get; protected internal set; }
		public int profit { get; internal set; }
		public int AmountOfBets { get; protected set; }
		public int betsWin { get; internal set; }
		protected internal int Flag;
		private static string[] possibleBets;

		public Player(string name, int deposit)
		{
			Name = name;
			Deposit = deposit;
			Balance = deposit;
			profit = 0;
			AmountOfBets = 0;
			betsWin = 0;
			Flag = 1;

			possibleBets = new string[44];
			for (int x = 0; x < 37; x++)
			{
				possibleBets[x] = x.ToString();
			}
			possibleBets[37] = "red";
			possibleBets[38] = "black";
			possibleBets[39] = "odd";
			possibleBets[40] = "even";
			possibleBets[41] = "dozen 1";
			possibleBets[42] = "dozen 2";
			possibleBets[43] = "dozen 3";
		}

		public abstract List<Bet> MakeBet(int player);

		public void GetInfo()
		{
			Console.WriteLine("Player {0} made a deposit of {1} units.\nCurrent balance is {2}.", Name, Deposit, Balance);
			Console.WriteLine("Current Profit is {0}. {1} bets were made and {2} of them won.", profit, AmountOfBets, betsWin);
			Console.WriteLine("");
		}
	}
}