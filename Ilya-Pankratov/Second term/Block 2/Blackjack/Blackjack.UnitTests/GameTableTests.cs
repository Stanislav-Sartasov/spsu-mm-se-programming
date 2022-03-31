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
            var bot = new WinningStrategyBot();
            var firstGameTable = new GameTable(bot, minBet);

            Assert.IsNotNull(firstGameTable);
            Assert.IsNotNull(firstGameTable.Dealer);
            Assert.IsNotNull(firstGameTable.Player);
            Assert.IsNotNull(firstGameTable.GameDeck);
            Assert.AreSame(bot, firstGameTable.Player);
            Assert.AreEqual(firstGameTable.MinBet, minBet);

            // second case

            var secondGameTable = new GameTable(BotName.HiOptIIStrategyBot, minBet);

            Assert.IsNotNull(secondGameTable);
            Assert.IsNotNull(secondGameTable.Dealer);
            Assert.IsNotNull(secondGameTable.Player);
            Assert.IsNotNull(secondGameTable.GameDeck);
            Assert.AreEqual(secondGameTable.MinBet, minBet);

            // third case

            var thirdGameTable = new GameTable(BotName.WinningStrategyBot, minBet);

            Assert.IsNotNull(thirdGameTable);
            Assert.IsNotNull(thirdGameTable.Dealer);
            Assert.IsNotNull(thirdGameTable.Player);
            Assert.IsNotNull(thirdGameTable.GameDeck);
            Assert.AreEqual(thirdGameTable.MinBet, minBet);

            // fourth case

            var fourthGameTable = new GameTable(BotName.HiLowStrategyBot, minBet);

            Assert.IsNotNull(fourthGameTable);
            Assert.IsNotNull(fourthGameTable.Dealer);
            Assert.IsNotNull(fourthGameTable.Player);
            Assert.IsNotNull(fourthGameTable.GameDeck);
            Assert.AreEqual(fourthGameTable.MinBet, minBet);
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