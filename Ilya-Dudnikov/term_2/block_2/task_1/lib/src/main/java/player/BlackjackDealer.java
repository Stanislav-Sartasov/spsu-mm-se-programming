package player;

import card.CardStatus;
import deck.Shoe;

public class BlackjackDealer extends AbstractPlayer implements IDealer {
	public BlackjackDealer(String id) {
		super(id);
	}

	public void dealTo(AbstractPlayer player, Shoe shoe, CardStatus status) {
		var newCard = shoe.dealCard();
		newCard.setCardStatus(status);
		player.hand.addCard(newCard);
	}

	public void draw(Shoe shoe) {
		hand.showCards();

		while (hand.getHandScore() < 17) {
			dealTo(this, shoe, CardStatus.FACE_UP);
		}
	}
}
