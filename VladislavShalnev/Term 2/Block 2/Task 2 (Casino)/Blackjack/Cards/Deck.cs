namespace Blackjack.Cards;

public class Deck
{
	private readonly List<Card> _cards = new List<Card>();

	public Deck()
	{
		// Going through all card suits and types
		// and filling out the deck with 416 cards
		for (int i = 0; i < 8; i++)
			foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
				foreach (CardType type in Enum.GetValues(typeof(CardType)))
					_cards.Add(new Card(suit, type));

		// Shuffling
		Random random = new Random(DateTime.Now.Millisecond);
		_cards = _cards.OrderBy(_ => random.Next()).ToList();
	}

	public Card GetCard()
	{
		Card card = _cards[0];
		_cards.RemoveAt(0);
		return card;
	}
}