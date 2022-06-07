using System;
using BotsLib;
using RouletteLib;
using LibLoader;

namespace RouletteApp
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("This program show the sum, that bots using different strategies have left after 40 bets.");
			Console.WriteLine();

			BetEssence bet = new ColourBet(ColourBetsEnum.Red);
			BotsLibLoader loader = new BotsLibLoader();
			loader.Load(@"..\..\..\..\BotsLib\BotsLib.dll", bet, 5000);

			for (int i = 0; i < loader.Bots.Count; i++)
			{
				Bot bot = loader.Bots[i];
				Console.WriteLine("On average, {0} has {1} from 5000 left after 40 bets.", bot.GetType(), bot.CountAverageCasheLeftover(40, 1000));
			}
		}
	}
}