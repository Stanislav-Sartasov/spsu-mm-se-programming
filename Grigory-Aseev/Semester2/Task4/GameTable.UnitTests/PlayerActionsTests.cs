using NUnit.Framework;

namespace GameTable.UnitTests
{
    public class PlayerActionsTests
    {
        Dealer dealer = new Dealer();
        ToolKit.Shoe shoe = new ToolKit.Shoe();

        [Test]
        public void StandTest()
        {
            ToolKit.Hand hand = new ToolKit.Hand();
            PlayerActions.Stand(hand);
            Assert.AreEqual(ToolKit.GamingState.Stand, hand.State);
        }

        [Test]
        public void SurrenderTest()
        {
            ToolKit.Hand hand = new ToolKit.Hand();
            PlayerActions.Surrender(hand);
            Assert.AreEqual(ToolKit.GamingState.Surrender, hand.State);
        }

        [Test]
        public void HitTest()
        {
            ToolKit.Hand hand = new ToolKit.Hand();
            PlayerActions.Hit(hand, dealer, shoe);
            Assert.AreEqual(1, hand.Cards.Count);
            Assert.AreEqual(ToolKit.GamingState.Hit, hand.State);
        }

        [Test]
        public void DoubleDownTest()
        {
            ToolKit.Hand hand = new ToolKit.Hand();
            Bots.StandartBot bot = new Bots.StandartBot();
            hand.Bet = 666666;

            PlayerActions.DoubleDown(bot, hand, dealer, shoe);
            Assert.AreEqual(ToolKit.GamingState.Hit, hand.State);

            hand.Bet = 100;

            PlayerActions.DoubleDown(bot, hand, dealer, shoe);
            Assert.AreEqual(ToolKit.GamingState.Stand, hand.State);
            Assert.AreEqual(200, hand.Bet);
            Assert.AreEqual(10000 - 100, bot.Balance);
        }

        [Test]
        public void SplitTest()
        {
            ToolKit.Card card = new ToolKit.Card(ToolKit.Suits.Heart, ToolKit.CardPoints.Two);
            Bots.StandartBot bot = new Bots.StandartBot();
            bot.Hands[0].Cards.Add(card);
            bot.Hands[0].Cards.Add(card);

            PlayerActions.Split(bot, bot.Hands[0], shoe, dealer);

            Assert.AreEqual(2, bot.Hands.Count);
            Assert.AreEqual(2, bot.Hands[0].Cards.Count);
            Assert.AreEqual(2, bot.Hands[1].Cards.Count);
            Assert.AreEqual(card.Suit, bot.Hands[0].Cards[0].Suit);
            Assert.AreEqual(card.Suit, bot.Hands[1].Cards[0].Suit);
            Assert.AreEqual(card.CardPoint, bot.Hands[0].Cards[0].CardPoint);
            Assert.AreEqual(card.CardPoint, bot.Hands[1].Cards[0].CardPoint);
            Assert.AreEqual(ToolKit.GamingState.Hit, bot.Hands[0].State);
            Assert.AreEqual(ToolKit.GamingState.Hit, bot.Hands[1].State);
        }
    }
}