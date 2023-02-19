using NUnit.Framework;
using Bots;
using System.Collections.Generic;

namespace BlackJack.UnitTests
{
    public class GameTests
    {
        [Test]
        public void StartNewTurnTest()
        {
            GoodBot goodBot = new GoodBot("GoodBot", 1000);
            DoubleBot doubleBot = new DoubleBot("DoubleBot", 1000);
            RandomBot randomBot = new RandomBot("RandomBot", 1000);
            List<Player> players = new List<Player> { goodBot, doubleBot, randomBot };
            Game game = new Game(players);
            game.StartNewTurn();
            foreach (Player player in players)
            {
                Assert.AreEqual(player.Cards.Count, 2);
                Assert.IsTrue(player.Balance < 1000);
                Assert.IsTrue(player.Bet > 0);
            }
            Assert.AreEqual(game.Croupier.Cards.Count, 2);

            Assert.Pass();
        }

        [Test]
        public void StartActingTest()
        {
            GoodBot firstGoodBot = new GoodBot("FirstGoodBot", 1000);
            GoodBot secondGoodBot = new GoodBot("SecondGoodBot", 1000);
            GoodBot thirdGoodBot = new GoodBot("ThirdGoodBot", 1000);
            List<Player> players = new List<Player> { firstGoodBot, secondGoodBot, thirdGoodBot };
            Game game = new Game(players);
            game.StartNewTurn();
            game.StartActing();
            foreach (Player player in players)
            {
                Assert.IsTrue(player.GetScore() > 17 || player.Bet > 100 || player.Balance < 900 || player.Cards.Count > 2);
            }
            Assert.IsTrue(game.Croupier.GetScore() > 17 || game.Croupier.GetScore() == 0);

            Assert.Pass();
        }

        [Test]
        public void EndTurnTest()
        {
            GoodBot firstGoodBot = new GoodBot("FirstGoodBot", 1000);
            GoodBot secondGoodBot = new GoodBot("SecondGoodBot", 1000);
            GoodBot thirdGoodBot = new GoodBot("ThirdGoodBot", 1000);
            GoodBot fourthGoodBot = new GoodBot("FourthGoodBot", 1000);
            List<Player> players = new List<Player> { firstGoodBot, secondGoodBot, thirdGoodBot, fourthGoodBot };
            Game game = new Game(players);
            firstGoodBot.TookCard(new Card(0, 1));
            firstGoodBot.TookCard(new Card(0, 10));
            firstGoodBot.MakeBet(100);
            secondGoodBot.TookCard(new Card(0, 1));
            secondGoodBot.MakeBet(100);
            game.Croupier.TookCard(new Card(0, 1));
            thirdGoodBot.TookCard(new Card(0, 2));
            thirdGoodBot.MakeBet(1000);
            fourthGoodBot.TookCard(new Card(0, 10));
            fourthGoodBot.TookCard(new Card(0, 10));
            fourthGoodBot.MakeBet(100);
            game.EndTurn();
            Assert.AreEqual(firstGoodBot.Balance, 1150);
            Assert.AreEqual(secondGoodBot.Balance, 1000);
            Assert.AreEqual(fourthGoodBot.Balance, 1100);
            Assert.AreEqual(game.Players.Count, 3);

            Assert.Pass();
        }

        [Test]
        public void StartGameTest()
        {
            RandomBot randomBot = new RandomBot("RandomBot", 0);
            Game game = new Game(new List<Player> { randomBot });
            game.StartGame();
            Assert.AreEqual(game.Players.Count, 0);
            Assert.AreEqual(randomBot.Balance, 0);
            GoodBot goodBot = new GoodBot("GoodBot", 1000);
            game = new Game(new List<Player> { goodBot });
            game.StartGame(40);
            Assert.IsTrue(goodBot.Balance != 1000);

            Assert.Pass();
        }
    }
}
