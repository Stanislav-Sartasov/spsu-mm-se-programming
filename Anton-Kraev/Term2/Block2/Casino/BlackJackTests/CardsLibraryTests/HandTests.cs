using Cards;
using NUnit.Framework;

namespace CardsLibraryTests
{
    public class HandTests
    {
        private Hand hand;

        [SetUp]
        public void Setup()
        {
            hand = new Hand(100);
        }

        [Test]
        public void GetHandValueTests()
        {
            hand.Cards.Add(new Card("A"));
            hand.Cards.Add(new Card("5"));
            hand.Cards.Add(new Card("3"));
            Assert.AreEqual(19, hand.GetHandValue());
            hand.Cards.Add(new Card("8"));
            Assert.AreEqual(17, hand.GetHandValue());
            hand.Cards.Add(new Card("A"));
            Assert.AreEqual(18, hand.GetHandValue());
        }

        [Test]
        public void ContainsAceTest()
        {
            hand.Cards.Add(new Card("A"));
            hand.Cards.Add(new Card("2"));
            Assert.IsTrue(hand.ContainsAce());
            hand.Cards.Add(new Card("10"));
            Assert.IsFalse(hand.ContainsAce());
            hand.Cards.Add(new Card("A"));
            Assert.IsFalse(hand.ContainsAce());
        }
    }
}