using NUnit.Framework;

namespace GameTable.UnitTests
{
    public class DealerTests
    {
        Dealer dealer = new Dealer();
        ToolKit.Shoe shoe = new ToolKit.Shoe();
        Bots.StandartBot bot = new Bots.StandartBot();

        [Test]
        public void CreateTest()
        {
            Dealer dealer = new Dealer();
            Assert.IsNotNull(dealer.Hand);
            Assert.AreEqual(0, dealer.Points);
        }

        [Test]
        public void DrawCardTest()
        {
            Dealer dealer = new Dealer();
            Assert.IsInstanceOf<ToolKit.Card>(dealer.DrawCard(shoe));
        }

        [Test]
        public void DrawCardToPlayerTest()
        {
            Dealer dealer = new Dealer();
            ToolKit.Hand hand = new ToolKit.Hand();
            dealer.DrawCardToPlayer(shoe, hand);

            Assert.AreEqual(1, hand.Cards.Count);
        }

        [Test]
        public void StartGameTest()
        {
            Start();

            Assert.AreEqual(2, bot.Hands[0].Cards.Count);
            Assert.AreEqual(2, dealer.Hand.Count);
        }

        [Test]
        public void PlayTest()
        {
            Start();
            dealer.Play(shoe);
            Assert.IsTrue(dealer.Points >= 17);
        }

        [Test]
        public void UpdateShoeTest()
        {
            Dealer dealer = new Dealer();
            Assert.IsFalse(dealer.UpdateShoe(ref shoe, 8));

            for (int i = 0; i < 7 * 52; i++)
            {
                dealer.DrawCard(shoe);
            }

            Assert.IsTrue(dealer.UpdateShoe(ref shoe, 8));
        }

        [Test]
        public void ResetTableTest()
        {
            Start();
            dealer.Play(shoe);
            bot.Hands.Add(new ToolKit.Hand());

            dealer.ResetTable(bot);
            Assert.AreEqual(1, bot.Hands.Count);
            Assert.AreEqual(0, bot.Hands[0].Cards.Count);
            Assert.AreEqual(0, dealer.Hand.Count);
            Assert.AreEqual(0, dealer.Points);
        }

        private void Start()
        {
            dealer.ResetTable(bot);
            dealer.StartGame(shoe, bot);
        }
    }
}
