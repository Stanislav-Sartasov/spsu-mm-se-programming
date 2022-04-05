using NUnit.Framework;
using BlackjackBots;
using AbstractClasses;

namespace BlackackTests.BotTests
{
    public class BaseBotFuctionsTests
    {
        private const double exampleStartMoney = 1000;
        private const double exampleStartRate = 50;

        private void ConstructorTest(ABot bot)
        {
            Assert.IsTrue(bot.IsWantNextGame);
            Assert.IsTrue(bot.CountGames == 0);
            Assert.IsTrue(bot.Money == exampleStartMoney);
            Assert.IsTrue(bot.Rate == exampleStartRate);
            Assert.IsNotNull(bot.CardsInHand);
        }

        [Test]
        public void UsualBaseStrategyConstructorTest()
        {
            var bot = new UsualBaseStrategyBot(exampleStartMoney, exampleStartRate);
            ConstructorTest(bot);
        }

        [Test]
        public void MartingaleBotConstructorTest()
        {
            var bot = new MartingaleBot(exampleStartMoney, exampleStartRate);
            ConstructorTest(bot);
        }

        [Test]
        public void OneThreeTwoSixBotConstructorTest()
        {
            var bot = new OneThreeTwoSixBot(exampleStartMoney, exampleStartRate);
            ConstructorTest(bot);
        }

        [Test]
        public void PrimitiveManchetanStrategyBotConstructorTest()
        {
            var bot = new PrimitiveManchetanStrategyBot(exampleStartMoney, exampleStartRate);
            ConstructorTest(bot);
        }

        [Test]
        public void BotWinningTest()
        {
            var bot = new PrimitiveManchetanStrategyBot(exampleStartMoney, exampleStartRate);
            bot.Win();
            Assert.IsTrue(bot.Money == exampleStartMoney + exampleStartRate);
            Assert.IsTrue(bot.CountGames == 1);
            Assert.IsTrue(bot.CountWinGames == 1);
            Assert.IsTrue(bot.IsWantNextGame);
        }

        [Test]
        public void BotLosingTest()
        {
            var bot = new PrimitiveManchetanStrategyBot(exampleStartMoney, exampleStartRate);
            bot.Lose();
            Assert.IsTrue(bot.Money == exampleStartMoney - exampleStartRate);
            Assert.IsTrue(bot.CountGames == 1);
            Assert.IsTrue(bot.CountWinGames == 0);
            Assert.IsTrue(bot.IsWantNextGame);
        }
    }
}
