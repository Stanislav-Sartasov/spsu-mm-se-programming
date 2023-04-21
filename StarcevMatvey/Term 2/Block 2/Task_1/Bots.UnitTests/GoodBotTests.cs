using NUnit.Framework;

namespace Bots.UnitTests
{
    public class GoodBotTests
    {
        [Test]
        public void GetNewBetTest()
        {
            GoodBot goodBot = new GoodBot("SomeName", 1000);
            Assert.AreEqual(goodBot.GetNewBet(), 100);

            Assert.Pass();
        }

        [Test]
        public void GetMoveTest()
        {
            GoodBot goodBot = new GoodBot("SomeName", 1000);
            goodBot.TookCard(new BlackJack.Card(0, 1));
            Assert.AreEqual(goodBot.GetMove(), BlackJack.PlayerMove.Double);
            goodBot.MakeBet(600);
            Assert.AreEqual(goodBot.GetMove(), BlackJack.PlayerMove.Hit);
            goodBot.TookCard(new BlackJack.Card(0, 6));
            Assert.AreEqual(goodBot.GetMove(), BlackJack.PlayerMove.Hit);
            goodBot.TookCard(new BlackJack.Card(0, 10));
            goodBot.TookCard(new BlackJack.Card(0, 10));
            Assert.AreEqual(goodBot.GetMove(), BlackJack.PlayerMove.Stand);

            Assert.Pass();
        }
    }
}
