using Roulette;
using System;
using System.Collections.Generic;

namespace Bots
{
	public class BotMartingale : Player
	{
		private string bet;
		private int money;
		private int wins;

		public BotMartingale(string name, int deposit) : base(name, deposit)
		{
			Random rnd = new Random();
			int value = rnd.Next(0, 4);

			if (value == 0)
				bet = "red";
			else if (value == 1)
				bet = "black";
			else if (value == 2)
				bet = "odd";
			else
				bet = "even";
		}

		public override List<Bet> MakeBet(int player)
		{
			if (Balance == 0)
			{
				Console.WriteLine("BotMartingale lost all his money.");
				flag = 0;
				return null;
			}

			List<Bet> playersBets = new List<Bet>();

			if (AmountOfBets == 0)
			{
				wins = 0;
				money = Balance / 40;
				playersBets.Add(new Bet(player, bet, money));
				Balance -= money;
			}
			else
			{
				if (wins < BetsWin)
				{
					wins++;
					money = Balance / 40;
					playersBets.Add(new Bet(player, bet, money));
					Balance -= money;
				}
				else
				{
					money *= 2;
					if (money > Balance)
					{
						Console.WriteLine("BotMartingale has some money, but it is impossible to continue the tactic.");
						flag = 0;
						return null;
					}

					playersBets.Add(new Bet(player, bet, money));
					Balance -= money;
				}
			}
			return playersBets;
		}
	}
}