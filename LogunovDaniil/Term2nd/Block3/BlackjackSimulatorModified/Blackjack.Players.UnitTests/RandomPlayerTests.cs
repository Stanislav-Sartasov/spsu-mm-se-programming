using NUnit.Framework;

namespace Blackjack.Players.UnitTests
{
	// since the player is designed to be random, we just check he returns valid bet
	public class Tests
	{
		[Test]
		public void GetNextBetWithinRangeTest()
		{
			RandomPlayer player = new();
			int initial = 100;
			int minBet = 10;
			player.AddChips(initial);
			player.PlaceBet(minBet);
			Assert.True(player.GetCurrentStack() <= initial - minBet);
		}
	}
}