using NUnit.Framework;

namespace BlackJack.UnitTests
{
    public class PlayerTests
    {
        [Test]
        public void TookCardTest()
        {
            Player player = new Player("SomeName", 1000);
            player.TookCard(new Card(0, 1));
            Assert.AreEqual(player.Cards.Count, 1);

            Assert.Pass();
        }

        [Test]
        public void GetNewHandTest()
        {
            Player player = new Player("SomeName", 1000);
            player.GetNewHand(new ShuffleMachine());
            Assert.AreEqual(player.Cards.Count, 2);

            Assert.Pass();
        }

        [Test]
        public void GetScoreTest()
        {
            Player player = new Player("SomeName", 1000);
            player.TookCard(new Card(0, 1));
            player.TookCard(new Card(0, 5));
            Assert.AreEqual(player.GetScore(), 16);
            player.TookCard(new Card(0, 10));
            Assert.AreEqual(player.GetScore(), 16);
            player.TookCard(new Card(0, 10));
            Assert.AreEqual(player.GetScore(), 0);

            Assert.Pass();
        }

        public void MakeBetTest()
        {
            Player player = new Player("SomeName", 1000);
            player.MakeBet(100);
            Assert.AreEqual(player.Balance, 900);
            Assert.AreEqual(player.Bet, 100);
            player.MakeBet(1100);
            Assert.AreEqual(player.Balance, 0);
            Assert.AreEqual(player.Bet, 1000);

            Assert.Pass();
        }

        [Test]
        public void DoubleTest()
        {
            Player player = new Player("SomeName", 1000);
            player.MakeBet(100);
            player.Double(new Card(0, 1));
            Assert.AreEqual(player.Bet, 200);
            Assert.AreEqual(player.Cards.Count, 1);

            Assert.Pass();
        }

        [Test]
        public void LoseBetTest()
        {
            Player player = new Player("SomeName", 1000);
            player.MakeBet(100);
            player.LoseBet();
            Assert.AreEqual(player.Bet, 0);

            Assert.Pass();
        }

        [Test]
        public void TakeNormalBetTest()
        {
            Player player = new Player("SomeName", 1000);
            player.MakeBet(100);
            player.TakeNormalBet();
            Assert.AreEqual(player.Balance, 1100);
            Assert.AreEqual(player.Bet, 0);

            Assert.Pass();
        }

        [Test]
        public void TakeBlackJackBetTest()
        {
            Player player = new Player("SomeName", 1000);
            player.MakeBet(100);
            player.TakeBlackJackBet();
            Assert.AreEqual(player.Balance, 1150);
            Assert.AreEqual(player.Bet, 0);

            Assert.Pass();
        }

        [Test]
        public void TakeBetBackTest()
        {
            Player player = new Player("SomeName", 1000);
            player.MakeBet(100);
            player.TakeBetBack();
            Assert.AreEqual(player.Balance, 1000);
            Assert.AreEqual(player.Bet, 0);

            Assert.Pass();
        }
    }
}
