using NUnit.Framework;

namespace Bots.Tests
{
	public class RandomBotTest
	{
		[Test]
		public void RandomBotBetTest()
		{
			RandomBot randomBot = new();
			randomBot.Bet();
			Assert.IsNotNull(randomBot.GetBet());

			Assert.Pass();
		}
	}
}