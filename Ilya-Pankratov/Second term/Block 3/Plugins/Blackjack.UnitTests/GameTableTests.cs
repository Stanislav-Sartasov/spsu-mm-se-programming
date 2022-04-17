using NUnit.Framework;
using Bots;

namespace Blackjack.UnitTests
{
    public class GameTableTests
    {
        [Test]
        public void ConstructorTest()
        {
            // first case

            int minBet = 6;
            var firstBot = new WinningStrategyBot();
            var firstGameTable = new GameTable(firstBot, minBet);

            Assert.IsNotNull(firstGameTable);
            Assert.IsNotNull(firstGameTable.Dealer);
            Assert.IsNotNull(firstGameTable.Player);
            Assert.IsNotNull(firstGameTable.GameDeck);
            Assert.AreSame(firstBot, firstGameTable.Player);
            Assert.AreEqual(firstGameTable.MinBet, minBet);

            // second case

            var secondBot = new HiLowStrategyBot();
            var secondGameTable = new GameTable(secondBot, minBet);

            Assert.IsNotNull(secondGameTable);
            Assert.IsNotNull(secondGameTable.Dealer);
            Assert.IsNotNull(secondGameTable.Player);
            Assert.IsNotNull(secondGameTable.GameDeck);
            Assert.AreSame(secondBot, secondGameTable.Player);
            Assert.AreEqual(secondGameTable.MinBet, minBet);

            // third case

            var thirdBot = new HiOptIIStrategyBot();
            var thirdGameTable = new GameTable(thirdBot, minBet);

            Assert.IsNotNull(thirdGameTable);
            Assert.IsNotNull(thirdGameTable.Dealer);
            Assert.IsNotNull(thirdGameTable.Player);
            Assert.IsNotNull(thirdGameTable.GameDeck);
            Assert.AreSame(thirdBot, thirdGameTable.Player);
            Assert.AreEqual(thirdGameTable.MinBet, minBet);
        }

        [Test]
        public void PlayWinningStrategyBotTest()
        {
            // preparation

            int minBet = 5;
            var bot = new WinningStrategyBot(20 * minBet);
            var gameTable = new GameTable(bot, minBet);
            int startAmountOfCards = gameTable.GameDeck.Cards.Count;

            // checking

            Assert.DoesNotThrow(() => gameTable.Play());
            Assert.IsTrue(gameTable.GameDeck.Cards.Count < startAmountOfCards);
        }

        [Test]
        public void PlayHiLowStrategyBotTest()
        {
            // preparation

            int minBet = 5;
            var bot = new HiLowStrategyBot(20 * minBet);
            var gameTable = new GameTable(bot, minBet);
            int startAmountOfCards = gameTable.GameDeck.Cards.Count;

            // checking

            Assert.DoesNotThrow(() => gameTable.Play());
            Assert.IsTrue(gameTable.GameDeck.Cards.Count < startAmountOfCards);
        }

        [Test]
        public void PlayHiOptIIStrategyBotTest()
        {
            // preparation

            int minBet = 5;
            var bot = new HiLowStrategyBot(20 * minBet);
            var gameTable = new GameTable(bot, minBet);
            int startAmountOfCards = gameTable.GameDeck.Cards.Count;

            // checking

            Assert.DoesNotThrow(() => gameTable.Play());
            Assert.IsTrue(gameTable.GameDeck.Cards.Count < startAmountOfCards);
        }

        [Test]
        public void PlayLongGameTest()
        {
            // preparation

            int minBet = 5;
            var bot = new HiLowStrategyBot(x => x > 200, 1000 * minBet);
            var gameTable = new GameTable(bot, minBet);
            int startAmountOfCards = gameTable.GameDeck.Cards.Count;

            // checking

            Assert.DoesNotThrow(() => gameTable.Play());
            Assert.IsTrue(gameTable.GameDeck.Cards.Count < startAmountOfCards);
        }
    }
}