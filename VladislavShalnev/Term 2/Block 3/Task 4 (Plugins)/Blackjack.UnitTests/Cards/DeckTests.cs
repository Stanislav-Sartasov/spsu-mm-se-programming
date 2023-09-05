using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Blackjack.Cards.UnitTests;

public class DeckTests
{
	[Test]
	public void GetCardTest()
	{
		Deck deck = new Deck();
		List<Card> allCards = new List<Card>();
		List<Card> deckCards = new List<Card>();
		
		for (int i = 0; i < 8; i++)
			foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
				foreach (CardType type in Enum.GetValues(typeof(CardType)))
					allCards.Add(new Card(suit, type));
		
		for (int i = 0; i < 416; i++)
			deckCards.Add(deck.GetCard());
		
		Assert.AreEqual(allCards.Count, deckCards.Count);

		foreach (Card card in allCards)
			if (!deckCards.Contains(card))
				Assert.Fail();
		
		Assert.Pass();
	}
}