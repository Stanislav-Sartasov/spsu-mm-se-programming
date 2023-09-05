using NUnit.Framework;
using MartingaleBotLibrary;
using OneThreeTwoSixBotLibrary;
using UsualBaseStrategyBotLibrary;
using PrimitiveManchetanStrategyBotLibrary;
using AbstractClasses;

namespace BlackackTests.BotTests
{
    public class BaseBotFuctionsTests
    {
        private const double ExampleStartMoney = 1000;
        private const double ExampleStartRate = 50;

        private void ConstructorTest(ABot bot)
        {
            Assert.IsTrue(bot.IsWantNextGame);
            Assert.IsTrue(bot.CountGames == 0);
            Assert.IsTrue(bot.Money == ExampleStartMoney);
            Assert.IsTrue(bot.Rate == ExampleStartRate);
            Assert.IsNotNull(bot.CardsInHand);
        }

        [Test]
        public void UsualBaseStrategyConstructorTest()
        {
            var bot = new UsualBaseStrategyBot(ExampleStartMoney, ExampleStartRate);
            ConstructorTest(bot);
        }

        [Test]
        public void MartingaleBotConstructorTest()
        {
            var bot = new MartingaleBot(ExampleStartMoney, ExampleStartRate);
            ConstructorTest(bot);
        }

        [Test]
        public void OneThreeTwoSixBotConstructorTest()
        {
            var bot = new OneThreeTwoSixBot(ExampleStartMoney, ExampleStartRate);
            ConstructorTest(bot);
        }

        [Test]
        public void PrimitiveManchetanStrategyBotConstructorTest()
        {
            var bot = new PrimitiveManchetanStrategyBot(ExampleStartMoney, ExampleStartRate);
            ConstructorTest(bot);
        }

        [Test]
        public void BotWinningTest()
        {
            var bot = new PrimitiveManchetanStrategyBot(ExampleStartMoney, ExampleStartRate);
            bot.Win();
            Assert.IsTrue(bot.Money == ExampleStartMoney + ExampleStartRate);
            Assert.IsTrue(bot.CountGames == 1);
            Assert.IsTrue(bot.CountWinGames == 1);
            Assert.IsTrue(bot.IsWantNextGame);
        }

        [Test]
        public void BotLosingTest()
        {
            var bot = new PrimitiveManchetanStrategyBot(ExampleStartMoney, ExampleStartRate);
            bot.Lose();
            Assert.IsTrue(bot.Money == ExampleStartMoney - ExampleStartRate);
            Assert.IsTrue(bot.CountGames == 1);
            Assert.IsTrue(bot.CountWinGames == 0);
            Assert.IsTrue(bot.IsWantNextGame);
        }
    }
}
