using NUnit.Framework;

namespace GameTools.UnitTests
{
    public class HandTests
    {
        [Test]
        public void ConstructorTest()
        {
            var hand = new Hand();

            Assert.IsTrue(hand != null);
            Assert.IsTrue(hand.Points == 0);
            Assert.IsTrue(hand.Bet == 0);
            Assert.IsTrue(hand.Flag == HandState.Playing);
            Assert.IsTrue(hand.Cards != null);
        }

        [Test]
        public void RecountPointsTest()
        {
            // preparation  for checking first situation

            var hand = new Hand();
            hand.Cards.Add(new Card (CardSuit.Diamond, CardRank.Ten));
            hand.Cards.Add(new Card (CardSuit.Diamond, CardRank.Ace));
            hand.RecountPoints();

            // cheking the first situation.
            // the player has 21 points, then he takes one ace, therefore,
            // he should has 12 points

            hand.Cards.Add(new Card(CardSuit.Diamond, CardRank.Ace));
            hand.RecountPoints();
            Assert.IsTrue(hand.Points == 12);

            // preparation  for checking second situation

            hand.Cards.Clear();
            hand.Cards.Add(new Card(CardSuit.Diamond, CardRank.Ace));
            var removingCard = new Card(CardSuit.Diamond, CardRank.Two);
            hand.Cards.Add(removingCard);
            hand.RecountPoints();

            // cheking the second situation.
            // the player has 13 points, then he split his hand and there is ace in one of
            // the hand. He should has 11 points

            hand.Cards.Remove(removingCard);
            hand.RecountPoints();
            Assert.IsTrue(hand.Points == 11);
        }
    }
}
