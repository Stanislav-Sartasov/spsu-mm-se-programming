using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blackjack;
using BotsPluginManagement;

namespace BlackjackTests
{
    [TestClass]
    public class GameTests
    {
        private const string pathForTests = @"..\..\..\..\..\Bots";

        [TestMethod]
        public void PlayOneBetBalanceTrueTest()
        {
            Game game = new Game();
            APlayer? player = new BotsManager(pathForTests, 100).Bots[0];

            game.Play(player, 10, 1);

            Assert.IsTrue(player.Balance < 120);
        }

        [TestMethod]
        public void PlayOneBetBalanceFalseTest()
        {
            Game game = new Game();
            APlayer? player = new BotsManager(pathForTests, 100).Bots[0];

            game.Play(player, 10, 1);

            Assert.IsFalse(player.Balance < 90);
        }

        [TestMethod]
        public void PlayLotsOfBetsTest()
        {
            Game game = new Game();
            APlayer? player = new BotsManager(pathForTests, 100).Bots[0];

            game.Play(player, 100, 1000);
        }

        [TestMethod]
        public void PlayOneDeckTest()
        {
            Game game = new Game();
            APlayer? player = new BotsManager(pathForTests, 500).Bots[0];

            game.Play(player, 100, 100, 1);
        }

        [TestMethod]
        public void PlayZeroDeckTest()
        {
            Game game = new Game();
            APlayer? player = new BotsManager(pathForTests, 1000).Bots[0];

            game.Play(player, 100, 1000, 0);
        }

        [TestMethod]
        public void PlayNegativeNumOfBetsTest()
        {
            Game game = new Game();
            APlayer? player = new BotsManager(pathForTests, 1000).Bots[0];

            game.Play(player, 100, -1000, 0);
        }
    }
}