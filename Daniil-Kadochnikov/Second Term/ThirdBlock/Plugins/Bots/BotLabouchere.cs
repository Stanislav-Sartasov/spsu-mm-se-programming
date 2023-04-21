using Roulette.Bets;
using Roulette;
using System;
using System.Collections.Generic;

namespace Bots
{
	public class BotLabouchere : Bot
	{
		private List<int> list;

		public BotLabouchere(string name, int deposit) : base(name, deposit)
		{
			wins = BetsWin - 1;
			list = new List<int>();
			list.Add(0);
			list.Add(0);
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

				betDirector.Construct(player, money);
				playersBets.Add(betDirector.GetBet());
				Balance -= money;
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

				betDirector.Construct(player, money);
				playersBets.Add(betDirector.GetBet());
				Balance -= money;
			}
			return playersBets;
		}
	}
}