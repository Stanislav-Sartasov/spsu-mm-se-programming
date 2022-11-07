using System;
using Roulette;
using Bots;

namespace Task2
{
	class Program
	{

		static void Main(string[] args)
		{
			Console.WriteLine("Welcome to the roulette table\n" +
				"Press esc if you want to see the results of the bots. Press any other key to continue");
			if (Console.ReadKey().Key != ConsoleKey.Escape)
			{
				Player player = new();
				while (player.GetBalance() >= 100)
				{
					Game.SpintheDrum();
					player.Bet();
					Game.GetMoney(player);
					Console.WriteLine($"Your balance: {player.GetBalance()}");
				}
				Console.WriteLine("The minimum bet is one hundred.Insufficient funds on the account.");
			}
			else
			{
				MartingaleBot martingale = new();
				LightBot lightBot = new();
				RandomBot randomBot = new();
				int countOfGames = 100;
				int countOfTunrs = 40;
				long sumsM, sumsL, sumsR;
				sumsM = sumsL = sumsR = 0;
				for (int j = 0; j < countOfGames; j++)
				{
					for (int i = 0; i < countOfTunrs; i++)
					{
						Game.SpintheDrum();
						martingale.Bet();
						lightBot.Bet();
						randomBot.Bet();
						Game.GetMoney(martingale);
						Game.GetMoney(lightBot);
						Game.GetMoney(randomBot);
					}
					sumsL += lightBot.GetBalance();
					sumsM += martingale.GetBalance();
					sumsR += randomBot.GetBalance();
				}
				Console.WriteLine("MartingaleBot on average after " + countOfGames + " games has " + sumsM / countOfGames +
					"\nLightBot on average after " + countOfGames + " games has " + sumsL / countOfGames +
					"\nRandomBot on average after " + countOfGames + " games has " + sumsR / countOfGames);
			}
		}
	}
}
