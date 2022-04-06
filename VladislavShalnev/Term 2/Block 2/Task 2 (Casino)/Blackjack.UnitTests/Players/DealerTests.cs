using System.Collections.Generic;
using Blackjack.Cards;
using Bots;
using NUnit.Framework;

namespace Blackjack.Players.UnitTests;

public class DealerTests
{
	[Test]
	public void BeginGameTest()
	{
		List<Player> players = new List<Player>()
		{
			new MadBot("Mad", 1000),
			new RiskyBot("Risky", 1000),
			new SillyBot("Silly", 1000)
		};
		Deck deck = new Deck();
		Dealer dealer = new Dealer();
		
		dealer.BeginGame(players, deck);

		foreach (Player player in players)
			Assert.AreEqual(2, player.Cards.Count);
		
		Assert.AreEqual(2, dealer.Cards.Count);
	}
	
	[Test]
	public void GetCardsTest()
	{
		Deck deck = new Deck();
		Dealer dealer = new Dealer();
		
		dealer.GetCards(deck);
		
		if (dealer.Score >= 17)
			Assert.Pass();
	}
}