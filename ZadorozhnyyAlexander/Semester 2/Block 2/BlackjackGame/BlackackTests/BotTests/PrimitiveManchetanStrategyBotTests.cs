using NUnit.Framework;
using BlackjackBots;
using AbstractClasses;
using BlackjackMechanics.Cards;
using BlackjackMechanics.GameTools;

namespace BlackackTests.BotTests
{
    public class PrimitiveManchetanStrategyBotTests
    {
        private const double ExampleStartMoney = 1000;
        private const double ExampleStartRate = 50;
        private ACard ExapleDealerCard = new NumberCard(CardNames.Two, CardSuits.Spade);

        [Test]
        public void GetNextTurnTest()
        {
            var bot = new PrimitiveManchetanStrategyBot(ExampleStartMoney, ExampleStartRate);

            bot.CardsInHand.Add(new NumberCard(CardNames.Six, CardSuits.Diamond));
            Assert.IsTrue(bot.GetNextTurn(ExapleDealerCard) == PlayerTurn.Hit);

            bot.CardsInHand.Add(new NumberCard(CardNames.Six, CardSuits.Diamond));
            Assert.IsTrue(bot.GetNextTurn(ExapleDealerCard) == PlayerTurn.Double);

            bot.CardsInHand.Add(new NumberCard(CardNames.Six, CardSuits.Diamond));
            Assert.IsTrue(bot.GetNextTurn(ExapleDealerCard) == PlayerTurn.Stand);

            bot.CardsInHand.Add(new NumberCard(CardNames.Three, CardSuits.Diamond));
            Assert.IsTrue(bot.GetNextTurn(ExapleDealerCard) == PlayerTurn.Blackjack);
        }

        [Test]
        public void ActionAfterBlackjackTest()
        {
            var bot = new PrimitiveManchetanStrategyBot(ExampleStartMoney, ExampleStartRate);
            var game = new Game(bot);
            game.CreateGame(1);
            game.Dealer.GiveCard(new NumberCard(CardNames.Ten, CardSuits.Diamond), bot);
            game.Dealer.GiveCard(new NumberCard(CardNames.Ace, CardSuits.Diamond), bot);

            Assert.IsTrue(game.GetAnswerAfterFirstBlackjack() == PlayerTurn.Stand);
        }

        [Test]
        public void NextGamePreparationTest()
        {
            var bot = new PrimitiveManchetanStrategyBot(ExampleStartMoney, ExampleStartRate);

            bot.Win();
            Assert.IsTrue(bot.Rate == ExampleStartRate * 2);

            bot.Win();
            Assert.IsTrue(bot.Rate == ExampleStartRate);

            bot.Lose();
            Assert.IsTrue(bot.Rate == ExampleStartRate);
        }
    }
}
