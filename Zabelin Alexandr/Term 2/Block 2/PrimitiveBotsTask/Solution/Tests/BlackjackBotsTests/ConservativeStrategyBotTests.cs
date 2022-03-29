using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlackjackBots;

namespace BlackjackBotsTests
{
    [TestClass]
    public class ConservativeStrategyBotTests
    {
        [TestMethod]
        public void HitTest()
        {
            IBot bot = new ConservativeStrategyBot();
            bool expectedTrue = true;
            bool expectedFalse = false;

            bool actualFirst = bot.Hit(7, 11);
            bool actualSecond = bot.Hit(14, 13);

            Assert.AreEqual(expectedTrue, actualFirst);
            Assert.AreEqual(expectedFalse, actualSecond);
        }
    }
}
