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
            hand.Cards.Add(new Card(CardRank.Ace));
            hand.Cards.Add(new Card(CardRank.Five));
            hand.Cards.Add(new Card(CardRank.Three));
            Assert.AreEqual(19, hand.GetHandValue());
            hand.Cards.Add(new Card(CardRank.Eight));
            Assert.AreEqual(17, hand.GetHandValue());
            hand.Cards.Add(new Card(CardRank.Ace));
            Assert.AreEqual(18, hand.GetHandValue());
        }

        [Test]
        public void ContainsAceTest()
        {
            hand.Cards.Add(new Card(CardRank.Ace));
            hand.Cards.Add(new Card(CardRank.Two));
            Assert.IsTrue(hand.ContainsAce());
            hand.Cards.Add(new Card(CardRank.King));
            Assert.IsFalse(hand.ContainsAce());
            hand.Cards.Add(new Card(CardRank.Ace));
            Assert.IsFalse(hand.ContainsAce());
        }
    }
}