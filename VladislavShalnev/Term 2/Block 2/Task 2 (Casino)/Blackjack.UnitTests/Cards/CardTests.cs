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
}