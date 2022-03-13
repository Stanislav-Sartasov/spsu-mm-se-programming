package model.hand;

import model.card.BlackjackCard;

import java.util.ArrayList;

public class Hand {
	private ArrayList<BlackjackCard> cardsArray;

	public Hand() {
		cardsArray = new ArrayList<>();
	}

	public BlackjackCard getCardAt(int pos) {
		return cardsArray.get(pos);
	}

	public void addCard(BlackjackCard card) {
		cardsArray.add(card);
	}

	public void clear() {
		cardsArray.clear();
	}
}
