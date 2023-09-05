using Cards;
using Casino;
using NUnit.Framework;

namespace CasinoTests
{
    public class CroupierTests
    {
        [Test]
        public void CroupierPlayTest()
        {
            var croupier = new Croupier();
            var shoes = new Shoes();
            croupier.Play(shoes);
            Assert.IsTrue(croupier.HandValue >= 17);
            Assert.AreEqual(416 - croupier.Hand.Count, shoes.AllCardsCount);
        }
    }
}