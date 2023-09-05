using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bots;
using Blackjack;

namespace Casino
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("This is a blackjack game.");
			int sum = 500;
			Game game = new Game(sum);
			game.Players.Add(new Bot(sum));
			game.Players.Add(new BotOne(sum));
			game.Players.Add(new BotTwo(sum));
			game.PlayGame();
		}
	}
}
