package card;


public class BlackjackCard extends AbstractCard {
	private CardStatus cardStatus;

	public BlackjackCard(int rank, Suits suit) {
		super(rank, suit);
		cardStatus = CardStatus.FACE_DOWN;
	}

	public BlackjackCard(Card card) {
		this(card.rank(), card.suit());
	}

	public void setCardStatus(CardStatus cardStatus) {
		this.cardStatus = cardStatus;
	}

	public Card getCard() {
		return card;
	}

	public CardStatus getCardStatus() {
		return cardStatus;
	}
}
