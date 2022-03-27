using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlackjackBots;

namespace BlackjackBotsTests
{
    [TestClass]
    public class RiskyStrategyBotTests
    {
        [TestMethod]
        public void HitTest()
        {
            IBot bot = new RiskyStrategyBot();
            bool expectedTrue = true;
            bool expectedFalse = false;

            bool actualFirst = bot.Hit(7, 15);
            bool actualSecond = bot.Hit(9, 16);

            Assert.AreEqual(expectedTrue, actualFirst);
            Assert.AreEqual(expectedFalse, actualSecond);
        }
    }
}
