using NUnit.Framework;
using Blackjack.TestingDummies;
using Blackjack.PlayerLibrary;
using Blackjack.DealerLibrary;
using Blackjack.GeneralInfo;

namespace Blackjack.GameTable.UnitTests
{
	public class GameTableTests
	{
		[Test]
		public void DeckRunningOutTest()
		{
			AbstractPlayer player = new PlayerAlwaysHit();
			player.AddChips(1000);
			AbstractDealer dealer = new DealerDeckEater();
			GameTable table = new(player, dealer);
			Assert.AreEqual(RoundResult.DeckRunOut, table.RunNewRound().Result);
		}

		[Test]
		public void TableAddingChipsTest()
		{
			AbstractPlayer player = new PlayerAlwaysHit();
			int initial = 1000;
			player.AddChips(initial);
			AbstractDealer dealer = new DealerStackDoubler();
			GameTable table = new(player, dealer);
			table.RunNewRound();
			Assert.AreEqual(initial * 2, player.GetCurrentStack());
		}

		[Test]
		public void TableHandlingTieTest()
		{
			AbstractPlayer player = new PlayerAlwaysHit();
			AbstractDealer dealer = new DummyDealer();
			GameTable table = new(player, dealer);
			int initial = 1000;
			player.AddChips(initial);
			table.RunNewRound();
			Assert.AreEqual(initial, player.GetCurrentStack());
		}
	}
}
