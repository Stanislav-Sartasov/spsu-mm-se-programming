using Roulette;
using System;
using System.Collections.Generic;

namespace Bots
{
	public class BotDAlembert : Player
	{
		private int unit;
		private string bet;
		private int wins;
		private int money;

		public BotDAlembert(string name, int deposit) : base(name, deposit)
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

			unit = Balance / 40;
		}

		public override List<Bet> MakeBet(int player)
		{
			if (Balance == 0)
			{
				Console.WriteLine("BotDAlembert lost all his money.");
				flag = 0;
				return null;
			}

			List<Bet> playersBets = new List<Bet>();

			if (AmountOfBets == 0)
			{
				money = unit;
				wins = 0;
				playersBets.Add(new Bet(player, bet, money));
				Balance -= money;
			}
			else
			{
				if (wins < BetsWin)
				{
					wins++;

					if (money != unit)
					{
						money -= unit;
					}
					if (money > Balance)
					{
						Console.WriteLine("BotDAlembert has some money, but it is impossible to continue the tactic.");
						flag = 0;
						return null;
					}

					playersBets.Add(new Bet(player, bet, money));
					Balance -= money;
				}
				else
				{
					money += unit;
					if (money > Balance)
					{
						Console.WriteLine("BotDAlembert has some money, but it is impossible to continue the tactic.");
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
