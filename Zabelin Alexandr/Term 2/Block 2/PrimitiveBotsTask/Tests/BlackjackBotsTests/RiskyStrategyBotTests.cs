using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlackjackBots;

namespace BlackjackBotsTests
{
    [TestClass]
    public class RiskyStrategyBotTests
    {
        [TestMethod]
        public void DoesHitTest()
        {
            bool expected = true;
            RiskyStrategyBot bot = new RiskyStrategyBot(100);

            bool actual = bot.DoesHit(11);

            Assert.AreEqual(expected, actual);
        }
    }
}
