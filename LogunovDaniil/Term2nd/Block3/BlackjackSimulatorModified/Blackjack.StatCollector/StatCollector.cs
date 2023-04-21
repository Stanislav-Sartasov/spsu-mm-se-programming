using Blackjack.DealerLibrary;
using Blackjack.PlayerLibrary;
using Blackjack.GeneralInfo;

namespace Blackjack.StatCollector
{
	public static class StatCollector
	{
		public static StatContainer RunBlackjackGame(AbstractDealer dealer,
			AbstractPlayer player,
			string testName,
			int startingStack,
			int rounds,
			int cycles)
		{
			RoundMemo[][] roundMemos = new RoundMemo[cycles][];
			int totalWinnings = 0;
			for (int i = 0; i < cycles; i++)
			{
				roundMemos[i] = new RoundMemo[rounds];
				player.Flush();
				player.AddChips(startingStack);
				GameTable.GameTable game = new(player, dealer);
				for (int j = 0; j < rounds; j++)
				{
					roundMemos[i][j] = game.RunNewRound();
				}
				totalWinnings += player.GetCurrentStack() - startingStack;
			}
			return new(roundMemos, testName, startingStack, (double)totalWinnings / cycles);
		}
	}
}
