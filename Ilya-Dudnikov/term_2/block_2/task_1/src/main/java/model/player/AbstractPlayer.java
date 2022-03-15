package model.player;

import model.hand.Hand;

public abstract class AbstractPlayer {
	protected String id;
	protected Hand[] hands;

	public AbstractPlayer(String id) {
		this.id = id;
		hands = new Hand[] {new Hand()};
	}

	public Hand[] getHands() {
		return hands;
	}
}
