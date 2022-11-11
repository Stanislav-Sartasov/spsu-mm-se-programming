using NUnit.Framework;

namespace Bots.Tests
{
	public class RandomBotTest
	{
		[Test]
		public void RandomBotBetTest()
		{
			RandomBot randomBot = new("randomBot", 1000000);
			randomBot.Bet();
			Assert.IsNotNull(randomBot.GetBet());

			Assert.Pass();
		}
	}
}