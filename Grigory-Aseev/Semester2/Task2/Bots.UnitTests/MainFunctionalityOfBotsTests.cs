using NUnit.Framework;

namespace Bots.UnitTests
{
    public class MainFunctionalityOfBotsTests
    {
        [Test]
        public void CreateBotTest()
        {
            StandartBot bot = new StandartBot();
            Assert.IsNotNull(bot.Hands);
            Assert.IsNotNull(bot.Hands[0]);
            Assert.AreEqual(10000, bot.Balance);
            Assert.AreEqual(40, bot.GameCounter);
            Assert.AreEqual(PlayerStructure.PlayerState.Playing, bot.State);
        }

        [Test]
        public void ChangeBalanceTest()
        {
            StandartBot bot = new StandartBot(1000 - 7);
            bot.ChangeBalance(-7);
            Assert.AreEqual(993 - 7, bot.Balance);
            bot.ChangeBalance(-bot.Balance);
            Assert.AreEqual(0, bot.Balance);
            bot.ChangeBalance(-7);
            Assert.AreEqual(0, bot.Balance);
            Assert.AreEqual(PlayerStructure.PlayerState.Stop, bot.State);
        }

        [Test]
        public void ResetBalanceTest()
        {
            StandartBot bot = new StandartBot(1000 - 7);
            bot.ResetBalance(993 - 7);
            Assert.AreEqual(993 - 7, bot.Balance);
            bot.ResetBalance(-bot.Balance);
            Assert.AreEqual(993 - 7, bot.Balance);
        }

        [Test]
        public void MakeBetTest()
        {
            StandartBot bot = new StandartBot(1000 - 7, 1);
            bot.MakeBet(7);
            Assert.AreEqual(0, bot.GameCounter);
            Assert.AreEqual(986 - 7, bot.Balance);
            Assert.AreEqual(7*2, bot.Hands[0].Bet);
            Assert.AreEqual(PlayerStructure.PlayerState.Playing, bot.State);

            bot.MakeBet(7);
            Assert.AreEqual(-1, bot.GameCounter);
            Assert.AreEqual(ToolKit.GamingState.Lose, bot.Hands[0].State);
            Assert.AreEqual(PlayerStructure.PlayerState.Stop, bot.State);
        }

        [Test]
        public void TakeMoveSplitTest()
        {
            StandartBot bot = new StandartBot();
            bot.State = PlayerStructure.PlayerState.Stop;
            ToolKit.Card dealearCard = new ToolKit.Card(ToolKit.Suits.Heart, ToolKit.CardPoints.Ace);
            Assert.AreEqual(ToolKit.GamingState.Stand, bot.TakeMove(bot.Hands[0], dealearCard));
            bot.State = PlayerStructure.PlayerState.Playing;

            ToolKit.Hand hand = new ToolKit.Hand();
            hand.Cards = NewSameCards(ToolKit.CardPoints.Five);
            Assert.AreEqual(ToolKit.GamingState.Double, bot.TakeMove(hand, dealearCard));

            hand.Cards = NewSameCards(ToolKit.CardPoints.Four);
            Assert.AreEqual(ToolKit.GamingState.Hit, bot.TakeMove(hand, dealearCard));

            dealearCard = new ToolKit.Card(ToolKit.Suits.Heart, ToolKit.CardPoints.Five);
            Assert.AreEqual(ToolKit.GamingState.Split, bot.TakeMove(hand, dealearCard));

            hand.Cards = NewSameCards(ToolKit.CardPoints.Nine);
            Assert.AreEqual(ToolKit.GamingState.Split, bot.TakeMove(hand, dealearCard));

            dealearCard = new ToolKit.Card(ToolKit.Suits.Heart, ToolKit.CardPoints.Ace);
            Assert.AreEqual(ToolKit.GamingState.Stand, bot.TakeMove(hand, dealearCard));

            hand.Cards = NewSameCards(ToolKit.CardPoints.Ten);
            Assert.AreEqual(ToolKit.GamingState.Stand, bot.TakeMove(hand, dealearCard));

            hand.Cards = NewSameCards(ToolKit.CardPoints.Eight);
            Assert.AreEqual(ToolKit.GamingState.Hit, bot.TakeMove(hand, dealearCard));

            dealearCard = new ToolKit.Card(ToolKit.Suits.Heart, ToolKit.CardPoints.Ten);
            Assert.AreEqual(ToolKit.GamingState.Split, bot.TakeMove(hand, dealearCard));

            hand.Cards = NewSameCards(ToolKit.CardPoints.Seven);
            dealearCard = new ToolKit.Card(ToolKit.Suits.Heart, ToolKit.CardPoints.Ace);
            Assert.AreEqual(ToolKit.GamingState.Hit, bot.TakeMove(hand, dealearCard));

            dealearCard = new ToolKit.Card(ToolKit.Suits.Heart, ToolKit.CardPoints.Two);
            Assert.AreEqual(ToolKit.GamingState.Split, bot.TakeMove(hand, dealearCard));
        }

        [Test]
        public void TakeMoveSoftHandTest()
        {
            StandartBot bot = new StandartBot();
            ToolKit.Hand hand = new ToolKit.Hand();

            hand.Cards = NewSameCards(ToolKit.CardPoints.Ace);
            hand.Cards.Add(new ToolKit.Card(ToolKit.Suits.Heart, ToolKit.CardPoints.Four));
            ToolKit.Card dealearCard = new ToolKit.Card(ToolKit.Suits.Heart, ToolKit.CardPoints.Five);
            Assert.AreEqual(ToolKit.GamingState.Double, bot.TakeMove(hand, dealearCard));

            hand.Cards = NewSameCards(ToolKit.CardPoints.Ace);
            hand.Cards.Add(new ToolKit.Card(ToolKit.Suits.Heart, ToolKit.CardPoints.Four));
            dealearCard = new ToolKit.Card(ToolKit.Suits.Heart, ToolKit.CardPoints.Ace);
            Assert.AreEqual(ToolKit.GamingState.Hit, bot.TakeMove(hand, dealearCard));

            hand.Cards = NewSameCards(ToolKit.CardPoints.Ace);
            hand.Cards.Add(new ToolKit.Card(ToolKit.Suits.Heart, ToolKit.CardPoints.Six));
            dealearCard = new ToolKit.Card(ToolKit.Suits.Heart, ToolKit.CardPoints.Five);
            Assert.AreEqual(ToolKit.GamingState.Stand, bot.TakeMove(hand, dealearCard));

            hand.Cards = NewSameCards(ToolKit.CardPoints.Ace);
            hand.Cards.Add(new ToolKit.Card(ToolKit.Suits.Heart, ToolKit.CardPoints.Six));
            dealearCard = new ToolKit.Card(ToolKit.Suits.Heart, ToolKit.CardPoints.Nine);
            Assert.AreEqual(ToolKit.GamingState.Hit, bot.TakeMove(hand, dealearCard));

            hand.Cards = NewSameCards(ToolKit.CardPoints.Ace);
            hand.Cards.Add(new ToolKit.Card(ToolKit.Suits.Heart, ToolKit.CardPoints.Seven));
            Assert.AreEqual(ToolKit.GamingState.Stand, bot.TakeMove(hand, dealearCard));
        }

        [Test]
        public void TakeMoveHardHandTest()
        {
            StandartBot bot = new StandartBot();
            ToolKit.Hand hand = new ToolKit.Hand();
            ToolKit.Card dealearCard = new ToolKit.Card(ToolKit.Suits.Heart, ToolKit.CardPoints.Two);

            hand.Cards = NewSameCards(ToolKit.CardPoints.Two);
            hand.Cards.AddRange(NewSameCards(ToolKit.CardPoints.Two));
            Assert.AreEqual(ToolKit.GamingState.Hit, bot.TakeMove(hand, dealearCard));

            hand.Cards = NewSameCards(ToolKit.CardPoints.Two);
            hand.Cards.Add(new ToolKit.Card(ToolKit.Suits.Heart, ToolKit.CardPoints.Five));
            Assert.AreEqual(ToolKit.GamingState.Hit, bot.TakeMove(hand, dealearCard));

            dealearCard = new ToolKit.Card(ToolKit.Suits.Heart, ToolKit.CardPoints.Six);
            Assert.AreEqual(ToolKit.GamingState.Double, bot.TakeMove(hand, dealearCard));

            hand.Cards.Add(new ToolKit.Card(ToolKit.Suits.Heart, ToolKit.CardPoints.Two));
            Assert.AreEqual(ToolKit.GamingState.Double, bot.TakeMove(hand, dealearCard));

            dealearCard = new ToolKit.Card(ToolKit.Suits.Heart, ToolKit.CardPoints.Ace);
            Assert.AreEqual(ToolKit.GamingState.Hit, bot.TakeMove(hand, dealearCard));

            hand.Cards.RemoveAt(hand.Cards.Count - 2);
            hand.Cards.Add(new ToolKit.Card(ToolKit.Suits.Heart, ToolKit.CardPoints.Six));
            Assert.AreEqual(ToolKit.GamingState.Hit, bot.TakeMove(hand, dealearCard));

            dealearCard = new ToolKit.Card(ToolKit.Suits.Heart, ToolKit.CardPoints.Six);
            Assert.AreEqual(ToolKit.GamingState.Stand, bot.TakeMove(hand, dealearCard));

            hand = new ToolKit.Hand();
            hand.Cards.Add(new ToolKit.Card(ToolKit.Suits.Heart, ToolKit.CardPoints.Ten));
            hand.Cards.Add(new ToolKit.Card(ToolKit.Suits.Heart, ToolKit.CardPoints.Six));
            dealearCard = new ToolKit.Card(ToolKit.Suits.Heart, ToolKit.CardPoints.Ten);
            Assert.AreEqual(ToolKit.GamingState.Surrender, bot.TakeMove(hand, dealearCard));

            dealearCard = new ToolKit.Card(ToolKit.Suits.Heart, ToolKit.CardPoints.Seven);
            Assert.AreEqual(ToolKit.GamingState.Hit, bot.TakeMove(hand, dealearCard));

            dealearCard = new ToolKit.Card(ToolKit.Suits.Heart, ToolKit.CardPoints.Six);
            Assert.AreEqual(ToolKit.GamingState.Stand, bot.TakeMove(hand, dealearCard));

            hand.Cards.Add(new ToolKit.Card(ToolKit.Suits.Heart, ToolKit.CardPoints.Two));
            Assert.AreEqual(ToolKit.GamingState.Stand, bot.TakeMove(hand, dealearCard));
        }

        private System.Collections.Generic.List<ToolKit.Card> NewSameCards(ToolKit.CardPoints card)
        {
            System.Collections.Generic.List<ToolKit.Card> cards = new System.Collections.Generic.List<ToolKit.Card>();
            cards.Add(new ToolKit.Card(ToolKit.Suits.Heart, card));
            cards.Add(new ToolKit.Card(ToolKit.Suits.Heart, card));
            return cards;
        }
    }
}