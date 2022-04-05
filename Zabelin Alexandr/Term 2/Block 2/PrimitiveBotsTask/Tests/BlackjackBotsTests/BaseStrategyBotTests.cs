using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlackjackBots;

namespace BlackjackBotsTests
{
    [TestClass]
    public class BaseStrategyBotTests
    {
        [TestMethod]
        public void DoesHitTest()
        {
            bool expected = true;
            BaseStrategyBot bot = new BaseStrategyBot(100);

            bool actual = bot.DoesHit(9);

            Assert.AreEqual(expected, actual);
        }
    }
}