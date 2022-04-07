using NUnit.Framework;
using BlackjackBots;
using AbstractClasses;
using BlackjackMechanics.Cards;
using BlackjackMechanics.GameTools;


namespace BlackackTests.BotTests
{
    public class UsualBaseStrategyBotTests
    {
        private const double exampleStartMoney = 1000;
        private const double exampleStartRate = 50;

        [Test]
        public void GetNextTurnAfterSoftTotalsTest()
        {
            var bot = new UsualBaseStrategyBot(exampleStartMoney, exampleStartRate);

            // Expected Player: 11 + 2 && Dealer: 2 => Hit
            bot.TakeCard(new AceCard(CardSuits.Diamond));
            bot.TakeCard(new UsualCard(CardNames.Two, CardSuits.Diamond));
            bot.MakeNextPlayerTurn(new UsualCard(CardNames.Two, CardSuits.Diamond));
            Assert.IsTrue(bot.PlayerTurnNow == PlayerTurn.Hit);

            bot = new UsualBaseStrategyBot(exampleStartMoney, exampleStartRate);
            // Expected Player: 11 + 5 && Dealer: 5 => Double
            bot.TakeCard(new AceCard(CardSuits.Diamond));
            bot.TakeCard(new UsualCard(CardNames.Five, CardSuits.Diamond));
            bot.MakeNextPlayerTurn(new UsualCard(CardNames.Five, CardSuits.Diamond));
            Assert.IsTrue(bot.PlayerTurnNow == PlayerTurn.Double);

            bot = new UsualBaseStrategyBot(exampleStartMoney, exampleStartRate);
            // Expected Player: 11 + 9 && Dealer: 8 => Stand
            bot.TakeCard(new AceCard(CardSuits.Diamond));
            bot.TakeCard(new UsualCard(CardNames.Nine, CardSuits.Diamond));
            bot.MakeNextPlayerTurn(new UsualCard(CardNames.Eight, CardSuits.Diamond));
            Assert.IsTrue(bot.PlayerTurnNow == PlayerTurn.Stand);

            bot = new UsualBaseStrategyBot(exampleStartMoney, exampleStartRate);
            // Expected Player: 11 + 10 && Dealer: 9 => Blackjack
            bot.TakeCard(new AceCard(CardSuits.Diamond));
            bot.TakeCard(new UsualCard(CardNames.Ten, CardSuits.Diamond));
            bot.MakeNextPlayerTurn(new UsualCard(CardNames.Nine, CardSuits.Diamond));
            Assert.IsTrue(bot.PlayerTurnNow == PlayerTurn.Blackjack);

            bot = new UsualBaseStrategyBot(exampleStartMoney, exampleStartRate);
            // Expected Player: 11 + 10 && Dealer: 10 => Blackjack, but dealer have 10
            bot.TakeCard(new AceCard(CardSuits.Diamond));
            bot.TakeCard(new UsualCard(CardNames.Ten, CardSuits.Diamond));
            bot.MakeNextPlayerTurn(new UsualCard(CardNames.Ten, CardSuits.Diamond));
            Assert.IsTrue(bot.PlayerTurnNow == PlayerTurn.Stand);

            bot = new UsualBaseStrategyBot(exampleStartMoney, exampleStartRate);
            // Expected Player: 11 + 10 && Dealer: 10 => Stand
            bot.TakeCard(new AceCard(CardSuits.Diamond));
            bot.TakeCard(new UsualCard(CardNames.Ten, CardSuits.Diamond));
            bot.MakeNextPlayerTurn(new AceCard(CardSuits.Diamond));
            Assert.IsTrue(bot.PlayerTurnNow == PlayerTurn.Stand);
        }

        [Test]
        public void GetNextTurnAfterHardTotalsTest()
        {
            var bot = new UsualBaseStrategyBot(exampleStartMoney, exampleStartRate);
            // Expected Player: 5 + 2 && Dealer: 2 => Hit
            bot.TakeCard(new UsualCard(CardNames.Five, CardSuits.Diamond));
            bot.TakeCard(new UsualCard(CardNames.Two, CardSuits.Diamond));
            bot.MakeNextPlayerTurn(new UsualCard(CardNames.Two, CardSuits.Diamond));
            Assert.IsTrue(bot.PlayerTurnNow == PlayerTurn.Hit);

            bot = new UsualBaseStrategyBot(exampleStartMoney, exampleStartRate);
            // Expected Player: 11 + 5 && Dealer: 5 => Double
            bot.TakeCard(new UsualCard(CardNames.Six, CardSuits.Diamond));
            bot.TakeCard(new UsualCard(CardNames.Five, CardSuits.Diamond));
            bot.MakeNextPlayerTurn(new UsualCard(CardNames.Five, CardSuits.Diamond));
            Assert.IsTrue(bot.PlayerTurnNow == PlayerTurn.Double);

            bot = new UsualBaseStrategyBot(exampleStartMoney, exampleStartRate);
            // Expected Player: 11 + 9 && Dealer: 8 => Stand
            bot.TakeCard(new UsualCard(CardNames.Nine, CardSuits.Diamond));
            bot.TakeCard(new UsualCard(CardNames.Nine, CardSuits.Diamond));
            bot.MakeNextPlayerTurn(new UsualCard(CardNames.Eight, CardSuits.Diamond));
            Assert.IsTrue(bot.PlayerTurnNow == PlayerTurn.Stand);
        }

        [Test]
        public void ActionAfterBlackjackTest()
        {
            var bot = new UsualBaseStrategyBot(exampleStartMoney, exampleStartRate);

            bot.TakeCard(new UsualCard(CardNames.Ten, CardSuits.Diamond));
            bot.TakeCard(new AceCard(CardSuits.Diamond));
            bot.MakeNextPlayerTurn(new AceCard(CardSuits.Diamond));

            Assert.IsTrue(bot.PlayerTurnNow == PlayerTurn.Stand);
        }

        [Test]
        public void NextGamePreparationTest()
        {
            var bot = new UsualBaseStrategyBot(exampleStartMoney, exampleStartRate);

            bot.Win();
            Assert.IsTrue(bot.Rate == exampleStartRate);

            bot.Lose();
            Assert.IsTrue(bot.Rate == exampleStartRate);
        }
    }
}
