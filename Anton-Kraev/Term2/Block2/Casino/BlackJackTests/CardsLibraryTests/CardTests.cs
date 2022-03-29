using Cards;
using NUnit.Framework;

namespace CardsLibraryTests
{
    public class CardTests
    {
        [Test]
        public void GetCardValueTest()
        {
            var ace = new Card("A");
            var ten = new Card("10");
            var unknown = new Card("X");
            Assert.AreEqual(0, unknown.GetCardValue());
            Assert.AreEqual(11, ace.GetCardValue());
            Assert.AreEqual(10, ten.GetCardValue());
        }
    }
}