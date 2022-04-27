using NUnit.Framework;
using System.Collections.Generic;
using System;

namespace DeckLibrary.UnitTests
{
	public class DeckTests
	{
		private void CheckContent(Deck deck)
		{
			List<string> fullDeck = new();
			foreach (CardSuits suit in Enum.GetValues(typeof(CardSuits)))
			{
				foreach (CardRanks rank in Enum.GetValues(typeof(CardRanks)))
				{
					fullDeck.Add(new Card(suit, rank).ToString());
				}
			}
			while (deck.GetPercentageLeft() > 0)
			{
				string card = deck.DrawNextCard().ToString();
				if (fullDeck.Contains(card))
					fullDeck.Remove(card);
				else
					Assert.Fail($"card '{card}' is duplicated");
			}
			if (fullDeck.Count > 0)
				Assert.Fail($"deck has {fullDeck.Count} less cards than it should");
			Assert.Pass();
		}

		[Test]
		public void InUseTest()
		{
			Deck deck = new(1);
			while (deck.GetPercentageLeft() > 0)
			{
				Card? card = deck.DrawNextCard();
				if (card == null)
					Assert.Fail("GetPercentageLeft() returned non-zero value, but there are no cards left");
			}
			Assert.Pass();
		}

		[Test]
		public void ContentAtCreationTest()
		{
			Deck deck = new(1);
			CheckContent(deck);
		}

		[Test]
		public void ContentAtReshuffleTest()
		{
			Deck deck = new(1);
			deck.Reshuffle();
			CheckContent(deck);
		}

		[Test]
		public void PercentageLeftNaNTest()
		{
			Deck deck = new(0);
			Assert.AreEqual(double.NaN, deck.GetPercentageLeft());
		}

		[Test]
		public void PercentageLeftFullTest()
		{
			Deck deck = new(1);
			Assert.AreEqual(1.0, deck.GetPercentageLeft());
		}
	}
}
