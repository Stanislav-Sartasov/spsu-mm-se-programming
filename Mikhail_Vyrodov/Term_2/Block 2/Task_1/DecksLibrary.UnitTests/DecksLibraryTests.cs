using NUnit.Framework;

namespace DecksLibrary.UnitTests
{
    public class DecksLibraryTests
    {
        [Test]
        public void FillCardsTest()
        {
            uint[] cardsArr = new uint[10];
            byte[] playingCards = new byte[416];
            Decks playingDecks = new Decks();
            playingDecks.FillCards();
            playingCards = playingDecks.Cards;
            for (int i = 0; i < 416; i++)
            {
                cardsArr[playingCards[i] - 1] += 1;
            }
            for (int i = 0; i < 9; i++)
            {
                Assert.AreEqual(cardsArr[i], 32); // number of cards whose score is 2...9 and number of aces
            }
            Assert.AreEqual(cardsArr[9], 128); // number of cards whose score is 10
            Assert.Pass();
        }

        [Test]
        public void GetCardTest()
        {
            Decks playingDecks = new Decks();
            byte[] playingCards = new byte[416];
            byte[] cardsAfterTest = new byte[416];
            playingDecks.FillCards();
            for (int i = 0; i < 416; i++)
            {
                playingCards[i] = playingDecks.Cards[i];
            }
            byte firstCard = 0, secondCard = 0, thirdCard = 0;
            firstCard = playingCards[0];
            secondCard = playingCards[1];
            thirdCard = playingCards[2];
            for (int i = 0; i < 3; i++)
            {
                playingCards[i] = 0;
            }
            byte functionFirstValue = playingDecks.GetCard();
            byte functionSecondValue = playingDecks.GetCard();
            byte functionThirdValue = playingDecks.GetCard();
            Assert.AreEqual(firstCard, functionFirstValue);
            Assert.AreEqual(secondCard, functionSecondValue);
            Assert.AreEqual(thirdCard, functionThirdValue);
            for (int i = 0; i < 416; i++)
            {
                cardsAfterTest[i] = playingDecks.Cards[i];
            }
            for (int i = 0; i < 416; i++)
            {
                Assert.AreEqual(playingCards[i], cardsAfterTest[i]);
            }
        }

        [Test]
        public void CountCardsTest()
        {
            // I use Halves strategy of counting cards from wikipedia "Card counting" article
            byte[] cardArr = new byte[5] { 1, 2, 3, 8, 10 };
            double[] countOfCards = new double[5] { -1, 0.5, 1, 0, -1 };
            Decks playingDecks = new Decks();
            for (int i = 0; i < 5; i++)
            {
                Assert.AreEqual(countOfCards[i], playingDecks.CountCards(cardArr[i]));
            }
        }
    }
}