using Roulette.Bets;
using System;
using System.Collections.Generic;

namespace Bots
{
	public class BotMartingale : Bot
	{
		public BotMartingale(string name, int deposit) : base(name, deposit)
		{
			wins = BetsWin - 1;
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

			if (wins < BetsWin)
			{
				wins++;
				money = Balance / 40;
				playersBets.Add(CreateBet(player, money, betCell));
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

				playersBets.Add(CreateBet(player, money, betCell));
				Balance -= money;
			}
			return playersBets;
		}
	}
}