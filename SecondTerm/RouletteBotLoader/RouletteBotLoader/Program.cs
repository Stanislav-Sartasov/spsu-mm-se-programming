using System;
using System.Collections.Generic;
using LibraryLoader;
using Roulette;

namespace RouletteBotLoader
{
	class Program
	{
		static void Main(string[] args)
		{
			int countOfGames = 50;
			Loader load = new();
			Console.Write("Enter the path to the folder with the libraries of bots.\n> ");
			string path = Console.ReadLine();

			List<Player> bots = load.LoadBots(path);
			if (bots == null)
				return;

			for (int i = 0; i < countOfGames; i++)
			{
				Game.SpintheDrum();
				foreach (var bot in bots)
				{
					bot.Bet();
					Game.GetMoney(bot);
				}
			}
		}
	}
}
