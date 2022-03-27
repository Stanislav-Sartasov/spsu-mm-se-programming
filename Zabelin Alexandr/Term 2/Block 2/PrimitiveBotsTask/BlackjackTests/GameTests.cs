using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blackjack;
using BlackjackBots;

namespace BlackjackTests
{
    [TestClass]
    public class GameTests
    {
        [TestMethod]
        public void GameTest()
        {
            float expected = 12.7F;

            Game game = new Game(12.7F);

            Assert.AreEqual(expected, game.StartBalance);
        }

        [TestMethod]
        public void GetPlayerBalanceTest()
        {
            float expected = 100;
            Game game = new Game(100);
            IBot bot = new BaseStrategyBot();

            float actual = game.GetPlayerBalance();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void PlayTest()
        {
            Game game = new Game(1000);
            IBot bot = new ConservativeStrategyBot();

            for (int i = 0; i < 1000; i++)             // testing for exceptions
            {
                game.Play(20, 100, bot);
            }
        }
    }
}
