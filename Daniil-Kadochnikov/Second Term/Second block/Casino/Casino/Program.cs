using Bots;
using Roulette;
using System;

namespace Casino
{
	public class Program
	{
		static void Main()
		{
			Console.WriteLine("Welcome to LUCKY ROULETTE!");
			Console.WriteLine("");

			RouletteTable tableOne = new RouletteTable();

			Player bot;
			bot = new BotDAlembert("BotDAlembert", 1000);
			tableOne.AddPlayer(bot);

			bot = new BotLabouchere("BotLabouchere", 1000);
			tableOne.AddPlayer(bot);

			bot = new BotMartingale("BotMartingale", 1000);
			tableOne.AddPlayer(bot);

			for (int x = 0; x < 40; x++)
				tableOne.Spin();

			Console.WriteLine("");
			tableOne.GetInfoAboutPlayers();
		}
	}
}
