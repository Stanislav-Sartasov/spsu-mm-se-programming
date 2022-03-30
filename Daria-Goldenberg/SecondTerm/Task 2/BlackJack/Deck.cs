namespace BlackJack
{
	public class Deck
	{
		public List<Card> Cards;

		public Deck()
		{
			Cards = new List<Card>();

			Suit[] Suits = new Suit[] { Suit.Diamonds, Suit.Hearts, Suit.Clubs, Suit.Spades };
			string[] Ranks = new string[] { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King" };

			for (int i = 0; i < 8; i++)
				foreach (Suit suit in Suits)
					foreach (string rank in Ranks)
						Cards.Add(new Card(rank, rank + "-" + ConvertSuitToString(suit)));

			Cards = Cards.OrderBy(a => new Random().Next()).ToList();
		}

		private static string ConvertSuitToString(Suit suit)
		{
			return suit switch
			{
				Suit.Diamonds => "Diamonds",
				Suit.Hearts => "Hearts",
				Suit.Clubs => "Clubs",
				Suit.Spades => "Spades",
				_ => "",
			};
		}

		public Card TakeCard()
		{
			Card card = Cards[0];
			Cards.Remove(card);
			return card;
		}
	}
}
