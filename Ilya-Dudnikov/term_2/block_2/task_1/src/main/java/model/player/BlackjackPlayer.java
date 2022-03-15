package model.player;

import model.hand.Hand;

public class BlackjackPlayer extends AbstractPlayer {
	public BlackjackPlayer(String id) {
		super(id);
	}

	public void split() {
		Hand newHand = new Hand();
		newHand.addCard(hand.getCardAt(0));

		hand = newHand;
	}
}
