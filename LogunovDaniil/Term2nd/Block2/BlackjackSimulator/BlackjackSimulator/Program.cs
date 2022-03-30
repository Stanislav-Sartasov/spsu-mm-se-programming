using Blackjack.Dealers;
using Blackjack.Players;
using Blackjack.StatCollector;

namespace BlackjackSimulator
{
	public class Program
	{
		public static int Main()
		{
			ConsoleMessages.SendGreeitngs();

			RandomPlayer random = new();
			Hit17Player hit17 = new();
			BasicStrategyPlayer basic = new();

			StandartDealer dealer = new();

			int initialStack = 40000;
			int roundsPerGame = 40;
			int gameCycles = 1000;

			StatCollector.RunBlackjackGame(dealer, random, "RandomPlayerTest",
				initialStack, roundsPerGame, gameCycles).PrintMainData(Console.OpenStandardOutput());

			Console.WriteLine();

			StatCollector.RunBlackjackGame(dealer, hit17, "Hit17PlayerTest",
				initialStack, roundsPerGame, gameCycles).PrintMainData(Console.OpenStandardOutput());

			Console.WriteLine();

			StatCollector.RunBlackjackGame(dealer, basic, "BasicStrategyTest",
				initialStack, roundsPerGame, gameCycles).PrintMainData(Console.OpenStandardOutput());

			return 0;
		}
	}
}
