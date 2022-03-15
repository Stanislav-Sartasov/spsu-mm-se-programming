package model.player;

import model.card.CardStatus;
import model.deck.Shoe;

public class BlackjackDealer extends AbstractPlayer {
	public BlackjackDealer(String id) {
		super(id);
	}

	public void dealTo(BlackjackPlayer player, Shoe shoe) {
		for (int i = 0; i < player.hands.length; i++) {
			var newCard = shoe.dealCard();
			newCard.setCardStatus(CardStatus.FACE_UP);
			player.hands[i].addCard(newCard);
		}
	}

	private void draw(Shoe shoe) {
		hands[0].addCard(shoe.dealCard());
	}
}
