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
            var game = new Game(new UsualBaseStrategyBot(exampleStartMoney, exampleStartRate));
            game.CreateGame(1);

            // Expected Player: 11 + 2 && Dealer: 2 => Hit
            game.Dealer.GiveCard(new AceCard(CardSuits.Diamond), game.Bot);
            game.Dealer.GiveCard(new UsualCard(CardNames.Two, CardSuits.Diamond), game.Bot);
            game.Dealer.TakeCard(new UsualCard(CardNames.Two, CardSuits.Diamond));
            game.Bot.MakeNextPlayerTurn(game.Dealer.VisibleCard);
            Assert.IsTrue(game.Bot.PlayerTurnNow == PlayerTurn.Hit);

            game = new Game(new UsualBaseStrategyBot(exampleStartMoney, exampleStartRate));
            game.CreateGame(1);
            // Expected Player: 11 + 5 && Dealer: 5 => Double
            game.Dealer.GiveCard(new AceCard(CardSuits.Diamond), game.Bot);
            game.Dealer.GiveCard(new UsualCard(CardNames.Five, CardSuits.Diamond), game.Bot);
            game.Dealer.TakeCard(new UsualCard(CardNames.Five, CardSuits.Diamond));
            game.Bot.MakeNextPlayerTurn(game.Dealer.VisibleCard);
            Assert.IsTrue(game.Bot.PlayerTurnNow == PlayerTurn.Double);

            game = new Game(new UsualBaseStrategyBot(exampleStartMoney, exampleStartRate));
            game.CreateGame(1);
            // Expected Player: 11 + 9 && Dealer: 8 => Stand
            game.Dealer.GiveCard(new AceCard(CardSuits.Diamond), game.Bot);
            game.Dealer.GiveCard(new UsualCard(CardNames.Nine, CardSuits.Diamond), game.Bot);
            game.Dealer.TakeCard(new UsualCard(CardNames.Eight, CardSuits.Diamond));
            game.Bot.MakeNextPlayerTurn(game.Dealer.VisibleCard);
            Assert.IsTrue(game.Bot.PlayerTurnNow == PlayerTurn.Stand);

            game = new Game(new UsualBaseStrategyBot(exampleStartMoney, exampleStartRate));
            game.CreateGame(1);
            // Expected Player: 11 + 10 && Dealer: 9 => Blackjack
            game.Dealer.GiveCard(new AceCard(CardSuits.Diamond), game.Bot);
            game.Dealer.GiveCard(new UsualCard(CardNames.Ten, CardSuits.Diamond), game.Bot);
            game.Dealer.TakeCard(new UsualCard(CardNames.Nine, CardSuits.Diamond));
            game.Bot.MakeNextPlayerTurn(game.Dealer.VisibleCard);
            Assert.IsTrue(game.Bot.PlayerTurnNow == PlayerTurn.Blackjack);

            game = new Game(new UsualBaseStrategyBot(exampleStartMoney, exampleStartRate));
            game.CreateGame(1);
            // Expected Player: 11 + 10 && Dealer: 10 => Blackjack, but dealer have 10
            game.Dealer.GiveCard(new AceCard(CardSuits.Diamond), game.Bot);
            game.Dealer.GiveCard(new UsualCard(CardNames.Ten, CardSuits.Diamond), game.Bot);
            game.Dealer.TakeCard(new UsualCard(CardNames.Ten, CardSuits.Diamond));
            game.Bot.MakeNextPlayerTurn(game.Dealer.VisibleCard);
            Assert.IsTrue(game.Bot.PlayerTurnNow == PlayerTurn.Stand);

            game = new Game(new UsualBaseStrategyBot(exampleStartMoney, exampleStartRate));
            game.CreateGame(1);
            // Expected Player: 11 + 10 && Dealer: 10 => Stand
            game.Dealer.GiveCard(new AceCard(CardSuits.Diamond), game.Bot);
            game.Dealer.GiveCard(new UsualCard(CardNames.Ten, CardSuits.Diamond), game.Bot);
            game.Dealer.TakeCard(new AceCard(CardSuits.Diamond));
            game.Bot.MakeNextPlayerTurn(game.Dealer.VisibleCard);
            Assert.IsTrue(game.Bot.PlayerTurnNow == PlayerTurn.Stand);
        }

        [Test]
        public void GetNextTurnAfterHardTotalsTest()
        {
            var game = new Game(new UsualBaseStrategyBot(exampleStartMoney, exampleStartRate));
            game.CreateGame(1);

            // Expected Player: 5 + 2 && Dealer: 2 => Hit
            game.Dealer.GiveCard(new UsualCard(CardNames.Five, CardSuits.Diamond), game.Bot);
            game.Dealer.GiveCard(new UsualCard(CardNames.Two, CardSuits.Diamond), game.Bot);
            game.Dealer.TakeCard(new UsualCard(CardNames.Two, CardSuits.Diamond));
            game.Bot.MakeNextPlayerTurn(game.Dealer.VisibleCard);
            Assert.IsTrue(game.Bot.PlayerTurnNow == PlayerTurn.Hit);

            game = new Game(new UsualBaseStrategyBot(exampleStartMoney, exampleStartRate));
            game.CreateGame(1);
            // Expected Player: 11 + 5 && Dealer: 5 => Double
            game.Dealer.GiveCard(new UsualCard(CardNames.Six, CardSuits.Diamond), game.Bot);
            game.Dealer.GiveCard(new UsualCard(CardNames.Five, CardSuits.Diamond), game.Bot);
            game.Dealer.TakeCard(new UsualCard(CardNames.Five, CardSuits.Diamond));
            game.Bot.MakeNextPlayerTurn(game.Dealer.VisibleCard);
            Assert.IsTrue(game.Bot.PlayerTurnNow == PlayerTurn.Double);

            game = new Game(new UsualBaseStrategyBot(exampleStartMoney, exampleStartRate));
            game.CreateGame(1);
            // Expected Player: 11 + 9 && Dealer: 8 => Stand
            game.Dealer.GiveCard(new UsualCard(CardNames.Nine, CardSuits.Diamond), game.Bot);
            game.Dealer.GiveCard(new UsualCard(CardNames.Nine, CardSuits.Diamond), game.Bot);
            game.Dealer.TakeCard(new UsualCard(CardNames.Eight, CardSuits.Diamond));
            game.Bot.MakeNextPlayerTurn(game.Dealer.VisibleCard);
            Assert.IsTrue(game.Bot.PlayerTurnNow == PlayerTurn.Stand);
        }

        [Test]
        public void ActionAfterBlackjackTest()
        {
            var bot = new UsualBaseStrategyBot(exampleStartMoney, exampleStartRate);
            var game = new Game(bot);
            game.CreateGame(1);

            game.Dealer.TakeCard(new AceCard(CardSuits.Diamond));
            game.Bot.TakeCard(new UsualCard(CardNames.Ten, CardSuits.Diamond));
            game.Bot.TakeCard(new AceCard(CardSuits.Diamond));
            game.Bot.MakeNextPlayerTurn(game.Dealer.VisibleCard);

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
