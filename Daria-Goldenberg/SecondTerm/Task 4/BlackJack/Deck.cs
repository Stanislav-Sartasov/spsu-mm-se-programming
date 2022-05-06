namespace BlackJack
{
	public class Deck
	{
		private readonly List<Card> cards;

		public Deck()
		{
			cards = new List<Card>();

			CardSuit[] Suits = new CardSuit[] { CardSuit.Diamonds, CardSuit.Hearts, CardSuit.Clubs, CardSuit.Spades };
			CardRank[] Ranks = new CardRank[] { CardRank.Ace, CardRank.Two, CardRank.Three, CardRank.Four, CardRank.Five, CardRank.Six, CardRank.Seven,
												CardRank.Eight, CardRank.Nine, CardRank.Ten, CardRank.Jack, CardRank.Queen, CardRank.King };

			for (int i = 1; i <= 8; i++)
				foreach (CardSuit suit in Suits)
					foreach (CardRank rank in Ranks)
						cards.Add(new Card(rank, suit));

			cards = cards.OrderBy(a => new Random().Next()).ToList();
		}

		public Card TakeCard()
		{
			Card card = cards[0];
			cards.RemoveAt(0);
			return card;
		}

		public int CountCards()
		{
			return cards.Count;
		}
	}

}