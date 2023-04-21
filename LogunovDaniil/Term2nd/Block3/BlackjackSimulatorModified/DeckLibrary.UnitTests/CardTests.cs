using NUnit.Framework;

namespace DeckLibrary.UnitTests
{
	public class CardTests
	{
		[Test]
		public void RetainingValueTest()
		{
			CardSuits suit = CardSuits.Club;
			CardRanks rank = CardRanks.Eight;
			Card card = new(suit, rank);
			Assert.True(card.Suit == suit
				&& card.Rank == rank);
		}

		[Test]
		public void ToStringTest()
		{
			CardSuits suit = CardSuits.Spade;
			CardRanks rank = CardRanks.Ace;
			Card card = new(suit, rank);
			Assert.AreEqual(card.ToString(), ((char)suit).ToString() + ((char)rank).ToString());
		}
	}
}
