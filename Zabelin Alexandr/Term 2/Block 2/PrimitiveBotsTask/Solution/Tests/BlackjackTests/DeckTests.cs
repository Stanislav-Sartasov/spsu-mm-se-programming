using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blackjack;

namespace BlackJackTests
{
    [TestClass]
    public class DeckTests
    {
        [TestMethod]
        public void Length()
        {
            byte expectedLen = 10;

            Deck deck = new Deck(8);

            Assert.AreEqual(expectedLen, deck.Length);
        }

        [TestMethod]
        public void TakeCardInitTest()
        {
            byte expected = 10;

            Deck deck = new Deck(2);
            deck.TakeCard();
            byte actual = deck.Length;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TakeCardTest()
        {
            bool expected = true;

            Deck deck = new Deck(1);
            for (int i = 0; i < 4; i++)
            {
                deck.TakeCard();
            }

            bool actual = deck.Length < 10;

            Assert.AreEqual(expected, actual);
        }
    }
}
