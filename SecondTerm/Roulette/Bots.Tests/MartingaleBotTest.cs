using NUnit.Framework;

namespace Bots.Tests
{
	public class MartingaleBotTest
	{
		[Test]
		public void MartingaleBotBetTest()
		{
			MartingaleBot martingaleBot = new();
			martingaleBot.Bet();
			if (martingaleBot.GetBalance() < 100000)
				Assert.AreEqual(200, martingaleBot.GetBet());
			else
				Assert.AreEqual(100, martingaleBot.GetBet());

			Assert.Pass();
		}
	}
}