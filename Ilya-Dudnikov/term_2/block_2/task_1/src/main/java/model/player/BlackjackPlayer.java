package model.player;

import model.hand.Hand;

public class BlackjackPlayer extends AbstractPlayer {
	public void split() {
		if (hands.length >= 2) {
			throw new IllegalArgumentException("You cannot split more than one hand");
		}
		Hand newHand = new Hand();
		newHand.addCard(hands[0].getCardAt(0));

		hands = new Hand[] {newHand, newHand};
	}
}
