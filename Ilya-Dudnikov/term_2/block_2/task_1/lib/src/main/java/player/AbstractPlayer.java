package player;

import hand.Hand;

public abstract class AbstractPlayer {
	protected String id;
	protected Hand hand;

	public AbstractPlayer(String id) {
		this.id = id;
		hand = new Hand();
	}

	public Hand getHand() {
		return hand;
	}

	public String getId() {
		return id;
	}
}
