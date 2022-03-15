package model.player;

import model.card.CardStatus;
import model.deck.Shoe;

public class BlackjackDealer extends AbstractPlayer {
	public BlackjackDealer(String id) {
		super(id);
	}

	public void dealTo(AbstractPlayer player, Shoe shoe) {
		var newCard = shoe.dealCard();
		newCard.setCardStatus(CardStatus.FACE_UP);
		player.hand.addCard(newCard);
	}

	public void draw(Shoe shoe) {
		hand.showCards();

		while (hand.getHandScore() < 17) {
			dealTo(this, shoe);
		}
	}
}
