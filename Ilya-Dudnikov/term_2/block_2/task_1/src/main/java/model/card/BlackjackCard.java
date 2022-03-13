package model.card;

public class BlackjackCard {
	Card card;
	CardStatus cardStatus;

	public BlackjackCard(int rank, Suits suit) {
		this.card = new Card(rank, suit);
		cardStatus = CardStatus.FACE_DOWN;
	}

	public BlackjackCard(int rank, Suits suit, CardStatus cardStatus) {
		this(rank, suit);
		this.cardStatus = cardStatus;
	}

	public BlackjackCard(Card card) {
		this(card.rank(), card.suit());
	}

	public BlackjackCard(Card card, CardStatus cardStatus) {
		this(card);
		this.cardStatus = cardStatus;
	}
}
