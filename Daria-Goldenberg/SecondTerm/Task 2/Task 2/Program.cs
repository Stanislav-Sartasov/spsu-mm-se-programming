using System;
using BlackJack;
using Bots;

namespace Task_2.Casino
{
	public class Program
	{
		public static void Main()
		{
            Game game = new Game();
            game.Players.Add(new FirstBot(game, 1000));
            game.Players.Add(new SecondBot(game, 1000));
            game.Players.Add(new ThirdBot(game, 1000));
            game.Start();
        }
	}
}
