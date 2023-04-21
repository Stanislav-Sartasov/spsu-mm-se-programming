using Roulette;
using System;
using Plugin;
using System.Collections.Generic;

namespace Casino
{
	public class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Welcome to LUCKY ROULETTE!");
			Console.WriteLine("");

			if (args.Length != 1)
			{
				Console.WriteLine("Incorrect amount of parameters. Only one needed.");
				return;
			}

			List<Type> bots = new BotLoader().FindBots(args[0], typeof(Bot));

			if (bots == null)
				return;

			RouletteTable tableOne = new RouletteTable();

			for (int x = 0; x < bots.Count; x++)
			{
				object[] parameters = { bots[x].Name, 1000 };
				tableOne.AddPlayer((Player)Activator.CreateInstance(bots[x], parameters));
			}

			for (int x = 0; x < 40; x++)
				tableOne.Spin();

			Console.WriteLine("");
			tableOne.ShowInfoAboutPlayers();
		}
	}
}