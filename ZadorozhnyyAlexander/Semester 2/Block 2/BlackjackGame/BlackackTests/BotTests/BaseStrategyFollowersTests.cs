using NUnit.Framework;
using BlackjackBots;
using AbstractClasses;
using BlackjackMechanics.Cards;
using BlackjackMechanics.GameTools;

namespace BlackackTests.BotTests
{
    public class BaseStrategyFollowersTests
    {
        private const double ExampleStartMoney = 1000;
        private const double ExampleStartRate = 50;

        [Test]
        public void MartingaleBotActionAfterBlackjackTest()
        {
            var bot = new MartingaleBot(ExampleStartMoney, ExampleStartRate);
            var game = new Game(bot);
            game.CreateGame(1);
            game.Dealer.GiveCard(new NumberCard(CardNames.Ten, CardSuits.Diamond), bot);
            game.Dealer.GiveCard(new NumberCard(CardNames.Ace, CardSuits.Diamond), bot);

            Assert.IsTrue(game.GetAnswerAfterFirstBlackjack() == PlayerTurn.Stand);
        }

        [Test]
        public void OneThreeTwoSixBotActionAfterBlackjackTest()
        {
            var bot = new OneThreeTwoSixBot(ExampleStartMoney, ExampleStartRate);
            var game = new Game(bot);
            game.CreateGame(1);
            game.Dealer.GiveCard(new NumberCard(CardNames.Ten, CardSuits.Diamond), bot);
            game.Dealer.GiveCard(new NumberCard(CardNames.Ace, CardSuits.Diamond), bot);

            Assert.IsTrue(game.GetAnswerAfterFirstBlackjack() == PlayerTurn.Take);
        }

        [Test]
        public void MartingaleBotNextGamePreparationTest()
        {
            var bot = new MartingaleBot(ExampleStartMoney, ExampleStartRate);

            bot.Lose();
            Assert.IsTrue(bot.Rate == ExampleStartRate * 2);

            bot.Lose();
            Assert.IsTrue(bot.Rate == ExampleStartRate * 4);

            bot.Win();
            Assert.IsTrue(bot.Rate == ExampleStartRate);
        }

        [Test]
        public void OneThreeTwoSixBotNextGamePreparationTest()
        {
            var bot = new OneThreeTwoSixBot(ExampleStartMoney, ExampleStartRate);

            bot.Win();
            Assert.IsTrue(bot.Rate == ExampleStartRate * 3);

            bot.Win();
            Assert.IsTrue(bot.Rate == ExampleStartRate * 2);

            bot.Lose();
            Assert.IsTrue(bot.Rate == ExampleStartRate);
        }
    }
}
