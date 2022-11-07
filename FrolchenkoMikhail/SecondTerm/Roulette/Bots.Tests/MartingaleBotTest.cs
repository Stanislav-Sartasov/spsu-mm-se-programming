using NUnit.Framework;
using Roulette;

namespace Bots.Tests
{
	public class MartingaleBotTest
	{
		[Test]
		public void MartingaleBotBetTest()
		{
			MartingaleBot martingaleBot = new();
			martingaleBot.Bet();
			if (martingaleBot.GetBalance() < 10000)
				Assert.AreEqual(200, martingaleBot.GetBet());
			else
				Assert.AreEqual(100, martingaleBot.GetBet());

			Assert.Pass();
		}
	}
}