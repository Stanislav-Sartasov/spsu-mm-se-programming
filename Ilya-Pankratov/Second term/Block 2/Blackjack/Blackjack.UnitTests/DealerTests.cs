using NUnit.Framework;
using GameTools;
using Bots;

namespace Blackjack.UnitTests
{
    public class DealerTests
    {
        private const int numberOfDecks = 8;

        [Test]
        public void ConstructorTest()
        {
            // preparation 

            var dealer = new Dealer();

            // checking

            Assert.IsNotNull(dealer);
            Assert.AreEqual(dealer.Points, 0);
            Assert.IsNotNull(dealer.Cards);
        }

        [Test]
        public void DrawCardTest()
        {
            // preparation 

            var dealer = new Dealer();
            var gameDeck = new GameDeck(numberOfDecks);

            // checking

            Assert.IsNotNull(dealer.DrawCard(gameDeck));
        }

        [Test]
        public void DrawCardsTest()
        {
            // preparation

            var dealer = new Dealer();
            var gameDeck = new GameDeck(numberOfDecks);
            var numberOfCards = 2;

            // checking 

            Assert.IsNotNull(dealer.DrawCards(gameDeck, numberOfCards));
        }

        [Test]
        public void DrawCardsToHandTest()
        {
            // preparation

            var dealer = new Dealer();
            var gameDeck = new GameDeck(numberOfDecks);
            var playerHand = new Hand();

            // checking 

            dealer.DrawCardToHand(gameDeck, playerHand);
            Assert.IsNotNull(playerHand.Cards);
            Assert.IsTrue(playerHand.Cards.Count == 1);
        }

        [Test]
        public void DrawTwoCardsToAllTest()
        { 
            // preparation 

            var dealer = new Dealer();
            var gameDeck = new GameDeck(numberOfDecks);
            var bot = new WinningStrategyBot();

            // checking 

            dealer.DrawTwoCardsToAll(gameDeck, bot);

            Assert.IsNotNull(bot.Hands[0].Cards);
            Assert.IsTrue(bot.Hands[0].Cards.Count == 2);

            Assert.IsNotNull(dealer.Cards);
            Assert.IsTrue(dealer.Cards.Count == 2);
        }

        [Test]
        public void PlayTest()
        {
            // preparation 

            var dealer = new Dealer();
            var gameDeck = new GameDeck(numberOfDecks);
            var bot = new WinningStrategyBot();

            dealer.DrawTwoCardsToAll(gameDeck, bot);
            int dealerPoints = dealer.Points;
            dealer.Play(gameDeck);

            // checking 

            Assert.IsTrue(dealer.Cards.Count >= 2);
            Assert.IsTrue(dealer.Points > dealerPoints);
            Assert.IsTrue(dealer.Cards[1].Flag == Visibility.Visible);
        }

        [Test]
        public void TakeCardsOnTheTabbleTest()
        {
            // preparation 

            var dealer = new Dealer();
            var gameDeck = new GameDeck(numberOfDecks);
            var bot = new WinningStrategyBot();
            dealer.DrawTwoCardsToAll(gameDeck, bot);

            // checking 

            dealer.TakeCardsOnTheTable(bot);
            Assert.IsTrue(dealer.Cards.Count == 0);
            Assert.IsTrue(bot.Hands[0].Cards.Count == 0);
        }

        [Test]
        public void ResetGameDeckTest()
        {
            // preparation 

            var dealer = new Dealer();
            var gameDeck = new GameDeck(numberOfDecks);
            var oldCards = gameDeck.Cards;

            // checking 

            dealer.ResetGameDeck(gameDeck);
            Assert.AreNotSame(oldCards, gameDeck.Cards);
        }

        [Test]
        public void CheckAmountOfCardsTest()
        {
            // preparation 

            var dealer = new Dealer();
            var gameDeck = new GameDeck(numberOfDecks);
            int totalCards = 52 * 8;
            int amountOfRemovingCards = totalCards / 4 * 3;

            // checking 

            Assert.IsFalse(dealer.CheckAmountOfCards(gameDeck, totalCards));

            gameDeck.Cards.RemoveRange(totalCards - amountOfRemovingCards - 1, amountOfRemovingCards);
            Assert.IsTrue(dealer.CheckAmountOfCards(gameDeck, totalCards));
        }
    }
}