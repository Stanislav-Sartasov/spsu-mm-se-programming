using System;
using BotsLib;
using RouletteLib;

namespace RouletteApp
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("This program show the sum, that bots using different strategies have left after 40 bets.");
			Console.WriteLine();

			BetEssence bet = new ColourBet(ColourBetsEnum.Red);

			Bot Martingale = new BotMartingale(10, bet, 5000);
			Console.WriteLine("On average, a bot using the Martingale strategy has {0} from 5000 left after 40 bets.", Martingale.CountAverageCasheLeftover(40, 1000));

			Bot DAlembert = new BotDAlembert(10, bet, 5000);
			Console.WriteLine("On average, a bot using the D'Alambert strategy has {0} from 5000 left after 40 bets.", DAlembert.CountAverageCasheLeftover(40, 1000));

			Bot Laboucherer = new BotLaboucherer(bet, 5000);
			Console.WriteLine("On average, a bot using the Laboucherer strategy has {0} from 5000 left after 40 bets.", Laboucherer.CountAverageCasheLeftover(40, 1000));
		}
	}
}
