namespace DeckLibrary
{
	public class Deck
	{
		private Card[] cards = new Card[0];
		private int cardPos;
		private int curDeckCount;

		private void FillDeck(int deckCount)
		{
			List<Card> newCards = new();
			for (int i = 0; i < deckCount; i++)
			{
				foreach (CardSuits suit in Enum.GetValues(typeof(CardSuits)))
				{
					foreach (CardRanks rank in Enum.GetValues(typeof(CardRanks)))
					{
						newCards.Add(new Card(suit, rank));
					}
				}
			}
			cards = newCards.ToArray();
		}

		public Deck(int deckCount)
		{
			FillDeck(deckCount);
		}

		public void Reshuffle()
		{ 
			cardPos = 0;
			var rand = new Random();
			cards = cards.OrderBy(x => rand.Next(0, cards.Length)).ToArray();
		}

		public Card? DrawNextCard()
		{
			if (cardPos >= cards.Length)
				return null;
			var card = cards[cardPos];
			cardPos++;
			return card;
		}

		public double GetPercentageLeft()
		{
			if (cards.Length == 0)
				return double.NaN;
			return 1.0 - cardPos / cards.Length;
		}
	}
}
