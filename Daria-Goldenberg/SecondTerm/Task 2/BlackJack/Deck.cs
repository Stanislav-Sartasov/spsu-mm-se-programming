namespace BlackJack
{
	public class Deck
	{
		public readonly List<Card> Cards;

		public Deck()
		{
			Cards = new List<Card>();

			Suit[] Suits = new Suit[] { Suit.Diamonds, Suit.Hearts, Suit.Clubs, Suit.Spades };
			Rank[] Ranks = new Rank[] { Rank.Ace, Rank.Two, Rank.Three, Rank.Four, Rank.Five, Rank.Six, Rank.Seven, 
										Rank.Eight, Rank.Nine, Rank.Ten, Rank.Jack, Rank.Queen, Rank.King };

			for (int i = 1; i <= 8; i++)
				foreach (Suit suit in Suits)
					foreach (Rank rank in Ranks)
						Cards.Add(new Card(rank, suit));

			Cards = Cards.OrderBy(a => new Random().Next()).ToList();
		}

		public Card TakeCard()
		{
			Card card = Cards[0];
			Cards.Remove(card);
			return card;
		}
	}
}
