using NUnit.Framework;

namespace BlackJack.UnitTests
{
    public class CardTests
    {
        [Test]
        public void GetValueTest()
        {
            Card card = new Card(0, 1);
            Assert.AreEqual(card.GetValue(), 1);
            card = new Card(0, 9);
            Assert.AreEqual(card.GetValue(), 9);
            card = new Card(0, 12);
            Assert.AreEqual(card.GetValue(), 10);

            Assert.Pass();
        }

        [Test]
        public void EqualityTest()
        {
            Card leftCard = new Card(0, 1);
            Card rightCard = new Card(0, 1);
            Assert.IsTrue(leftCard == rightCard);

            Assert.Pass();
        }

        [Test]
        public void InequalityTest()
        {
            Card leftCard = new Card(0, 1);
            Card rightCard = new Card(1, 1);
            Assert.IsTrue(leftCard != rightCard);

            Assert.Pass();
        }
    }
}