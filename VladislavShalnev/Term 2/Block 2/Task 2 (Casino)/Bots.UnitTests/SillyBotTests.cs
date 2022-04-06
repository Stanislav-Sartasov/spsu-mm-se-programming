using NUnit.Framework;

namespace Bots.UnitTests;

public class SillyBotTests
{
	[TestCase(1000, 10)]
	[TestCase(300, 1)]
	public void SillyBotMakeBetTest(int balance, int result)
	{
		SillyBot bot = new SillyBot("Silly", balance);
		bot.MakeBet();
		Assert.AreEqual(result, bot.Bet);
	}
}