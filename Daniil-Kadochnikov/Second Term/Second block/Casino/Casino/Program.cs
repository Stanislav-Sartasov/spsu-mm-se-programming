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

			tableOne.AddPlayer(new BotDAlembert("BotDAlembert", 1000));
			tableOne.AddPlayer(new BotLabouchere("BotLabouchere", 1000));
			tableOne.AddPlayer(new BotMartingale("BotMartingale", 1000));

			for (int x = 0; x < 40; x++)
				tableOne.Spin();

			Console.WriteLine("");
			tableOne.ShowInfoAboutPlayers();
		}
	}
}