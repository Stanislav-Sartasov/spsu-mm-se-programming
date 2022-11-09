using NUnit.Framework;
using Bots;

namespace Roulette.Tests
{
	class PlayerTest
	{
		[Test]
		public void WinningsTest()
		{
			LightBot testBot = new("TestBot", 100000);
			testBot.Winnings(1, true);
			Assert.AreEqual(100100, testBot.GetBalance());
			testBot.Winnings(1, false);
			Assert.AreEqual(100000, testBot.GetBalance());

			Assert.Pass();
		}

		[Test]
		public void GetNameTest()
		{
			Player player = new("testName", 1000);
			Assert.AreEqual("testName", player.GetName());
			player = new("Name", 1000);
			Assert.AreEqual("Name", player.GetName());

			Assert.Pass();
		}

		[Test]
		public void GetBetTest()
		{
			Player testPlayer = new();
			Assert.AreEqual(100, testPlayer.GetBet());

			Assert.Pass();
		}

		[Test]
		public void GetBalanceTest()
		{
			Player player = new("testName", 1000);
			Assert.AreEqual(1000, player.GetBalance());
			player = new("testName", 777);
			Assert.AreEqual(777, player.GetBalance());

			Assert.Pass();
		}

		[Test]
		public void GetTypeOfBetTest()
		{
			LightBot testBot = new("TestBot", 100000);
			Assert.AreEqual(TypeOfBet.Single, testBot.GetTypeOfBet());

			Assert.Pass();
		}

		[Test]
		public void GetCellTest()
		{
			LightBot testBot = new("TestBot", 100000);
			Assert.AreEqual(0, testBot.GetCell());

			Assert.Pass();
		}
	}
}
