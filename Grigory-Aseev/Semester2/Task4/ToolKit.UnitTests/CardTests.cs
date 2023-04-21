using NUnit.Framework;

namespace ToolKit.UnitTests
{
    public class CardTests
    {
        [Test]
        public void CreateCardTest()
        {
            for (int i = 1; i < 5; i++)
            {
                for (int j = 1; j < 14; j++)
                {
                    Card card = new Card((Suits)i, (CardPoints)j);
                    Assert.AreEqual(card.Suit, (Suits)i);
                    Assert.AreEqual(card.CardPoint, (CardPoints)j);
                }
            }
        }

        [Test]
        public void GetPointsWithoutParameterTest()
        {
            Card card;

            for (int i = 2; i < 11; i++)
            {
                card = new Card((Suits)0, (CardPoints)i);
                Assert.AreEqual(card.GetPoints(), i);
            }

            for (int i = 11; i < 14; i++)
            {
                card = new Card((Suits)0, (CardPoints)i);
                Assert.AreEqual(card.GetPoints(), 10);
            }

            card = new Card((Suits)0, (CardPoints)1);
            Assert.AreEqual(card.GetPoints(), 11);
        }

        [Test]
        public void GetPointsAceTest()
        {
            Card card = new Card(Suits.Heart, CardPoints.Ace);
            Assert.AreEqual(card.GetPoints(), 11);
            Assert.AreEqual(card.GetPoints(11), 1);
        }
    }
}