using NUnit.Framework;
using Blackjack.TestingDummies;
using System.IO;

namespace Blackjack.StatCollector.UnitTests
{
	public class Tests
	{
		[Test]
		public void SimpleAverageWinsOnTieTest()
		{
			DummyDealer dealer = new();
			PlayerAlwaysHit player = new();
			StatContainer stat = StatCollector.RunBlackjackGame(dealer, player, "test", 1000, 2, 2);
			Assert.AreEqual(0, stat.AverageWins);
		}
		[Test]
		public void SimpleAverageWinsOnWinTest()
		{
			DealerStackDoubler dealer = new();
			PlayerAlwaysHit player = new();
			StatContainer stat = StatCollector.RunBlackjackGame(dealer, player, "test", 1000, 2, 2);
			Assert.AreEqual(2, stat.AverageWins);
		}

		[Test]
		public void SimpleAverageReturnTest()
		{
			DealerStackDoubler dealer = new();
			PlayerAlwaysHit player = new();
			StatContainer stat = StatCollector.RunBlackjackGame(dealer, player, "test", 1000, 1, 2);
			Assert.AreEqual(1000, stat.AverageReturn);
		}

		[Test]
		public void NoErrorPrintTest()
		{
			DummyDealer dealer = new();
			PlayerAlwaysHit player = new();
			StatContainer stat = StatCollector.RunBlackjackGame(dealer, player, "test", 1000, 1, 1);
			byte[] data = new byte[1000];
			using (MemoryStream ms = new MemoryStream(data))
			{
				Assert.AreEqual(0, stat.PrintMainData(ms));
			}
		}
	}
}