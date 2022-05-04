using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bots;
using Blackjack;
using Plugin;

namespace Casino
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("This program is a blackjack game using bots from a library.");
			Console.WriteLine("Loading bots...");

			int sum = 500;
			Game game = new Game();
			
			game.Players = BotLoader.LoadBots("../../../../Bots/Bots.dll", sum);
			Console.WriteLine(game.Players.Count + " bots were found in the libary.\n");
			Console.WriteLine("The game starts here.");

			game.PlayGame(sum);
		}
	}
}
