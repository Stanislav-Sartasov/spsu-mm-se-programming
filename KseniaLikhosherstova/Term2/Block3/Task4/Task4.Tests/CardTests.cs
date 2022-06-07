using Game.Cards;



namespace Task4.Tests
{
    public class CardTests
    {
        [Test]
        public void RankIsWrongTestl()
        {
            int wrongRank = 42;

            Assert.IsFalse(Enum.IsDefined((Rank)wrongRank));

            Assert.Throws<ArgumentException>(() =>
            {
                new Card((Rank)wrongRank, Suit.Clubs);
            });

            Assert.Pass();
        }


        [Test]

        public void SuitIsWrongTest()
        {
            int wrongSuit = 11;
            Assert.IsFalse(Enum.IsDefined((Suit)wrongSuit));

            Assert.Throws<ArgumentException>(() =>
            {
                new Card(Rank.Three, (Suit)wrongSuit);
            });

            Assert.Pass();
        }


        [Test]
        public void GetValueOfAceTest()
        {
            Card card = new Card(Rank.Ace, Suit.Clubs);

            var valueOfAce = card.GetValueOfCard(11);

            Assert.That(valueOfAce, Is.EqualTo(1));

            Assert.Pass();
        }


        [Test]
        public void GetValueTest()
        {
            Card card = new Card(Rank.Five, Suit.Diamonds);

            Assert.That(card.GetValueOfCard(12), Is.EqualTo(5));

            Assert.Pass();
        }


        [Test]
        public void ToStringTest()
        {
            var card = new Card(Rank.Queen, Suit.Clubs);
            Assert.That(card.ToString(), Is.EqualTo("Queen of clubs"));

            Assert.Pass();
        }
    }
}