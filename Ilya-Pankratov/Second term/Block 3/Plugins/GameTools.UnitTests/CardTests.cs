using NUnit.Framework;

namespace GameTools.UnitTests
{
    public class CardTests
    {
        [Test]
        public void IntConstructorTest()
        {
            for (int s = 0; s < 4; s++)
                for (int r = 1; r < 14; r ++)
                {
                    var card = new Card(s, r);
                    Assert.IsTrue(card.Suit == (CardSuit)s && card.Rank == (CardRank)r);
                }
        }

        [Test]
        public void ConstructorTest()
        {
            for (int s = 0; s < 4; s++)
                for (int r = 1; r < 14; r++)
                {
                    var cardSuit = (CardSuit)s;
                    var cardRank= (CardRank)r;
                    var card = new Card(cardSuit, cardRank);
                    Assert.IsTrue(card.Suit == cardSuit && card.Rank == cardRank);
                }
        }

        [Test]
        public void GetPointsTest()
        {
            // preparation

            int playerPoints = 0;

            // checking when playerpoints = 0

            for (int s = 0; s < 4; s++)
                for (int r = 1; r < 14; r++)
                {
                    var card = new Card(s, r);
                    int cardPoints;

                    if (r == 1)
                    {
                        cardPoints = 11;
                    }
                    else if (r < 10)
                    {
                        cardPoints = r;
                    }
                    else
                    {
                        cardPoints = 10;
                    }

                    Assert.IsTrue(card.GetPoints(playerPoints) == cardPoints);
                }

            // checking ace points when playerpoints > 10

            playerPoints = 11;

            for (int s = 0; s < 4; s++)
                for (; playerPoints < 22; playerPoints++)
                {
                    var card = new Card(s, 1);
                    Assert.IsTrue(card.GetPoints(playerPoints) == 1);
                }
        }

    }
}