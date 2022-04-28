using NUnit.Framework;

namespace Bots.UnitTests
{
    public class FunctionalityOfCountingBotsTests
    {
        readonly ToolKit.Card cardTen = new ToolKit.Card(ToolKit.Suits.Heart, ToolKit.CardPoints.Ten);
        readonly ToolKit.Card cardEight = new ToolKit.Card(ToolKit.Suits.Heart, ToolKit.CardPoints.Eight);
        readonly ToolKit.Card cardFive = new ToolKit.Card(ToolKit.Suits.Heart, ToolKit.CardPoints.Five);
        readonly System.Collections.Generic.List<ToolKit.Card> cards = new System.Collections.Generic.List<ToolKit.Card>();

        [Test]
        public void CreateTest()
        {
            var bot1 = new PlusMinusBot();
            Assert.AreEqual(PlayerStructure.ShoeState.Calculated, bot1.StateOfShoe);

            var bot2 = new HalvesBot();
            Assert.AreEqual(PlayerStructure.ShoeState.Calculated, bot2.StateOfShoe);
        }

        [Test]
        public void CountPlusMinusBotTest()
        {
            // realAccount less than zero

            var bot = new PlusMinusBot();
            PrepareToRealAccountLessZeroTest(bot);
            Assert.AreEqual(1000 - 7, bot.Hands[0].Bet);
            bot.MakeBet(300);
            Assert.AreEqual((int)((1000 - 7) / 2), bot.Hands[0].Bet);

            // realAccount more than zero

            bot = new PlusMinusBot();
            PrepareToRealAccountMoreZeroTest(bot);
            Assert.AreEqual(2 * (1000 - 7), bot.Hands[0].Bet);
            bot.MakeBet(100);
            Assert.AreEqual((1000 - 7) * 4, bot.Hands[0].Bet);

            // realAccount equals zero

            bot = new PlusMinusBot();
            PrepareToRealAccountZeroTest(bot);
            Assert.AreEqual(16 * (1000 - 7), bot.Hands[0].Bet);
        }

        [Test]
        public void CountHalvesBotTest()
        {
            // realAccount less than zero

            var bot = new HalvesBot();
            PrepareToRealAccountLessZeroTest(bot);
            Assert.AreEqual(1000 - 7, bot.Hands[0].Bet);
            bot.MakeBet(200);
            Assert.AreEqual((int)((1000 - 7) / 4), bot.Hands[0].Bet);

            // realAccount more than zero

            bot = new HalvesBot();
            PrepareToRealAccountMoreZeroTest(bot);
            Assert.AreEqual(4 * (1000 - 7), bot.Hands[0].Bet);
            bot.MakeBet(10);
            Assert.AreEqual((1000 - 7) * 16, bot.Hands[0].Bet);

            // realAccount equals zero

            bot = new HalvesBot();
            PrepareToRealAccountZeroTest(bot);
            Assert.AreEqual(16 * (1000 - 7), bot.Hands[0].Bet);
        }

        private void PrepareToRealAccountLessZeroTest(Bots.ACountingBot bot)
        {
            for (int i = 0; i < 160; i++)
            {
                bot.Hands[0].Cards.Add(cardTen);
            }

            bot.ThinkOver(cards);
            bot.MakeBet(1000 - 7);
        }

        private void PrepareToRealAccountMoreZeroTest(Bots.ACountingBot bot)
        {
            for (int i = 0; i < 32; i++)
            {
                bot.Hands[0].Cards.Add(cardFive);
            }

            bot.ThinkOver(cards);
            bot.MakeBet(1000 - 7);
        }

        private void PrepareToRealAccountZeroTest(Bots.ACountingBot bot)
        {
            for (int i = 0; i < 32; i++)
            {
                bot.Hands[0].Cards.Add(cardEight);
            }

            bot.ThinkOver(cards);
            bot.MakeBet(1000 - 7);
        }
    }
}