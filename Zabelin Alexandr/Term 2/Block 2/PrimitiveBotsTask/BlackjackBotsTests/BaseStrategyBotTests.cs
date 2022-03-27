using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlackjackBots;

namespace BlackjackBotsTests
{
    [TestClass]
    public class BaseStrategyBotTests
    {
        [TestMethod]
        public void HitTestPlayerSumLess12()
        {
            bool expected = true;
            BaseStrategyBot bot = new BaseStrategyBot();

            bool actual = bot.Hit(9, 11);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void HitTestPlayerSum12AndCroupierLess5()
        {
            bool expected = true;
            BaseStrategyBot bot = new BaseStrategyBot();

            bool actual = bot.Hit(4, 12);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void HitTestPlayerSum13AndCroupier6()
        {
            bool expected = true;
            BaseStrategyBot bot = new BaseStrategyBot();

            bool actual = bot.Hit(6, 13);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void HitTestPlayerSumLess17AndCroupierMore6()
        {
            bool expected = true;
            BaseStrategyBot bot = new BaseStrategyBot();

            bool actual = bot.Hit(8, 16);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void HitTestPlayerSum18()
        {
            bool expected = false;
            BaseStrategyBot bot = new BaseStrategyBot();

            bool actual = bot.Hit(11, 18);

            Assert.AreEqual(expected, actual);
        }
    }
}