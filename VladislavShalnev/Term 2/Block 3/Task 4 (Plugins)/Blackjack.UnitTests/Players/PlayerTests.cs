using System.Collections.Generic;
using Blackjack.Cards;
using Bots;
using NUnit.Framework;

namespace Blackjack.Players.UnitTests;

public class PlayerTests
{
	[Test]
	public void ClearTest()
	{
		Player bot = new SillyBot("Silly", 1000);
		
		bot.MakeBet();
		
		Assert.AreNotEqual(0, bot.Bet);
		
		bot.Clear();
		
		Assert.AreEqual(0, bot.Bet);
	}
	
	[Test]
	public void BlackjackWinTest()
	{
		Player bot = new SillyBot("Silly", 1000);
		bot.TakeCard(new Card(CardSuit.Diamonds, CardType.Ten));
		bot.TakeCard(new Card(CardSuit.Diamonds, CardType.Ace));
		
		Dealer dealer = new Dealer();
		dealer.TakeCard(new Card(CardSuit.Diamonds, CardType.Eight));
		
		Assert.IsTrue(bot.HasBlackjack);
		
		bot.MakeBet();
		
		bot.Finish(dealer);
		
		Assert.AreEqual(1015, bot.Money);
	}
	
	[Test]
	public void BlackjackTieTest()
	{
		Player bot = new SillyBot("Silly", 1000);
		bot.TakeCard(new Card(CardSuit.Diamonds, CardType.Ten));
		bot.TakeCard(new Card(CardSuit.Diamonds, CardType.Ace));
		
		Dealer dealer = new Dealer();
		dealer.TakeCard(new Card(CardSuit.Clubs, CardType.Ten));
		dealer.TakeCard(new Card(CardSuit.Clubs, CardType.Ace));
		
		bot.MakeBet();
		
		bot.Finish(dealer);
		
		Assert.AreEqual(1000, bot.Money);
	}
	
	[Test]
	public void LossTest()
	{
		Player bot = new SillyBot("Silly", 1000);
		bot.TakeCard(new Card(CardSuit.Diamonds, CardType.Ten));
		bot.TakeCard(new Card(CardSuit.Diamonds, CardType.Ten));
		bot.TakeCard(new Card(CardSuit.Diamonds, CardType.Ten));
		
		Dealer dealer = new Dealer();

		bot.MakeBet();
		
		bot.Finish(dealer);
		
		Assert.AreEqual(990, bot.Money);
	}
	
	[Test]
	public void TwentyOneTieTest()
	{
		Player bot = new SillyBot("Silly", 1000);
		bot.TakeCard(new Card(CardSuit.Diamonds, CardType.Ten));
		bot.TakeCard(new Card(CardSuit.Diamonds, CardType.Five));
		bot.TakeCard(new Card(CardSuit.Diamonds, CardType.Six));
		
		Dealer dealer = new Dealer();
		dealer.TakeCard(new Card(CardSuit.Clubs, CardType.Ace));
		dealer.TakeCard(new Card(CardSuit.Clubs, CardType.Nine));
		dealer.TakeCard(new Card(CardSuit.Clubs, CardType.Ace));

		bot.MakeBet();
		
		bot.Finish(dealer);
		
		Assert.AreEqual(1000, bot.Money);
	}
	
	[Test]
	public void TwentyOneWinTest()
	{
		Player bot = new SillyBot("Silly", 1000);
		bot.TakeCard(new Card(CardSuit.Diamonds, CardType.Ten));
		bot.TakeCard(new Card(CardSuit.Diamonds, CardType.Five));
		bot.TakeCard(new Card(CardSuit.Diamonds, CardType.Six));
		
		Dealer dealer = new Dealer();
		dealer.TakeCard(new Card(CardSuit.Clubs, CardType.Ace));
		dealer.TakeCard(new Card(CardSuit.Clubs, CardType.Nine));

		bot.MakeBet();
		
		bot.Finish(dealer);
		
		Assert.AreEqual(1010, bot.Money);
	}
	
	[Test]
	public void LessTwentyOneTieTest()
	{
		Player bot = new SillyBot("Silly", 1000);
		bot.TakeCard(new Card(CardSuit.Diamonds, CardType.Ten));
		bot.TakeCard(new Card(CardSuit.Diamonds, CardType.Two));

		Dealer dealer = new Dealer();
		dealer.TakeCard(new Card(CardSuit.Clubs, CardType.Seven));
		dealer.TakeCard(new Card(CardSuit.Clubs, CardType.Five));

		bot.MakeBet();
		
		bot.Finish(dealer);
		
		Assert.AreEqual(1000, bot.Money);
	}
	
	[Test]
	public void LessTwentyOneLossTest()
	{
		Player bot = new SillyBot("Silly", 1000);
		bot.TakeCard(new Card(CardSuit.Diamonds, CardType.Ten));
		bot.TakeCard(new Card(CardSuit.Diamonds, CardType.Six));

		Dealer dealer = new Dealer();
		dealer.TakeCard(new Card(CardSuit.Clubs, CardType.Seven));
		dealer.TakeCard(new Card(CardSuit.Clubs, CardType.Queen));

		bot.MakeBet();
		
		bot.Finish(dealer);
		
		Assert.AreEqual(990, bot.Money);
	}
	
	[Test]
	public void LessTwentyOneWinTest()
	{
		Player bot = new SillyBot("Silly", 1000);
		bot.TakeCard(new Card(CardSuit.Diamonds, CardType.Ten));
		bot.TakeCard(new Card(CardSuit.Diamonds, CardType.Six));

		Dealer dealer = new Dealer();
		dealer.TakeCard(new Card(CardSuit.Clubs, CardType.Ten));
		dealer.TakeCard(new Card(CardSuit.Clubs, CardType.Ten));
		dealer.TakeCard(new Card(CardSuit.Clubs, CardType.Ten));

		bot.MakeBet();
		
		bot.Finish(dealer);
		
		Assert.AreEqual(1010, bot.Money);
	}
}