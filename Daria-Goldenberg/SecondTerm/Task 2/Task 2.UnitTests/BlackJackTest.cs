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
			Assert.AreEqual(new Card(CardRank.Three, CardSuit.Diamonds).GetValue(10), 3);
			Assert.AreEqual(new Card(CardRank.Eight, CardSuit.Diamonds).GetValue(15), 8);
			Assert.AreEqual(new Card(CardRank.Ace, CardSuit.Clubs).GetValue(10), 11);
			Assert.AreEqual(new Card(CardRank.Ace, CardSuit.Clubs).GetValue(15), 1);
			Assert.AreEqual(new Card(CardRank.King, CardSuit.Spades).GetValue(10), 10);
			Assert.AreEqual(new Card(CardRank.Jack, CardSuit.Hearts).GetValue(15), 10);

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
			hand.Cards.Add(new Card(CardRank.Three, CardSuit.Diamonds));
			hand.Cards.Add(new Card(CardRank.Ace, CardSuit.Clubs));
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

		[TestCase(1000, 950)]
		[TestCase(20, 15)]
		public void FirstBotMakeBetTest(int balance, int result)
		{
			Game game = new Game();
			FirstBot bot = new FirstBot(game, balance);
			bot.MakeBet();
			Assert.AreEqual(bot.Balance, result);

			Assert.Pass();
		}

		[TestCase(1000, 875)]
		[TestCase(800, 750)]
		[TestCase(204, 153)]
		[TestCase(198, 99)]
		[TestCase(37, 27)]
		public void SecondBotMakeBetTest(int balance, int result)
		{
			Game game = new Game();

			SecondBot bot = new SecondBot(game, balance);
			bot.MakeBet();
			Assert.AreEqual(bot.Balance, result);

			Assert.Pass();
		}

		[TestCase(1000, 880)]
		[TestCase(700, 640)]
		[TestCase(500, 470)]
		[TestCase(300, 285)]
		[TestCase(100, 95)]
		public void ThirdBotMakeBetTest(int balance, int result)
		{
			Game game = new Game();

			ThirdBot bot = new ThirdBot(game, balance);
			bot.MakeBet();
			Assert.AreEqual(bot.Balance, result);

			Assert.Pass();
		}

		[Test]
		public void PlayTurnTest()
		{
			Game game = new Game();
			FirstBot bot = new FirstBot(game, 1000);

			bot.Hand.Cards.Add(new Card(CardRank.Ace, CardSuit.Clubs));
			bot.Hand.Cards.Add(new Card(CardRank.Eight, CardSuit.Diamonds));
			bot.PlayTurn();
			Assert.AreEqual(bot.Hand.CountPoints(), 19);
			Assert.AreEqual(bot.Hand.Cards.Count, 2);

			bot = new FirstBot(game, 1000);

			bot.Hand.Cards.Add(new Card(CardRank.Ace, CardSuit.Clubs));
			bot.Hand.Cards.Add(new Card(CardRank.Jack, CardSuit.Diamonds));
			bot.PlayTurn();
			Assert.AreEqual(bot.Hand.CountPoints(), 21);
			Assert.AreEqual(bot.Hand.Cards.Count, 2);

			bot = new FirstBot(game, 1000);

			bot.Hand.Cards.Add(new Card(CardRank.Ace, CardSuit.Clubs));
			bot.Hand.Cards.Add(new Card(CardRank.Seven, CardSuit.Diamonds));
			bot.PlayTurn();
			Assert.AreEqual(bot.Hand.Cards.Count, 3);

			Assert.Pass();
		}

		[Test]
		public void PlayerFinishTest()
		{
			Game game = new Game();
			FirstBot bot = new FirstBot(game, 1000);
			Croupier croupier = game.Croupier;

			bot.MakeBet();
			bot.Hand.Cards.Add(new Card(CardRank.King, CardSuit.Clubs));
			bot.Hand.Cards.Add(new Card(CardRank.Five, CardSuit.Diamonds));
			bot.Hand.Cards.Add(new Card(CardRank.Seven, CardSuit.Diamonds));
			bot.Finish();
			Assert.AreEqual(bot.Balance, 950);

			bot = new FirstBot(game, 1000);

			bot.MakeBet();
			bot.Hand.Cards.Add(new Card(CardRank.King, CardSuit.Clubs));
			bot.Hand.Cards.Add(new Card(CardRank.Five, CardSuit.Diamonds));
			bot.Hand.Cards.Add(new Card(CardRank.Six, CardSuit.Diamonds));
			croupier.Hand.Cards.Add(new Card(CardRank.Ace, CardSuit.Spades));
			croupier.Hand.Cards.Add(new Card(CardRank.Ten, CardSuit.Clubs));
			bot.Finish();
			croupier.Finish();
			Assert.AreEqual(bot.Balance, 1000);

			bot = new FirstBot(game, 1000);

			bot.MakeBet();
			bot.Hand.Cards.Add(new Card(CardRank.Ace, CardSuit.Spades));
			bot.Hand.Cards.Add(new Card(CardRank.Ten, CardSuit.Clubs));
			croupier.Hand.Cards.Add(new Card(CardRank.Ace, CardSuit.Clubs));
			croupier.Hand.Cards.Add(new Card(CardRank.Ten, CardSuit.Spades));
			bot.Finish();
			croupier.Finish();
			Assert.AreEqual(bot.Balance, 1000);

			bot = new FirstBot(game, 1000);

			bot.MakeBet();
			bot.Hand.Cards.Add(new Card(CardRank.Ace, CardSuit.Spades));
			bot.Hand.Cards.Add(new Card(CardRank.Ten, CardSuit.Clubs));
			croupier.Hand.Cards.Add(new Card(CardRank.Ace, CardSuit.Clubs));
			croupier.Hand.Cards.Add(new Card(CardRank.Seven, CardSuit.Spades));
			bot.Finish();
			croupier.Finish();
			Assert.AreEqual(bot.Balance, 1025);

			bot = new FirstBot(game, 1000);

			bot.MakeBet();
			bot.Hand.Cards.Add(new Card(CardRank.King, CardSuit.Clubs));
			bot.Hand.Cards.Add(new Card(CardRank.Five, CardSuit.Diamonds));
			bot.Hand.Cards.Add(new Card(CardRank.Six, CardSuit.Diamonds));
			croupier.Hand.Cards.Add(new Card(CardRank.Ace, CardSuit.Spades));
			croupier.Hand.Cards.Add(new Card(CardRank.Four, CardSuit.Clubs));
			croupier.Hand.Cards.Add(new Card(CardRank.Six, CardSuit.Spades));
			bot.Finish();
			croupier.Finish();
			Assert.AreEqual(bot.Balance, 1000);

			bot = new FirstBot(game, 1000);

			bot.MakeBet();
			bot.Hand.Cards.Add(new Card(CardRank.King, CardSuit.Clubs));
			bot.Hand.Cards.Add(new Card(CardRank.Five, CardSuit.Diamonds));
			bot.Hand.Cards.Add(new Card(CardRank.Six, CardSuit.Diamonds));
			croupier.Hand.Cards.Add(new Card(CardRank.Ace, CardSuit.Spades));
			croupier.Hand.Cards.Add(new Card(CardRank.Seven, CardSuit.Clubs));
			bot.Finish();
			croupier.Finish();
			Assert.AreEqual(bot.Balance, 1025);

			bot = new FirstBot(game, 1000);

			bot.MakeBet();
			bot.Hand.Cards.Add(new Card(CardRank.King, CardSuit.Clubs));
			bot.Hand.Cards.Add(new Card(CardRank.Five, CardSuit.Diamonds));
			bot.Hand.Cards.Add(new Card(CardRank.Four, CardSuit.Diamonds));
			croupier.Hand.Cards.Add(new Card(CardRank.Ace, CardSuit.Spades));
			croupier.Hand.Cards.Add(new Card(CardRank.Eight, CardSuit.Clubs));
			bot.Finish();
			croupier.Finish();
			Assert.AreEqual(bot.Balance, 1000);

			bot = new FirstBot(game, 1000);

			bot.MakeBet();
			bot.Hand.Cards.Add(new Card(CardRank.King, CardSuit.Clubs));
			bot.Hand.Cards.Add(new Card(CardRank.Five, CardSuit.Diamonds));
			bot.Hand.Cards.Add(new Card(CardRank.Four, CardSuit.Diamonds));
			croupier.Hand.Cards.Add(new Card(CardRank.Ace, CardSuit.Spades));
			croupier.Hand.Cards.Add(new Card(CardRank.Five, CardSuit.Clubs));
			croupier.Hand.Cards.Add(new Card(CardRank.King, CardSuit.Diamonds));
			bot.Finish();
			croupier.Finish();
			Assert.AreEqual(bot.Balance, 1025);

			bot = new FirstBot(game, 1000);

			bot.MakeBet();
			bot.Hand.Cards.Add(new Card(CardRank.King, CardSuit.Clubs));
			bot.Hand.Cards.Add(new Card(CardRank.Five, CardSuit.Diamonds));
			bot.Hand.Cards.Add(new Card(CardRank.Four, CardSuit.Diamonds));
			croupier.Hand.Cards.Add(new Card(CardRank.Ace, CardSuit.Spades));
			croupier.Hand.Cards.Add(new Card(CardRank.Seven, CardSuit.Clubs));
			bot.Finish();
			croupier.Finish();
			Assert.AreEqual(bot.Balance, 1025);

			bot = new FirstBot(game, 1000);

			bot.MakeBet();
			bot.Hand.Cards.Add(new Card(CardRank.King, CardSuit.Clubs));
			bot.Hand.Cards.Add(new Card(CardRank.Five, CardSuit.Diamonds));
			croupier.Hand.Cards.Add(new Card(CardRank.Ace, CardSuit.Spades));
			croupier.Hand.Cards.Add(new Card(CardRank.Seven, CardSuit.Clubs));
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

			game = new Game();
			game.Players.Add(new FirstBot(game, 800));
			game.Players.Add(new SecondBot(game, 0));
			game.Players.Add(new ThirdBot(game, 1000));
			Assert.AreEqual(game.Players.Count, 3);
			game.Start();
			Assert.AreEqual(game.Players.Count, 2);

			Assert.Pass();
		}
	}
}