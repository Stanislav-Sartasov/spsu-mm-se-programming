namespace AdditionalBots.UnitTests;
using NUnit.Framework;
using Roulette;
using AdditionalBots;

public class BotAlexTests
{
    [Test]
    public void PlayManyGamesTest()
    {
        Casino casino = new Casino(100, 5000);
        BotAlex bot = new BotAlex(6000, casino);

        for (int i = 0; i < 10000; i++)
        {
            if (bot.AmountOfMoney >= casino.MinBetAmount)
            {
                Assert.AreEqual(true, casino.PlayWith(bot));
            }
            else
            {
                Assert.AreEqual(false, casino.PlayWith(bot));
            }
        }
    }

    [Test]
    public void GiveNewRulesTest()
    {
        Casino casino = new Casino(300, 30000);
        BotAlex bot = new BotAlex(3000, casino);

        for (int i = 0; i < 10; i++)
        {
            casino.PlayWith(bot);
        }

        Casino newCasino = new Casino(200, 15000);
        bot.GiveNewRules(newCasino);

        for (int i = 0; i < 100; i++)
        {
            if (bot.AmountOfMoney >= newCasino.MinBetAmount)
            {
                Assert.AreEqual(true, newCasino.PlayWith(bot));
            }
            else
            {
                Assert.AreEqual(false, newCasino.PlayWith(bot));
            }
        }
    }
}
