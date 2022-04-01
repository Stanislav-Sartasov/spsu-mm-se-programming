using NUnit.Framework;
using BlackJack;
using Bots;

namespace Task_2.UnitTests
{
	public class BlackJackTests
	{
		[Test]
		public void GetValueTest()
		{
			Assert.AreEqual(new Card(Rank.Three, Suit.Diamonds).GetValue(10), 3);
			Assert.AreEqual(new Card(Rank.Eight, Suit.Diamonds).GetValue(15), 8);
			Assert.AreEqual(new Card(Rank.Ace, Suit.Clubs).GetValue(10), 11);
			Assert.AreEqual(new Card(Rank.Ace, Suit.Clubs).GetValue(15), 1);
			Assert.AreEqual(new Card(Rank.King, Suit.Spades).GetValue(10), 10);
			Assert.AreEqual(new Card(Rank.Jack, Suit.Hearts).GetValue(15), 10);

			Assert.Pass();
		}

		[Test]
		public void BeginGameTest()
		{
			Game game = new Game();
			Croupier croupier = new Croupier(game);
			croupier.BeginGame();
			Assert.AreEqual(croupier.Hand.Cards.Count, 2);

			Assert.Pass();
		}

		[Test]
		public void HandClearTest()
		{
			Hand hand = new Hand();
			Deck deck = new Deck();
			hand.TakeCard(deck);
			hand.Clear();
			Assert.AreEqual(hand.Cards.Count, 0);

			Assert.Pass();
		}

		[Test]
		public void CountPointsTest()
		{
			Hand hand = new Hand();
			hand.Cards.Add(new Card(Rank.Three, Suit.Diamonds));
			hand.Cards.Add(new Card(Rank.Ace, Suit.Clubs));
			Assert.AreEqual(hand.CountPoints(), 14);

			Assert.Pass();
		}

		[Test]
		public void CroupierFinishTest()
		{
			Game game = new Game();
			Croupier croupier = new Croupier(game);
			croupier.BeginGame();
			croupier.Finish();
			Assert.AreEqual(croupier.Hand.Cards.Count, 0);

			Assert.Pass();
		}

		[Test]
		public void FirstBotMakeBetTst()
		{
			Game game = new Game();
			FirstBot bot = new FirstBot(game, 1000);
			bot.MakeBet();
			Assert.AreEqual(bot.Balance, 950);

			bot.Balance = 20;
			bot.MakeBet();
			Assert.AreEqual(bot.Balance, 15);

			Assert.Pass();
		}

		[Test]
		public void SecondBotMakeBetTest()
		{
			Game game = new Game();

			SecondBot bot = new SecondBot(game, 1000);
			bot.MakeBet();
			Assert.AreEqual(bot.Balance, 875);

			bot.Balance = 800;
			bot.MakeBet();
			Assert.AreEqual(bot.Balance, 750);

			bot.Balance = 204;
			bot.MakeBet();
			Assert.AreEqual(bot.Balance, 153);

			bot.Balance = 198;
			bot.MakeBet();
			Assert.AreEqual(bot.Balance, 99);

			bot.Balance = 37;
			bot.MakeBet();
			Assert.AreEqual(bot.Balance, 27);

			Assert.Pass();
		}

		[Test]
		public void ThirdBotMakeBetTest()
		{
			Game game = new Game();

			ThirdBot bot = new ThirdBot(game, 1000);
			bot.MakeBet();
			Assert.AreEqual(bot.Balance, 880);

			bot.Balance = 700;
			bot.MakeBet();
			Assert.AreEqual(bot.Balance, 640);

			bot.Balance = 500;
			bot.MakeBet();
			Assert.AreEqual(bot.Balance, 470);

			bot.Balance = 300;
			bot.MakeBet();
			Assert.AreEqual(bot.Balance, 285);

			bot.Balance = 100;
			bot.MakeBet();
			Assert.AreEqual(bot.Balance, 95);
		}

		[Test]
		public void PlayTurnTest()
		{
			Game game = new Game();
			FirstBot bot = new FirstBot(game, 1000);

			bot.Hand.Cards.Add(new Card(Rank.Ace, Suit.Clubs));
			bot.Hand.Cards.Add(new Card(Rank.Eight, Suit.Diamonds));
			bot.PlayTurn();

			bot = new FirstBot(game, 1000);

			bot.Hand.Cards.Add(new Card(Rank.Ace, Suit.Clubs));
			bot.Hand.Cards.Add(new Card(Rank.Jack, Suit.Diamonds));
			bot.PlayTurn();

			bot = new FirstBot(game, 1000);

			bot.Hand.Cards.Add(new Card(Rank.Ace, Suit.Clubs));
			bot.Hand.Cards.Add(new Card(Rank.Four, Suit.Diamonds));
			bot.PlayTurn();

			Assert.Pass();
		}

		[Test]
		public void PlayerFinishTest()
		{
			Game game = new Game();
			FirstBot bot = new FirstBot(game, 1000);
			Croupier croupier = game.Croupier;

			bot.MakeBet();
			bot.Hand.Cards.Add(new Card(Rank.King, Suit.Clubs));
			bot.Hand.Cards.Add(new Card(Rank.Five, Suit.Diamonds));
			bot.Hand.Cards.Add(new Card(Rank.Seven, Suit.Diamonds));
			bot.Finish();
			Assert.AreEqual(bot.Balance, 950);

			bot.Balance = 1000;

			bot.MakeBet();
			bot.Hand.Cards.Add(new Card(Rank.King, Suit.Clubs));
			bot.Hand.Cards.Add(new Card(Rank.Five, Suit.Diamonds));
			bot.Hand.Cards.Add(new Card(Rank.Six, Suit.Diamonds));
			croupier.Hand.Cards.Add(new Card(Rank.Ace, Suit.Spades));
			croupier.Hand.Cards.Add(new Card(Rank.Ten, Suit.Clubs));
			bot.Finish();
			croupier.Finish();
			Assert.AreEqual(bot.Balance, 1000);

			bot.Balance = 1000;

			bot.MakeBet();
			bot.Hand.Cards.Add(new Card(Rank.Ace, Suit.Spades));
			bot.Hand.Cards.Add(new Card(Rank.Ten, Suit.Clubs));
			croupier.Hand.Cards.Add(new Card(Rank.Ace, Suit.Clubs));
			croupier.Hand.Cards.Add(new Card(Rank.Ten, Suit.Spades));
			bot.Finish();
			croupier.Finish();
			Assert.AreEqual(bot.Balance, 1000);

			bot.Balance = 1000;

			bot.MakeBet();
			bot.Hand.Cards.Add(new Card(Rank.Ace, Suit.Spades));
			bot.Hand.Cards.Add(new Card(Rank.Ten, Suit.Clubs));
			croupier.Hand.Cards.Add(new Card(Rank.Ace, Suit.Clubs));
			croupier.Hand.Cards.Add(new Card(Rank.Seven, Suit.Spades));
			bot.Finish();
			croupier.Finish();
			Assert.AreEqual(bot.Balance, 1025);

			bot.Balance = 1000;

			bot.MakeBet();
			bot.Hand.Cards.Add(new Card(Rank.King, Suit.Clubs));
			bot.Hand.Cards.Add(new Card(Rank.Five, Suit.Diamonds));
			bot.Hand.Cards.Add(new Card(Rank.Six, Suit.Diamonds));
			croupier.Hand.Cards.Add(new Card(Rank.Ace, Suit.Spades));
			croupier.Hand.Cards.Add(new Card(Rank.Four, Suit.Clubs));
			croupier.Hand.Cards.Add(new Card(Rank.Six, Suit.Spades));
			bot.Finish();
			croupier.Finish();
			Assert.AreEqual(bot.Balance, 1000);

			bot.Balance = 1000;

			bot.MakeBet();
			bot.Hand.Cards.Add(new Card(Rank.King, Suit.Clubs));
			bot.Hand.Cards.Add(new Card(Rank.Five, Suit.Diamonds));
			bot.Hand.Cards.Add(new Card(Rank.Six, Suit.Diamonds));
			croupier.Hand.Cards.Add(new Card(Rank.Ace, Suit.Spades));
			croupier.Hand.Cards.Add(new Card(Rank.Seven, Suit.Clubs));
			bot.Finish();
			croupier.Finish();
			Assert.AreEqual(bot.Balance, 1025);

			bot.Balance = 1000;

			bot.MakeBet();
			bot.Hand.Cards.Add(new Card(Rank.King, Suit.Clubs));
			bot.Hand.Cards.Add(new Card(Rank.Five, Suit.Diamonds));
			bot.Hand.Cards.Add(new Card(Rank.Four, Suit.Diamonds));
			croupier.Hand.Cards.Add(new Card(Rank.Ace, Suit.Spades));
			croupier.Hand.Cards.Add(new Card(Rank.Eight, Suit.Clubs));
			bot.Finish();
			croupier.Finish();
			Assert.AreEqual(bot.Balance, 1000);

			bot.Balance = 1000;

			bot.MakeBet();
			bot.Hand.Cards.Add(new Card(Rank.King, Suit.Clubs));
			bot.Hand.Cards.Add(new Card(Rank.Five, Suit.Diamonds));
			bot.Hand.Cards.Add(new Card(Rank.Four, Suit.Diamonds));
			croupier.Hand.Cards.Add(new Card(Rank.Ace, Suit.Spades));
			croupier.Hand.Cards.Add(new Card(Rank.Five, Suit.Clubs));
			croupier.Hand.Cards.Add(new Card(Rank.King, Suit.Diamonds));
			bot.Finish();
			croupier.Finish();
			Assert.AreEqual(bot.Balance, 1025);

			bot.Balance = 1000;

			bot.MakeBet();
			bot.Hand.Cards.Add(new Card(Rank.King, Suit.Clubs));
			bot.Hand.Cards.Add(new Card(Rank.Five, Suit.Diamonds));
			bot.Hand.Cards.Add(new Card(Rank.Four, Suit.Diamonds));
			croupier.Hand.Cards.Add(new Card(Rank.Ace, Suit.Spades));
			croupier.Hand.Cards.Add(new Card(Rank.Seven, Suit.Clubs));
			bot.Finish();
			croupier.Finish();
			Assert.AreEqual(bot.Balance, 1025);

			bot.Balance = 1000;

			bot.MakeBet();
			bot.Hand.Cards.Add(new Card(Rank.King, Suit.Clubs));
			bot.Hand.Cards.Add(new Card(Rank.Five, Suit.Diamonds));
			croupier.Hand.Cards.Add(new Card(Rank.Ace, Suit.Spades));
			croupier.Hand.Cards.Add(new Card(Rank.Seven, Suit.Clubs));
			bot.Finish();
			croupier.Finish();
			Assert.AreEqual(bot.Balance, 950);

			Assert.Pass();
		}

		[Test]
		public void HitTest()
		{
			Game game = new Game();
			FirstBot bot = new FirstBot(game, 1000);
			for (int i = 0; i < 10; i++)
				bot.Hit();
			Assert.AreEqual(bot.Hand.Cards.Count, 10);

			Assert.Pass();
		}

		[Test]
		public void StartGameTest()
		{
			Game game = new Game();
			game.Start();
			Assert.AreEqual(game.Players.Count, 0);

			ThirdBot bot = new ThirdBot(game, 1000);
			game = new Game();
			game.Players.Add(new FirstBot(game, 800));
			game.Players.Add(new SecondBot(game, 800));
			game.Players.Add(bot);
			Assert.AreEqual(game.Players.Count, 3);
			bot.Balance = 0;
			game.Start();
		}
	}
}