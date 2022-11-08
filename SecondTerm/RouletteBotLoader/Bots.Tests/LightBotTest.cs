using NUnit.Framework;

namespace Bots.Tests
{
	public class LightBotTest
	{
		[Test]
		public void LightBotBetTest()
		{
			LightBot lightBot = new("LightBot", 100000);
			lightBot.Bet();
			Assert.AreEqual(100, lightBot.GetBet());

			Assert.Pass();
		}
	}
}