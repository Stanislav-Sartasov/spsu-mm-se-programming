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
            var game = new Game(bot);
            game.CreateGame(8);

            // Expected Player: 11 + 2 && Dealer: 2 => Hit
            game.Dealer.GiveCard(new AceCard(CardSuits.Diamond), bot);
            game.Dealer.GiveCard(new UsualCard(CardNames.Two, CardSuits.Diamond), bot);
            game.Dealer.TakeCard(new UsualCard(CardNames.Two, CardSuits.Diamond));
            Assert.IsTrue(game.GetNextPlayerTurn() == PlayerTurn.Hit);
            game.ResetGameParams();

            // Expected Player: 11 + 5 && Dealer: 5 => Double
            game.Dealer.GiveCard(new AceCard(CardSuits.Diamond), bot);
            game.Dealer.GiveCard(new UsualCard(CardNames.Five, CardSuits.Diamond), bot);
            game.Dealer.TakeCard(new UsualCard(CardNames.Five, CardSuits.Diamond));
            Assert.IsTrue(game.GetNextPlayerTurn() == PlayerTurn.Double);
            game.ResetGameParams();

            // Expected Player: 11 + 9 && Dealer: 8 => Stand
            game.Dealer.GiveCard(new AceCard(CardSuits.Diamond), bot);
            game.Dealer.GiveCard(new UsualCard(CardNames.Nine, CardSuits.Diamond), bot);
            game.Dealer.TakeCard(new UsualCard(CardNames.Eight, CardSuits.Diamond));
            Assert.IsTrue(game.GetNextPlayerTurn() == PlayerTurn.Stand);
            game.ResetGameParams();

            // Expected Player: 11 + 10 && Dealer: 9 => Blackjack
            game.Dealer.GiveCard(new AceCard(CardSuits.Diamond), bot);
            game.Dealer.GiveCard(new UsualCard(CardNames.Ten, CardSuits.Diamond), bot);
            game.Dealer.TakeCard(new UsualCard(CardNames.Nine, CardSuits.Diamond));
            Assert.IsTrue(game.GetNextPlayerTurn() == PlayerTurn.Blackjack);
            game.ResetGameParams();

            // Expected Player: 11 + 10 && Dealer: 10 => Blackjack, but dealer have 10
            game.Dealer.GiveCard(new AceCard(CardSuits.Diamond), bot);
            game.Dealer.GiveCard(new UsualCard(CardNames.Ten, CardSuits.Diamond), bot);
            game.Dealer.TakeCard(new UsualCard(CardNames.Ten, CardSuits.Diamond));
            Assert.IsTrue(game.GetNextPlayerTurn() == PlayerTurn.Stand);
            game.ResetGameParams();

            // Expected Player: 11 + 10 && Dealer: 10 => Stand
            game.Dealer.GiveCard(new AceCard(CardSuits.Diamond), bot);
            game.Dealer.GiveCard(new UsualCard(CardNames.Ten, CardSuits.Diamond), bot);
            game.Dealer.TakeCard(new AceCard(CardSuits.Diamond));
            Assert.IsTrue(game.GetNextPlayerTurn() == PlayerTurn.Stand);
            game.ResetGameParams();
        }

        [Test]
        public void GetNextTurnAfterHardTotalsTest()
        {
            var bot = new UsualBaseStrategyBot(exampleStartMoney, exampleStartRate);
            var game = new Game(bot);
            game.CreateGame(8);

            // Expected Player: 5 + 2 && Dealer: 2 => Hit
            game.Dealer.GiveCard(new UsualCard(CardNames.Five, CardSuits.Diamond), bot);
            game.Dealer.GiveCard(new UsualCard(CardNames.Two, CardSuits.Diamond), bot);
            game.Dealer.TakeCard(new UsualCard(CardNames.Two, CardSuits.Diamond));
            Assert.IsTrue(game.GetNextPlayerTurn() == PlayerTurn.Hit);
            game.ResetGameParams();

            // Expected Player: 11 + 5 && Dealer: 5 => Double
            game.Dealer.GiveCard(new UsualCard(CardNames.Six, CardSuits.Diamond), bot);
            game.Dealer.GiveCard(new UsualCard(CardNames.Five, CardSuits.Diamond), bot);
            game.Dealer.TakeCard(new UsualCard(CardNames.Five, CardSuits.Diamond));
            Assert.IsTrue(game.GetNextPlayerTurn() == PlayerTurn.Double);
            game.ResetGameParams();

            // Expected Player: 11 + 9 && Dealer: 8 => Stand
            game.Dealer.GiveCard(new UsualCard(CardNames.Nine, CardSuits.Diamond), bot);
            game.Dealer.GiveCard(new UsualCard(CardNames.Nine, CardSuits.Diamond), bot);
            game.Dealer.TakeCard(new UsualCard(CardNames.Eight, CardSuits.Diamond));
            Assert.IsTrue(game.GetNextPlayerTurn() == PlayerTurn.Stand);
            game.ResetGameParams();
        }

        [Test]
        public void ActionAfterBlackjackTest()
        {
            var bot = new UsualBaseStrategyBot(exampleStartMoney, exampleStartRate);
            var game = new Game(bot);
            game.CreateGame(1);

            bot.CardsInHand.Add(new UsualCard(CardNames.Ten, CardSuits.Diamond));
            bot.CardsInHand.Add(new UsualCard(CardNames.Ace, CardSuits.Diamond));

            Assert.IsTrue(game.GetAnswerAfterFirstBlackjack() == PlayerTurn.Stand);
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
