using NUnit.Framework;

namespace Bots.Tests
{
	public class LightBotTest
	{
		[Test]
		public void LightBotBetTest()
		{
			LightBot lightBot = new();
			lightBot.Bet();
			Assert.AreEqual(100, lightBot.GetBet());
			lightBot.Bet();
			Assert.AreEqual(100, lightBot.GetBet());

			Assert.Pass();
		}
	}
}