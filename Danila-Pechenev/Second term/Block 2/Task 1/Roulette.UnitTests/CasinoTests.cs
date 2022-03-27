namespace Roulette.UnitTests;
using NUnit.Framework;
using Bots;

public class CasinoTests
{
    [Test]
    public void InitTest()
    {
        Casino casino = new Casino(50, 10000);

        Assert.AreEqual(50, casino.MinBetAmount);
        Assert.AreEqual(10000, casino.MaxBetAmount);
    }

    [Test]
    public void NegativeNumberInBetTest()
    {
        Casino casino = new Casino(50, 10000);
        FirstTestBot bot = new FirstTestBot(100, casino);

        Assert.AreEqual(false, casino.PlayWith(bot));
    }

    [Test]
    public void DoesntWantToPlayTest()
    {
        Casino casino = new Casino(100, 10000);
        SecondTestBot bot = new SecondTestBot(99, casino);

        Assert.AreEqual(false, casino.PlayWith(bot));
    }

    [Test]
    public void NotEnoughMoneyTest()
    {
        Casino casino = new Casino(300, 20000);
        NinthTestBot bot = new NinthTestBot(200, casino);

        Assert.AreEqual(false, casino.PlayWith(bot));
    }

    [Test]
    public void IncorrectPositiveNumberTest()
    {
        Casino casino = new Casino(100, 10000);
        ThirdTestBot bot = new ThirdTestBot(5000, casino);

        Assert.AreEqual(false, casino.PlayWith(bot));
    }

    [Test]
    public void LessThenMinTest()
    {
        Casino casino = new Casino(100, 10000);
        FourthTestBot bot = new FourthTestBot(5000, casino);

        Assert.AreEqual(false, casino.PlayWith(bot));
    }

    [Test]
    public void GreaterThenMaxTest()
    {
        Casino casino = new Casino(100, 10000);
        FifthTestBot bot = new FifthTestBot(30000, casino);

        Assert.AreEqual(false, casino.PlayWith(bot));
    }

    [Test]
    public void ZeroTest()
    {
        Casino casino = new Casino(100, 10000);
        SixthTestBot bot = new SixthTestBot(5000, casino);

        Assert.AreEqual(false, casino.PlayWith(bot));
    }

    [Test]
    public void ThrowBallTest()
    {
        Casino casino = new Casino(100, 18000);
        BotOleg bot = new BotOleg(15000, casino);

        Assert.AreEqual(true, casino.PlayWith(bot));
    }

    [Test]
    public void GiveResultTest()
    {
        Casino casino = new Casino(100, 20000);
        SeventhTestBot bot = new SeventhTestBot(15000, casino);

        for (int i = 0; i < 100; i++)
        {
            int lastAmountOfMoney = bot.AmountOfMoney;
            casino.PlayWith(bot);
            Assert.AreEqual(bot.AmountOfMoney - lastAmountOfMoney > 0, bot.Won);
        }
    }

    [Test]
    public void RulesTest()
    {
        Casino firstCasino = new Casino(100, 20000);
        Casino secondCasino = new Casino(200, 17000);
        EighthTestBot bot = new EighthTestBot(15000, firstCasino);

        Assert.AreEqual(100, bot.GetMinimal());
        Assert.AreEqual(20000, bot.GetMaximal());

        bot.GiveNewRules(secondCasino);

        Assert.AreEqual(200, bot.GetMinimal());
        Assert.AreEqual(17000, bot.GetMaximal());
    }

    public void PlayManyGamesTest()
    {
        Casino casino = new Casino(100, 5000);
        BotAndrei bot = new BotAndrei(6000, casino);

        try
        {
            for (int i = 0; i < 10000; i++)
            {
                casino.PlayWith(bot);
            }
        }
        catch
        {
            Assert.Fail();
        }
    }
}
