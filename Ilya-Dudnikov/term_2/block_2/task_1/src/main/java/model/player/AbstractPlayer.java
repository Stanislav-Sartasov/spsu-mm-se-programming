package model.player;

import model.hand.Hand;

public abstract class AbstractPlayer {
	protected String id;
	protected Hand[] hands;

	public Hand[] getHands() {
		return hands;
	}
}
