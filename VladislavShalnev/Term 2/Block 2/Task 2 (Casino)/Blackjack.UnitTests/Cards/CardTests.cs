using NUnit.Framework;

namespace Blackjack.Cards.UnitTests;

public class CardTests
{
	[TestCase(CardSuit.Clubs, CardType.Two, "Clubs Two")]
	[TestCase(CardSuit.Diamonds, CardType.Three, "Diamonds Three")]
	[TestCase(CardSuit.Hearts, CardType.Four, "Hearts Four")]
	[TestCase(CardSuit.Spades, CardType.Five, "Spades Five")]
	public void CardToStringTest(CardSuit suit, CardType type, string result)
	{
		Assert.AreEqual(result, new Card(suit, type).ToString());
	}
	
	[TestCase(CardSuit.Hearts, CardType.Four, 1, 4)]
	[TestCase(CardSuit.Hearts, CardType.Ace, 3, 11)]
	[TestCase(CardSuit.Spades, CardType.Ace, 13, 1)]
	public void GetValueTest(CardSuit suit, CardType type, int score, int result)
	{
		Assert.AreEqual(result, new Card(suit, type).GetValue(score));
	}
}