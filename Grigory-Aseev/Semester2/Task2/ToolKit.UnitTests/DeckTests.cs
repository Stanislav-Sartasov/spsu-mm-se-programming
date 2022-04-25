using NUnit.Framework;

namespace ToolKit.UnitTests
{
    public class DeckTests
    {
        [Test]
        public void CreateDeckTest()
        {
            Deck deck = new Deck();
            Assert.AreEqual(deck.CountOfCards, 52);

            for (int i = 1; i < 5; i++)
            {
                for (int j = 1; j < 14; j++)
                {
                    Card card = deck.TakeCard();
                    Assert.AreEqual((Suits)i, card.Suit);
                    Assert.AreEqual(card.CardPoint, (CardPoints)j);
                }
            }
        }

        [Test]
        public void TakeCardTest()
        {
            Deck deck = new Deck();

            for (int i = 0; i < 52; i++)
            {
                deck.TakeCard();
                Assert.AreEqual(deck.CountOfCards, 51 - i);
            }

            Assert.Catch<System.ArgumentException>(() => deck.TakeCard());
        }

        [Test]
        public void TakeCardsTest()
        {
            Deck deck = new Deck();

            Assert.Catch<System.ArgumentOutOfRangeException>(() => deck.TakeCards(0));

            Assert.Catch<System.ArgumentException>(() => deck.TakeCards(53));
        }
    }
}