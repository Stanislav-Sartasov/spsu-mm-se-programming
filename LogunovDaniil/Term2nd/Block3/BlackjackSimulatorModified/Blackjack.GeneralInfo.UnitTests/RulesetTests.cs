using NUnit.Framework;
using System.Collections.Generic;
using DeckLibrary;

namespace Blackjack.GeneralInfo.UnitTests
{
	public class RulesetTests
	{
		private static readonly Card Ace = new(CardSuits.Club, CardRanks.Ace);
		private static readonly Card Six = new(CardSuits.Diamond, CardRanks.Six);
		private static readonly Card Ten = new(CardSuits.Heart, CardRanks.Ten);
		private static readonly Card King = new(CardSuits.Spade, CardRanks.King);

		[Test]
		public void EvaluateHandValueEmptyTest()
		{
			Assert.AreEqual(0, Ruleset.EvaluateHandValue(new List<Card>()));
		}

		[Test]
		public void EvaluateHandValueTwoAcesTest()
		{
			List<Card> cards = new() { Ace, Ace };
			Assert.AreEqual(12, Ruleset.EvaluateHandValue(cards));
		}

		[Test]
		public void EvaluateHandValueOneAceTest()
		{
			List<Card> cards = new() { Ace };
			Assert.AreEqual(11, Ruleset.EvaluateHandValue(cards));
		}

		[Test]
		public void EvaluateHandValueOneSixTest()
		{
			List<Card> cards = new() { Six };
			Assert.AreEqual(6, Ruleset.EvaluateHandValue(cards));
		}

		[Test]
		public void EvaluateHandValueBlackjackTest()
		{
			List<Card> cards = new() { Ace, King };
			Assert.AreEqual(21, Ruleset.EvaluateHandValue(cards));
		}

		[Test]
		public void IsBlackjackTrueAKTest()
		{
			List<Card> cards = new() { Ace, King };
			Assert.True(Ruleset.IsBlackjack(cards));
		}

		[Test]
		public void IsBlackjackTrueTATest()
		{
			List<Card> cards = new() { Ten, Ace };
			Assert.True(Ruleset.IsBlackjack(cards));
		}

		[Test]
		public void IsBlackjackFalseA6Test()
		{
			List<Card> cards = new() { Ace, Six };
			Assert.False(Ruleset.IsBlackjack(cards));
		}
	}
}
