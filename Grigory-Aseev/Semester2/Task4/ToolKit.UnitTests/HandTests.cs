using NUnit.Framework;

namespace ToolKit.UnitTests
{
    public class HandTests
    {
        [Test]
        public void CreateTest()
        {
            Hand hand = new Hand();
            Assert.AreEqual(0, hand.Bet);
            Assert.AreEqual(0, hand.Points);
            Assert.AreEqual(GamingState.Playing, hand.State);
            Assert.IsNotNull(hand.Cards);

            hand = new Hand(1000 - 7);
            Assert.AreEqual(1000 - 7, hand.Bet);
        }

        [Test]
        public void UpdateScoreTest()
        {
            Hand hand = new Hand();
            hand.Cards.Add(new Card(Suits.Heart, CardPoints.Ace));
            hand.UpdateScore();
            Assert.AreEqual(11, hand.Points);

            hand.Cards.Add(new Card(Suits.Heart, CardPoints.Ace));
            hand.UpdateScore();
            Assert.AreEqual(12, hand.Points);
        }
    }
}