using NUnit.Framework;
using BlackJackEnumerations;

namespace Bots.UnitTests
{
    public class DoubleBotTests
    {
        [Test]
        public void GetNewBetTest()
        {
            DoubleBot doubleBot = new DoubleBot("SomeName", 1000);
            Assert.AreEqual(doubleBot.GetNewBet(), 100);
            doubleBot.MakeBet(100);
            doubleBot.TookCard(new BlackJack.Card(0, 10));
            doubleBot.TookCard(new BlackJack.Card(0, 10));
            doubleBot.GetMove();
            doubleBot.MakeBet(100);
            Assert.AreEqual(doubleBot.GetNewBet(), 200);

            Assert.Pass();
        }

        [Test]
        public void GetMoveTest()
        {
            DoubleBot doubleBot = new DoubleBot("SomeName", 1000);
            doubleBot.TookCard(new BlackJack.Card(0, 10));
            Assert.AreEqual(doubleBot.GetMove(), PlayerMove.Hit);
            doubleBot.TookCard(new BlackJack.Card(0, 10));
            Assert.AreEqual(doubleBot.GetMove(), PlayerMove.Stand);

            Assert.Pass();
        }
    }
}