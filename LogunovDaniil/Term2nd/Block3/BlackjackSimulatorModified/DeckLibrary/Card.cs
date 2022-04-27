namespace DeckLibrary
{
	public class Card
	{
		public CardSuits Suit { private set; get; }
		public CardRanks Rank { private set; get; }

		public Card(CardSuits suit, CardRanks rank)
		{
			this.Suit = suit;
			this.Rank = rank;
		}

		public override string ToString()
		{
			char[] str = { (char)Suit, (char)Rank };
			return new string(str);
		}
	}
}
