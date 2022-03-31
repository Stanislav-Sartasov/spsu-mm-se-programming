using Cards;
using NUnit.Framework;

namespace CardsLibraryTests
{
    public class CardTests
    {
        [Test]
        public void GetCardValueTest()
        {
            var ace = new Card(CardRank.Ace);
            var six = new Card(CardRank.Six);
            var jack = new Card(CardRank.Jack);
            Assert.AreEqual(6, six.GetCardRank());
            Assert.AreEqual(11, ace.GetCardRank());
            Assert.AreEqual(10, jack.GetCardRank());
        }
    }
}