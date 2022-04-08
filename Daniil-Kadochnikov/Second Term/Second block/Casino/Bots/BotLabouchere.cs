using Roulette;
using System;
using System.Collections.Generic;

namespace Bots
{
	public class BotLabouchere : Player
	{
		private List<int> list;
		private string bet;
		private int wins;
		private int money;

		public BotLabouchere(string name, int deposit) : base(name, deposit)
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

			list = new List<int>();
			int sum = Balance / 20;
			int unit = sum / 15;

			list.Add(unit);
			list.Add(unit * 2);
			list.Add(unit * 3);
			list.Add(unit * 4);
			list.Add(sum - unit - unit * 2 - unit * 3 - unit * 4);
		}

		public override List<Bet> MakeBet(int player)
		{
			if (Balance == 0)
			{
				Console.WriteLine("BotLabouchere lost all his money.");
				flag = 0;
				return null;
			}

			List<Bet> playersBets = new List<Bet>();

			if (AmountOfBets == 0)
			{
				money = list[0] + list[4];
				wins = 0;
				playersBets.Add(new Bet(player, bet, money));
				Balance -= money;
				AmountOfBets++;
			}
			else
			{
				if (wins < BetsWin)
				{
					wins++;
					list.Remove(0);
					list.Remove(list.Count - 1);

					if (list.Count == 0)
					{
						int sum = Balance / 20;
						int unit = sum / 15;
						list.Add(unit);
						list.Add(unit * 2);
						list.Add(unit * 3);
						list.Add(unit * 4);
						list.Add(sum - unit - unit * 2 - unit * 3 - unit * 4);
					}

					money = list[0] + list[list.Count - 1];
					if (money > Balance)
					{
						Console.WriteLine("BotLabouchere has some money, but it is impossible to continue the tactic.");
						flag = 0;
						return null;
					}

					playersBets.Add(new Bet(player, bet, money));
					Balance -= money;
					AmountOfBets++;
				}
				else
				{
					list.Add(money);

					money = list[0] + list[list.Count - 1];
					if (money > Balance)
					{
						Console.WriteLine("BotLabouchere has some money, but it is impossible to continue the tactic.");
						flag = 0;
						return null;
					}

					playersBets.Add(new Bet(player, bet, money));
					Balance -= money;
					AmountOfBets++;
				}
			}
			return playersBets;
		}
	}
}
