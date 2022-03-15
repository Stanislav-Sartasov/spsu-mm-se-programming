package model.bet;

public class Bet {
	private String bettorId;
	private int value;
	private BetStatus betStatus;

	public Bet(String bettorId, int value) {
		this.bettorId = bettorId;
		this.value = value;
	}

	public int getValue() {
		return value;
	}

	public BetStatus getBetStatus() {
		return betStatus;
	}

	public String getBettorId() {
		return bettorId;
	}

	public void changeBetStatus(BetStatus betStatus) {
		this.betStatus = betStatus;
	}
}
