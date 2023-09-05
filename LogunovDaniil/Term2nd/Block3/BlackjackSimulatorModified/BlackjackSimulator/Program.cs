using Blackjack.Dealers;
using Blackjack.PlayerLoaderLibrary;
using Blackjack.StatCollector;

namespace BlackjackSimulator
{
	public class Program
	{
		public static int Main(string[] argv)
		{
			ConsoleMessages.SendGreetings();

			if (argv.Length != 1)
				return ConsoleMessages.SendError(1, "unexcpected amount of arguments; please pass only one path");

			var players = PlayerLoader.LoadFromDLL(argv[0]);

			if (players == null)
				return ConsoleMessages.SendError(-1, $"could not load .dll from the given path: {argv[0]}");

			StandartDealer dealer = new();

			int initialStack = 40000;
			int roundsPerGame = 40;
			int gameCycles = 1000;

			foreach (var player in players)
			{
				StatCollector.RunBlackjackGame(dealer, player, player.GetType().ToString(),
					initialStack, roundsPerGame, gameCycles).PrintMainData(Console.OpenStandardOutput());

				Console.WriteLine();
			}
			
			return 0;
		}
	}
}
