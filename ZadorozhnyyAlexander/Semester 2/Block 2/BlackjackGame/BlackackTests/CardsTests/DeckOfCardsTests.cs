using System.Linq;
using BlackjackMechanics.Players;
using BlackjackMechanics.Cards;
using AbstractClasses;
using NUnit.Framework;

namespace BlackackTests.CardsTests
{
    public class DeckOfCardsTests
    {
        private const int countDecksInOne = 1;

        private bool CheckIsCardsEqual(ACard first, ACard second)
        {
            return first.CardSuit == second.CardSuit && first.CardName == second.CardName;
        }

        private bool CheckIsCardInDeck(DeckOfCards deck, ACard searchedCard)
        {
            foreach (var card in deck.Deck)
            {
                if (CheckIsCardsEqual(card, searchedCard))
                    return true;
            }
            return false;
        }

        [Test]
        public void DeckOfCardsConstructorTest()
        {
            var deck = new DeckOfCards(countDecksInOne);

            Assert.IsNotNull(deck);
            Assert.IsTrue(deck.Deck.Count() == 52);

            for (int indexSuit = 0; indexSuit < 4; indexSuit++)
            {
                for (int indexName = 0; indexName < 9; indexName++)
                { 
                    var numberCard = new UsualCard((CardNames)indexName, (CardSuits)indexSuit);

                    Assert.IsTrue(CheckIsCardInDeck(deck, numberCard));
                }

                for (int indexName = 9; indexName < 12; indexName++)
                {
                    var faceCard = new UsualCard((CardNames)indexName, (CardSuits)indexSuit);

                    Assert.IsTrue(CheckIsCardInDeck(deck, faceCard));
                }

                var aceCard = new AceCard((CardSuits)indexSuit);
                Assert.IsTrue(CheckIsCardInDeck(deck, aceCard));
            }
        }

        [Test]
        public void TakeCardTest()
        {
            var dealer = new Dealer();
            var deck = new DeckOfCards(countDecksInOne);
            deck.ShuffleDeck();

            ACard takenCard = deck.GetOneCard(dealer);

            Assert.IsTrue(deck.Deck.Count() == 51);

            Assert.IsFalse(CheckIsCardInDeck(deck, takenCard));
        }

        [Test]
        public void ResetDeckOfCardsTest()
        {
            var dealer = new Dealer();
            var deck = new DeckOfCards(countDecksInOne);

            var changingDeck = new DeckOfCards(countDecksInOne);
            changingDeck.ShuffleDeck();
            changingDeck.GetOneCard(dealer);
            changingDeck.GetOneCard(dealer);

            Assert.IsFalse(deck.Deck.Count() == changingDeck.Deck.Count());

            changingDeck.ResetDeckOfCards();
            Assert.IsTrue(deck.Deck.Count() == changingDeck.Deck.Count());

            for (int i = 0; i < deck.Deck.Count(); i++)
                Assert.IsTrue(CheckIsCardsEqual(deck.Deck[i], changingDeck.Deck[i]));
        }
    }
}
