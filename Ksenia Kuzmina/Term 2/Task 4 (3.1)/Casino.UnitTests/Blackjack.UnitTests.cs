using NUnit.Framework;
using System;
using Bots;

namespace Blackjack.UnitTests
{
	public class Tests
	{
		[Test]
		public void CardTest()
		{
			Deck deck = new Deck();
			for (int i = 0; i < deck.Cards.Count;)
			{
				if (deck.Cards[i].Number == CardNumber.Ace)
				{
					Assert.AreEqual(deck.Cards[i].ConvertCardToNumber(20), 1);
					Assert.AreEqual(deck.Cards[i].ConvertCardToNumber(5), 11);
				}
				else
				{
					Assert.AreEqual(deck.Cards[i].ConvertCardToNumber(0), Math.Min((int)deck.Cards[i].Number, 10));
					deck.Cards[i].FindOutTheNameOfTheCard();
				}
				i += 8;
			}
			Assert.Pass();
		}

		[Test]
		public void BotMakeBetTest()
		{
			Bot bot = new Bot(500);
			bot.MakeBet();
			Assert.AreEqual(bot.Money, 490);

			bot.Money = 5;
			bot.MakeBet();
			Assert.AreEqual(bot.Money, 4);

			Assert.Pass();
		}

		[Test]
		public void BotOneMakeBetTest()
		{
			BotOne botOne = new BotOne(500);
			botOne.MakeBet();
			Assert.AreEqual(botOne.Money, 250);

			botOne.Money = 5;
			botOne.MakeBet();
			Assert.AreEqual(botOne.Money, 4);

			Assert.Pass();
		}

		[Test]
		public void BotTwoMakeBetTest()
		{
			BotTwo botTwo = new BotTwo(500);
			botTwo.MakeBet();
			Assert.AreEqual(botTwo.Money, 400);

			botTwo.Money = 5;
			botTwo.MakeBet();
			Assert.AreEqual(botTwo.Money, 4);

			botTwo.Money = 400;
			botTwo.MakeBet();
			Assert.AreEqual(botTwo.Money, 350);

			botTwo.Money = 50;
			botTwo.MakeBet();
			Assert.AreEqual(botTwo.Money, 40);
		}

		[Test]
		public void BotGetInsuranceTest()
		{
			Bot bot = new Bot(500);
			Dealer dealer = new Dealer();

			bot.MakeBet();
			bot.Hands[0].Score = 21;
			bot.GetInsurance(dealer);
			Assert.AreEqual(bot.Money, 490);
			Assert.AreEqual(bot.Hands[0].Bet, 10);

			bot.Money = 500;
			bot.MakeBet();
			dealer.Score = 21;
			bot.GetInsurance(dealer);
			Assert.AreEqual(bot.Money, 500);
			Assert.AreEqual(bot.Hands[0].Bet, 0);

			bot.MakeBet();
			bot.Hands[0].Score = 5;
			bot.GetInsurance(dealer);
			Assert.AreEqual(bot.Money, 500);
			Assert.AreEqual(bot.Hands[0].Bet, 0);

			bot.MakeBet();
			dealer.Score = 5;
			bot.GetInsurance(dealer);
			Assert.AreEqual(bot.Money, 490);
			Assert.AreEqual(bot.Hands[0].Bet, 5);

			Assert.Pass();
		}

		[Test]
		public void BotOneGetInsuranceTest()
		{
			BotOne botOne = new BotOne(500);
			Dealer dealer = new Dealer();

			botOne.MakeBet();
			botOne.Hands[0].Score = 21;
			botOne.GetInsurance(dealer);
			Assert.AreEqual(botOne.Money, 250);
			Assert.AreEqual(botOne.Hands[0].Bet, 250);

			botOne.Money = 500;
			botOne.MakeBet();
			dealer.Score = 21;
			botOne.GetInsurance(dealer);
			Assert.AreEqual(botOne.Money, 500);
			Assert.AreEqual(botOne.Hands[0].Bet, 0);

			botOne.Hands[0].Bet = 249;
			botOne.Hands[0].Score = 5;
			botOne.GetInsurance(dealer);
			Assert.AreEqual(botOne.Money, 500);
			Assert.AreEqual(botOne.Hands[0].Bet, 0);

			botOne.Hands[0].Bet = 249;
			dealer.Score = 5;
			botOne.GetInsurance(dealer);
			Assert.AreEqual(botOne.Money, 500);
			Assert.AreEqual(botOne.Hands[0].Bet, 249);

			botOne.Hands[0].Bet = 350;
			botOne.GetInsurance(dealer);
			Assert.AreEqual(botOne.Money, 500);
			Assert.AreEqual(botOne.Hands[0].Bet, 175);

			botOne.Hands[0].Bet = 350;
			dealer.Score = 21;
			botOne.GetInsurance(dealer);
			Assert.AreEqual(botOne.Money, 850);
			Assert.AreEqual(botOne.Hands[0].Bet, 0);

			Assert.Pass();
		}

		[Test]
		public void BotTwoGetInsuranceTest()
		{
			BotTwo botTwo = new BotTwo(500);
			Dealer dealer = new Dealer();

			botTwo.Hands[0].Bet = 250;
			botTwo.Hands[0].Score = 21;
			botTwo.GetInsurance(dealer);
			Assert.AreEqual(botTwo.Money, 500);
			Assert.AreEqual(botTwo.Hands[0].Bet, 250);

			dealer.Score = 21;
			botTwo.GetInsurance(dealer);
			Assert.AreEqual(botTwo.Money, 750);
			Assert.AreEqual(botTwo.Hands[0].Bet, 0);

			botTwo.Hands[0].Bet = 99;
			botTwo.Hands[0].Score = 5;
			botTwo.GetInsurance(dealer);
			Assert.AreEqual(botTwo.Money, 750);
			Assert.AreEqual(botTwo.Hands[0].Bet, 0);

			botTwo.Hands[0].Bet = 99;
			dealer.Score = 5;
			botTwo.GetInsurance(dealer);
			Assert.AreEqual(botTwo.Money, 750);
			Assert.AreEqual(botTwo.Hands[0].Bet, 99);

			botTwo.Hands[0].Bet = 350;
			botTwo.GetInsurance(dealer);
			Assert.AreEqual(botTwo.Money, 750);
			Assert.AreEqual(botTwo.Hands[0].Bet, 175);

			botTwo.Hands[0].Bet = 350;
			dealer.Score = 21;
			botTwo.GetInsurance(dealer);
			Assert.AreEqual(botTwo.Money, 1100);
			Assert.AreEqual(botTwo.Hands[0].Bet, 0);

			Assert.Pass();
		}

		[Test]
		public void BotsPlayTest()
		{
			Dealer dealer = new Dealer();
			Deck deck = new Deck();

			Bot bot = new Bot(500);

			bot.Hands[0].Cards.Add(new Card(CardSuit.Spades, CardNumber.Eight));
			bot.Hands[0].Cards.Add(new Card(CardSuit.Spades, CardNumber.Eight));
			bot.PlayTurn(deck);

			bot.Hands[0].Score = 21;
			bot.PlayTurn(deck);

			bot.Hands[0].Cards[0] = new Card(CardSuit.Spades, CardNumber.Two);
			bot.Hands[0].Score = 10;

			bot.PlayTurn(deck);

			Assert.Pass();
		}

		[Test]
		public void FinishGameTest()
		{
			Dealer dealer = new Dealer();
			Bot bot = new Bot(500);
			Deck deck = new Deck();

			bot.Hands[0].Cards.Add(new Card(CardSuit.Spades, CardNumber.Two));
			bot.Hands[0].Cards.Add(new Card(CardSuit.Spades, CardNumber.Two));
			bot.Hands[0].Score = 21;
			bot.Hands[0].Bet = 5;
			dealer.Score = 21;
			bot.Finish(dealer);
			Assert.AreEqual(bot.Money, 507);

			bot.Hands[0].Score = 21;
			bot.Hands[0].Bet = 5;
			bot.Hands[0].Cards.Add(new Card(CardSuit.Spades, CardNumber.Two));
			bot.Hands[0].Cards.Add(new Card(CardSuit.Spades, CardNumber.Two));
			dealer.Cards.Add(new Card(CardSuit.Spades, CardNumber.Two));
			dealer.Cards.Add(new Card(CardSuit.Spades, CardNumber.Two));
			bot.Finish(dealer);
			Assert.AreEqual(bot.Money, 512);

			bot.Hands[0].Score = 21;
			bot.Hands[0].Bet = 5;
			bot.Hands[0].Cards.Add(new Card(CardSuit.Spades, CardNumber.Two));
			bot.Hands[0].Cards.Add(new Card(CardSuit.Spades, CardNumber.Two));
			dealer.Score = 20;
			bot.Finish(dealer);
			Assert.AreEqual(bot.Money, 519);

			bot.Hands[0].Bet = 5;
			bot.Hands[0].Score = 21;
			dealer.Score = 21;
			bot.Finish(dealer);
			Assert.AreEqual(bot.Money, 524);

			bot.Hands[0].Bet = 5;
			bot.Hands[0].Score = 21;
			dealer.Score = 20;
			bot.Finish(dealer);
			Assert.AreEqual(bot.Money, 531);

			bot.Hands[0].Bet = 5;
			bot.Hands[0].Score = 22;
			bot.Finish(dealer);
			Assert.AreEqual(bot.Money, 531);

			bot.Hands[0].Bet = 5;
			bot.Hands[0].Score = 20;
			dealer.Score = 21;
			bot.Finish(dealer);
			Assert.AreEqual(bot.Money, 531);

			bot.Hands[0].Bet = 5;
			bot.Hands[0].Score = 5;
			dealer.Score = 22;
			bot.Finish(dealer);
			Assert.AreEqual(bot.Money, 538);

			bot.Hands[0].Bet = 5;
			bot.Hands[0].Score = 18;
			dealer.Score = 17;
			bot.Finish(dealer);
			Assert.AreEqual(bot.Money, 545);

			bot.Hands[0].Bet = 5;
			bot.Hands[0].Score = 18;
			dealer.Score = 18;
			bot.Finish(dealer);
			Assert.AreEqual(bot.Money, 550);

			bot.Hands[0].Bet = 5;
			bot.Hands[0].Cards.Add(new Card(CardSuit.Spades, CardNumber.Eight));
			bot.Hands[0].Cards.Add(new Card(CardSuit.Spades, CardNumber.Eight));
			bot.PlayTurn(deck);
			bot.Finish(dealer);
			Assert.AreEqual(bot.Money, 559);

			Assert.Pass();
		}

		[Test]
		public void DealerTest()
		{
			Dealer dealer = new Dealer();
			Deck deck = new Deck();
			deck.Shuffle();

			Card firstCard = deck.Cards[0];
			Card secondCard = deck.Cards[1];

			dealer.Begin(deck);
			Assert.AreEqual(dealer.Cards[0].Number, firstCard.Number);
			Assert.AreEqual(dealer.Cards[1].Number, secondCard.Number);

			dealer.Score = 5;

			dealer.Play(deck);
			Assert.IsTrue(dealer.Score > 16);

			dealer.Finish();
			Assert.AreEqual(dealer.Score, 0);
			Assert.AreEqual(dealer.Cards.Count, 0);

			Assert.Pass();
		}


		[Test]
		public void PlayGameTest()
		{
			Game game = new Game();
			game.Players.Add(new Bot(0));
			game.PlayGame(500);
			Assert.AreEqual(game.Players.Count, 0);

			game.Players.Add(new Bot(500));
			game.Dealer.Cards.Add(new Card(CardSuit.Spades, CardNumber.Ten));
			game.Dealer.Score = 21;
			Assert.IsTrue(game.DealerGetBlackjack());

			game.Dealer.Score = 20;
			Assert.IsFalse(game.DealerGetBlackjack());

			for (int i = 0; i < 10; i++)
			{
				game.PlayGame(500);
			}

			Assert.Pass();
		}
	}
}
