using System.Linq;
using NUnit.Framework;
using BlackjackBots;
using BlackjackMechanics.GameTools;

namespace BlackackTests.GameTests
{
    public class BlackjackGameTests
    {
        private const int countDecksInOne = 8;
        private const int minimumRate = 50;
        // All bots game test

        [Test]
        public void CreateGameTest()
        {
            var bot = new UsualBaseStrategyBot(1000, minimumRate);
            var game = new Game(bot);

            Assert.DoesNotThrow(() => game.CreateGame(countDecksInOne));
        }

        [Test]
        public void GameWithMartingaleBotConstructorTest()
        {
            var firstBot = new MartingaleBot(1000, minimumRate);
            var firstGame = new Game(firstBot);
            firstGame.CreateGame(countDecksInOne);

            Assert.IsNotNull(firstGame);
            Assert.IsNotNull(firstGame.Deck);
            Assert.IsNotNull(firstGame.Dealer);
            Assert.IsNotNull(firstGame.Bot);
            Assert.AreEqual(firstBot, firstGame.Bot);
        }

        [Test]
        public void GameWithOneThreeTwoSixBotConstructorTest()
        {
            var secondBot = new OneThreeTwoSixBot(1000, minimumRate);
            var secondGame = new Game(secondBot);
            secondGame.CreateGame(countDecksInOne);

            Assert.IsNotNull(secondBot);
            Assert.IsNotNull(secondGame.Deck);
            Assert.IsNotNull(secondGame.Dealer);
            Assert.IsNotNull(secondGame.Bot);
            Assert.AreEqual(secondBot, secondGame.Bot);
        }

        public void GameWithPrimitiveBotConstructorTest()
        {
            var thirdBot = new PrimitiveManchetanStrategyBot(1000, minimumRate);
            var thirdGame = new Game(thirdBot);
            thirdGame.CreateGame(countDecksInOne);

            Assert.IsNotNull(thirdBot);
            Assert.IsNotNull(thirdGame.Deck);
            Assert.IsNotNull(thirdGame.Dealer);
            Assert.IsNotNull(thirdGame.Bot);
            Assert.AreEqual(thirdBot, thirdGame.Bot);
        }

        [Test]
        public void MartingaleBotPlayTest()
        {
            var firstBot = new MartingaleBot(1000, minimumRate);
            var firstGame = new Game(firstBot);
            firstGame.CreateGame(countDecksInOne);

            int startCountOfCards = firstGame.Deck.Deck.Count();
            firstGame.CreateGame(countDecksInOne);

            Assert.DoesNotThrow(() => firstGame.StartGame());
            Assert.IsTrue(firstGame.Deck.Deck.Count() < startCountOfCards);
        }

        [Test]
        public void OneThreeTwoSixBotPlayTest()
        {
            var secondBot = new OneThreeTwoSixBot(1000, minimumRate);
            var secondGame = new Game(secondBot);
            secondGame.CreateGame(countDecksInOne);

            int startCountOfCards = secondGame.Deck.Deck.Count();
            secondGame.CreateGame(countDecksInOne);

            Assert.DoesNotThrow(() => secondGame.StartGame());
            Assert.IsTrue(secondGame.Deck.Deck.Count() < startCountOfCards);
        }

        [Test]
        public void PrimitiveBotPlayTest()
        {
            var thirdBot = new PrimitiveManchetanStrategyBot(1000, minimumRate);
            var thirdGame = new Game(thirdBot);
            thirdGame.CreateGame(countDecksInOne);

            int startCountOfCards = thirdGame.Deck.Deck.Count();
            thirdGame.CreateGame(countDecksInOne);

            Assert.DoesNotThrow(() => thirdGame.StartGame());
            Assert.IsTrue(thirdGame.Deck.Deck.Count() < startCountOfCards);
        }
    }
}