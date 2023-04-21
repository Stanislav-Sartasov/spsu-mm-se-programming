using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlackjackBots;

namespace BlackjackBotsTests
{
    [TestClass]
    public class ConservativeStrategyBotTests
    {
        [TestMethod]
        public void DoesHitTest()
        {
            bool expected = true;
            ConservativeStrategyBot bot = new ConservativeStrategyBot(100);

            bool actual = bot.DoesHit(7);

            Assert.AreEqual(expected, actual);
        }
    }
}
