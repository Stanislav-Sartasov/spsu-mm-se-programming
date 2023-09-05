using NUnit.Framework;

namespace Bots.UnitTests;

public class MadBotTests
{
	[TestCase(1000)]
	public void RiskyBotMakeBetTest(int balance)
	{
		MadBot bot = new MadBot("Mad", balance);
		bot.MakeBet();
		if (bot.Bet > 1 && bot.Bet <= 1000)
			Assert.Pass();
	}
}