using Roulette.Bets;
using System;
using System.Collections.Generic;

namespace Bots
{
	public class BotDAlembert : Bot
	{
		private readonly int unit;

		public BotDAlembert(string name, int deposit) : base(name, deposit)
		{
			unit = Balance / 40;
			wins = BetsWin;
			money = 0;
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

				playersBets.Add(CreateBet(player, money, betCell));
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

				playersBets.Add(CreateBet(player, money, betCell));
				Balance -= money;
			}
			return playersBets;
		}
	}
}