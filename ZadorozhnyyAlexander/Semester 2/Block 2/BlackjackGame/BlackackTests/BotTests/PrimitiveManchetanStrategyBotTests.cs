using NUnit.Framework;
using BlackjackBots;
using AbstractClasses;
using BlackjackMechanics.Cards;
using BlackjackMechanics.GameTools;

namespace BlackackTests.BotTests
{
    public class PrimitiveManchetanStrategyBotTests
    {
        private const double exampleStartMoney = 1000;
        private const double exampleStartRate = 50;
        private ACard exapleDealerCard = new UsualCard(CardNames.Two, CardSuits.Spade);

        [Test]
        public void GetNextTurnTest()
        {
            var bot = new PrimitiveManchetanStrategyBot(exampleStartMoney, exampleStartRate);

            bot.CardsInHand.Add(new UsualCard(CardNames.Six, CardSuits.Diamond));
            Assert.IsTrue(bot.GetNextTurn(exapleDealerCard) == PlayerTurn.Hit);

            bot.CardsInHand.Add(new UsualCard(CardNames.Six, CardSuits.Diamond));
            Assert.IsTrue(bot.GetNextTurn(exapleDealerCard) == PlayerTurn.Double);

            bot.CardsInHand.Add(new UsualCard(CardNames.Six, CardSuits.Diamond));
            Assert.IsTrue(bot.GetNextTurn(exapleDealerCard) == PlayerTurn.Stand);

            bot.CardsInHand.Add(new UsualCard(CardNames.Three, CardSuits.Diamond));
            Assert.IsTrue(bot.GetNextTurn(exapleDealerCard) == PlayerTurn.Blackjack);
        }

        [Test]
        public void ActionAfterBlackjackTest()
        {
            var bot = new PrimitiveManchetanStrategyBot(exampleStartMoney, exampleStartRate);
            var game = new Game(bot);
            game.CreateGame(1);
            game.Dealer.GiveCard(new UsualCard(CardNames.Ten, CardSuits.Diamond), bot);
            game.Dealer.GiveCard(new UsualCard(CardNames.Ace, CardSuits.Diamond), bot);

            Assert.IsTrue(game.GetAnswerAfterFirstBlackjack() == PlayerTurn.Stand);
        }

        [Test]
        public void NextGamePreparationTest()
        {
            var bot = new PrimitiveManchetanStrategyBot(exampleStartMoney, exampleStartRate);

            bot.Win();
            Assert.IsTrue(bot.Rate == exampleStartRate * 2);

            bot.Win();
            Assert.IsTrue(bot.Rate == exampleStartRate);

            bot.Lose();
            Assert.IsTrue(bot.Rate == exampleStartRate);
        }
    }
}
