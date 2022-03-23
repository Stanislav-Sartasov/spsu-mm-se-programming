package card;


public class BlackjackCard extends AbstractCard {
	private CardStatus cardStatus;

	public BlackjackCard(int rank, Suits suit) {
		super(rank, suit);
		cardStatus = CardStatus.FACE_DOWN;
	}

	public BlackjackCard(int rank, Suits suit, CardStatus cardStatus) {
		this(rank, suit);
		this.cardStatus = cardStatus;
	}

	public BlackjackCard(Card card) {
		super(card);
	}

	public BlackjackCard(Card card, CardStatus cardStatus) {
		this(card);
		this.cardStatus = cardStatus;
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
