using Bots;
using NUnit.Framework;

namespace Roulette.Tests
{
	public class GameTests
	{
		[Test]
		public void SpinTheDrumTest()
		{
			Game.SpintheDrum();
			Assert.IsNotNull(Game.VictoryCell);

			Assert.Pass();
		}

		[Test]
		public void GetMoneyTest()
		{
			RandomBot testBot = new();
			int oldCash = testBot.GetBalance();
			testBot.Bet();
			Game.GetMoney(testBot);

			Assert.AreNotEqual(oldCash, testBot.GetBalance());

			Assert.Pass();
		}
	}
}