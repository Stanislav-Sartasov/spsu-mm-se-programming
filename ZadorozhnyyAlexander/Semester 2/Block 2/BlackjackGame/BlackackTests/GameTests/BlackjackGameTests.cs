using System.Linq;
using NUnit.Framework;
using BlackjackBots;
using BlackjackMechanics.GameTools;

namespace BlackackTests.GameTests
{
    public class BlackjackGameTests
    {
        private const int CountDecksInOne = 8;
        private const int MinimumRate = 50;
        // All bots game test

        [Test]
        public void CreateGameTest()
        {
            var bot = new UsualBaseStrategyBot(1000, MinimumRate);
            var game = new Game(bot);

            Assert.DoesNotThrow(() => game.CreateGame(CountDecksInOne));
        }

        [Test]
        public void GameWithMartingaleBotConstructorTest()
        {
            var firstBot = new MartingaleBot(1000, MinimumRate);
            var firstGame = new Game(firstBot);
            firstGame.CreateGame(CountDecksInOne);

            Assert.IsNotNull(firstGame);
            Assert.IsNotNull(firstGame.Deck);
            Assert.IsNotNull(firstGame.Dealer);
            Assert.IsNotNull(firstGame.Bot);
            Assert.AreEqual(firstBot, firstGame.Bot);
        }

        [Test]
        public void GameWithOneThreeTwoSixBotConstructorTest()
        {
            var secondBot = new OneThreeTwoSixBot(1000, MinimumRate);
            var secondGame = new Game(secondBot);
            secondGame.CreateGame(CountDecksInOne);

            Assert.IsNotNull(secondBot);
            Assert.IsNotNull(secondGame.Deck);
            Assert.IsNotNull(secondGame.Dealer);
            Assert.IsNotNull(secondGame.Bot);
            Assert.AreEqual(secondBot, secondGame.Bot);
        }

        public void GameWithPrimitiveBotConstructorTest()
        {
            var thirdBot = new PrimitiveManchetanStrategyBot(1000, MinimumRate);
            var thirdGame = new Game(thirdBot);
            thirdGame.CreateGame(CountDecksInOne);

            Assert.IsNotNull(thirdBot);
            Assert.IsNotNull(thirdGame.Deck);
            Assert.IsNotNull(thirdGame.Dealer);
            Assert.IsNotNull(thirdGame.Bot);
            Assert.AreEqual(thirdBot, thirdGame.Bot);
        }

        [Test]
        public void MartingaleBotPlayTest()
        {
            var firstBot = new MartingaleBot(1000, MinimumRate);
            var firstGame = new Game(firstBot);
            firstGame.CreateGame(CountDecksInOne);

            int startCountOfCards = firstGame.Deck.Deck.Count();
            firstGame.CreateGame(CountDecksInOne);

            Assert.DoesNotThrow(() => firstGame.StartGame());
            Assert.IsTrue(firstGame.Deck.Deck.Count() < startCountOfCards);
        }

        [Test]
        public void OneThreeTwoSixBotPlayTest()
        {
            var secondBot = new OneThreeTwoSixBot(1000, MinimumRate);
            var secondGame = new Game(secondBot);
            secondGame.CreateGame(CountDecksInOne);

            int startCountOfCards = secondGame.Deck.Deck.Count();
            secondGame.CreateGame(CountDecksInOne);

            Assert.DoesNotThrow(() => secondGame.StartGame());
            Assert.IsTrue(secondGame.Deck.Deck.Count() < startCountOfCards);
        }

        [Test]
        public void PrimitiveBotPlayTest()
        {
            var thirdBot = new PrimitiveManchetanStrategyBot(1000, MinimumRate);
            var thirdGame = new Game(thirdBot);
            thirdGame.CreateGame(CountDecksInOne);

            int startCountOfCards = thirdGame.Deck.Deck.Count();
            thirdGame.CreateGame(CountDecksInOne);

            Assert.DoesNotThrow(() => thirdGame.StartGame());
            Assert.IsTrue(thirdGame.Deck.Deck.Count() < startCountOfCards);
        }
    }
}