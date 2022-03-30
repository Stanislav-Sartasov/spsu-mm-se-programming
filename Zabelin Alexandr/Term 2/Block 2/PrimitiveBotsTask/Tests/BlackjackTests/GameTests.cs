using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blackjack;
using BlackjackBots;

namespace BlackjackTests
{
    [TestClass]
    public class GameTests
    {
        [TestMethod]
        public void PlayOneBetBalanceTrueTest()
        {
            Game game = new Game();
            APlayer player = new BaseStrategyBot(100);

            game.Play(player, 10, 1);

            Assert.IsTrue(player.Balance < 120);
        }

        [TestMethod]
        public void PlayOneBetBalanceFalseTest()
        {
            Game game = new Game();
            APlayer player = new BaseStrategyBot(100);

            game.Play(player, 10, 1);

            Assert.IsFalse(player.Balance < 90);
        }

        [TestMethod]
        public void PlayLotsOfBetsTest()
        {
            Game game = new Game();
            APlayer player = new BaseStrategyBot(100);

            game.Play(player, 100, 1000);
        }

        [TestMethod]
        public void PlayOneDeckTest()
        {
            Game game = new Game();
            APlayer player = new BaseStrategyBot(500);

            game.Play(player, 100, 100, 1);
        }

        [TestMethod]
        public void PlayZeroDeckTest()
        {
            Game game = new Game();
            APlayer player = new BaseStrategyBot(1000);

            game.Play(player, 100, 1000, 0);
        }

        [TestMethod]
        public void PlayNegativeNumOfBetsTest()
        {
            Game game = new Game();
            APlayer player = new BaseStrategyBot(1000);

            game.Play(player, 100, -1000, 0);
        }
    }
}