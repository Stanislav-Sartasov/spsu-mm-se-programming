using NUnit.Framework;

namespace BlackJack.UnitTests
{
    public class CardTests
    {
        [Test]
        public void GetValueTest()
        {
            Card card = new Card("H", "A");
            Assert.AreEqual(card.GetValue(), 1);
            card = new Card("H", "9");
            Assert.AreEqual(card.GetValue(), 9);
            card = new Card("H", "Q");
            Assert.AreEqual(card.GetValue(), 10);

            Assert.Pass();
        }

        [Test]
        public void EqualityTest()
        {
            Card leftCard = new Card("H", "A");
            Card rightCard = new Card("H", "A");
            Assert.IsTrue(leftCard == rightCard);

            Assert.Pass();
        }

        [Test]
        public void InequalityTest()
        {
            Card leftCard = new Card("H", "A");
            Card rightCard = new Card("D", "A");
            Assert.IsTrue(leftCard != rightCard);

            Assert.Pass();
        }
    }
}