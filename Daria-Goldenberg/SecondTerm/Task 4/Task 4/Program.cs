using System;
using BlackJack;
using LibraryLoader;

namespace Task_4
{
	public class Program
	{
		public static void Main(string[] args)
		{
			if (args.Length != 1)
			{
				Console.WriteLine("Wrong path to bot libraries folder.");
				return;
			}

			Game game = new Game();
			game.Players.AddRange(Loader.LoadBots(args[0], game));
			game.Start();
		}
	}
}
