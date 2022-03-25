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
            goodBot.TookCard(new BlackJack.Card("H", "A"));
            Assert.AreEqual(goodBot.GetMove(), "double");
            goodBot.MakeBet(600);
            Assert.AreEqual(goodBot.GetMove(), "hit");
            goodBot.TookCard(new BlackJack.Card("H", "6"));
            Assert.AreEqual(goodBot.GetMove(), "hit");
            goodBot.TookCard(new BlackJack.Card("H", "10"));
            goodBot.TookCard(new BlackJack.Card("H", "10"));
            Assert.AreEqual(goodBot.GetMove(), "stand");

            Assert.Pass();
        }
    }
}
