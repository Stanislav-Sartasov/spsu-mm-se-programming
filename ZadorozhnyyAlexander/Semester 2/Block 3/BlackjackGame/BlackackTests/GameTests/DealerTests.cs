using System.Linq;
using NUnit.Framework;
using BlackjackMechanics.Players;
using BlackjackMechanics.Cards;
using UsualBaseStrategyBotLibrary;
using AbstractClasses;

namespace BlackackTests.GameTests
{
    public class DealerTests
    {
        private const int CountDecksInOne = 8;

        [Test]
        public void DealerConstructorTest()
        {
            var dealer = new Dealer();

            Assert.IsNotNull(dealer);
            Assert.IsNotNull(dealer.CardsInHand);
            Assert.AreEqual(dealer.GetSumOfCards(), 0);
        }

        [Test]
        public void DealerTakeCardTest()
        {
            var dealer = new Dealer();
            var card = new AceCard(CardSuits.Club);
            dealer.TakeCard(card);

            Assert.AreEqual(dealer.CardsInHand[0], card);
        }

        [Test]
        public void DealerHandOutCardsTest()
        {
            var dealer = new Dealer();
            var bot = new UsualBaseStrategyBot(10, 1);
            var gameDeck = new DeckOfCards(CountDecksInOne);

            dealer.HandOutCards(gameDeck, bot);
            Assert.IsNotNull(bot.CardsInHand);
            Assert.IsTrue(bot.CardsInHand.Count() == 2);
        }

        [Test]
        public void DealerGamePlayTest()
        {
            var dealer = new Dealer();

            dealer.TakeCard(new UsualCard(CardNames.Five, CardSuits.Diamond));
            dealer.TakeCard(new UsualCard(CardNames.Five, CardSuits.Diamond));

            int dealerStartCardCount = dealer.CardsInHand.Count();

            // game simulation 
            while (dealer.GetNextCard())
                dealer.TakeCard(new UsualCard(CardNames.Two, CardSuits.Diamond));

            Assert.IsTrue(dealer.CardsInHand.Count > 2);
            Assert.IsTrue(dealer.CardsInHand.Count() > dealerStartCardCount);
            Assert.IsNotNull(dealer.VisibleCard);
        }
    }
}