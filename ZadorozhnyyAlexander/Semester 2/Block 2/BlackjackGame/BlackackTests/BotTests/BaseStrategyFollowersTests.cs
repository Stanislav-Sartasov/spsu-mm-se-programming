using NUnit.Framework;
using BlackjackBots;
using AbstractClasses;
using BlackjackMechanics.Cards;
using BlackjackMechanics.GameTools;

namespace BlackackTests.BotTests
{
    public class BaseStrategyFollowersTests
    {
        private const double exampleStartMoney = 1000;
        private const double exampleStartRate = 50;

        [Test]
        public void MartingaleBotActionAfterBlackjackTest()
        {
            var bot = new MartingaleBot(exampleStartMoney, exampleStartRate);

            bot.TakeCard(new UsualCard(CardNames.Ten, CardSuits.Diamond));
            bot.TakeCard(new AceCard(CardSuits.Diamond));
            bot.MakeNextPlayerTurn(new AceCard(CardSuits.Diamond));

            Assert.IsTrue(bot.PlayerTurnNow == PlayerTurn.Stand);
        }

        [Test]
        public void OneThreeTwoSixBotActionAfterBlackjackTest()
        {
            var bot = new OneThreeTwoSixBot(exampleStartMoney, exampleStartRate);

            bot.TakeCard(new UsualCard(CardNames.Ten, CardSuits.Diamond));
            bot.TakeCard(new AceCard(CardSuits.Diamond));
            bot.MakeNextPlayerTurn(new AceCard(CardSuits.Diamond));

            Assert.IsTrue(bot.PlayerTurnNow == PlayerTurn.Take);
        }

        [Test]
        public void MartingaleBotNextGamePreparationTest()
        {
            var bot = new MartingaleBot(exampleStartMoney, exampleStartRate);

            bot.Lose();
            Assert.IsTrue(bot.Rate == exampleStartRate * 2);

            bot.Lose();
            Assert.IsTrue(bot.Rate == exampleStartRate * 4);

            bot.Win();
            Assert.IsTrue(bot.Rate == exampleStartRate);
        }

        [Test]
        public void OneThreeTwoSixBotNextGamePreparationTest()
        {
            var bot = new OneThreeTwoSixBot(exampleStartMoney, exampleStartRate);

            bot.Win();
            Assert.IsTrue(bot.Rate == exampleStartRate * 3);

            bot.Win();
            Assert.IsTrue(bot.Rate == exampleStartRate * 2);

            bot.Lose();
            Assert.IsTrue(bot.Rate == exampleStartRate);
        }
    }
}
