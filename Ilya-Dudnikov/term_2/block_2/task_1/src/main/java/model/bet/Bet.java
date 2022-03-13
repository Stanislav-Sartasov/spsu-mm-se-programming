package model.bet;

public class Bet {
	private int value;
	private BetStatus betStatus;

	public Bet(int value) {
		this.value = value;
	}

	public int getValue() {
		return value;
	}

	public BetStatus getBetStatus() {
		return betStatus;
	}

	private void changeBetStatus(BetStatus betStatus) {
		this.betStatus = betStatus;
	}
}
