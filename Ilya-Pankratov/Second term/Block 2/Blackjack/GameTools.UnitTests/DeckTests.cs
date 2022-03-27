using NUnit.Framework;
using System.Collections.Generic;

namespace GameTools.UnitTests
{
    public class DeckTests
    {
        [Test]
        public void ConstructorTest()
        {
            // preparation 

            int index = 0;
            var deck = new Deck();

            // checking amount of cards

            Assert.IsTrue(deck.Cards.Count == 52);

            // checking cards in deck

            for (int s = 0; s < 4; s++)
                for (int r = 1; r < 14; r++)
                {
                    var card = new Card(s, r);
                    Assert.IsTrue(AreCardsEqual(deck.Cards[index], card));
                    index++;
                }
            
        }

        [Test]
        public void TakeCardTest()
        {
            // preparation 

            var deck = new Deck();
            deck.Shuffle();

            // checking

            Card takenCard = deck.TakeCard();

            Assert.IsTrue(deck.Cards.Count == 51);

            foreach (Card card in deck.Cards)
            {
                Assert.IsFalse(AreCardsEqual(card, takenCard));
            }
        }

        [Test]
        public void TakeCardsTest()
        {
            // preparation 

            var deck = new Deck();
            deck.Shuffle();

            // checking

            int amountOfTakenCards = 5;
            List<Card> takenCards = deck.TakeCards(amountOfTakenCards);

            // checking amount of cards

            Assert.IsTrue(deck.Cards.Count == 52 - amountOfTakenCards);

            // checking that taken cards are not in the deck

            foreach (Card takenCard in takenCards)
            {
                foreach (Card card in deck.Cards)
                {
                    Assert.IsFalse(AreCardsEqual(card, takenCard));
                }
            }
            
        }

        private bool AreCardsEqual(Card firstCard, Card secondCard)
        {
            return firstCard.Suit == secondCard.Suit && firstCard.Rank == secondCard.Rank;
        }
    }
}