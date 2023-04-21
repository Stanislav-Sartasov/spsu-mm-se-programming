using NUnit.Framework;

namespace Bots.UnitTests;

public class RiskyBotTests
{
	[TestCase(1000, 333)]
	[TestCase(800, 266)]
	[TestCase(400, 10)]
	public void RiskyBotMakeBetTest(int balance, int result)
	{
		RiskyBot bot = new RiskyBot("Risky", balance);
		bot.MakeBet();
		Assert.AreEqual(result, bot.Bet);
	}
}